using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AgendaContactos.Models;
using Microsoft.Maui;
using System.Linq;
using Microsoft.Maui.Controls;

namespace AgendaContactos.ViewModels
{
    public class ContactosViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Contacto> _todosLosContactos;

        private ObservableCollection<Contacto> _contactosFiltrados;
        public ObservableCollection<Contacto> ContactosFiltrados
        {
            get => _contactosFiltrados;
            set
            {
                _contactosFiltrados = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ContadorResultados));
            }
        }

        private string _textoBusqueda;
        public string TextoBusqueda
        {
            get => _textoBusqueda;
            set
            {
                _textoBusqueda = value;
                OnPropertyChanged();
                FiltrarContactos();
            }
        }

        private Contacto _contactoSeleccionado;
        public Contacto ContactoSeleccionado
        {
            get => _contactoSeleccionado;
            set
            {
                _contactoSeleccionado = value;
                OnPropertyChanged();
                if (value != null)
                    MostrarDetalleContacto(value);
            }
        }

        public string ContadorResultados =>
            $"{ContactosFiltrados?.Count ?? 0} de {_todosLosContactos?.Count ?? 0} contactos";

        public ICommand AgregarContactoCommand { get; }
        public ICommand LimpiarBusquedaCommand { get; }

        private int _nextId = 6;

        public ContactosViewModel()
        {
            _todosLosContactos = new ObservableCollection<Contacto>
            {
                new Contacto(1, "Esmeralda Delgado", "809-555-1234", "esmeralda@email.com", "\U0001F469"),
                new Contacto(2, "Carlos Martinez", "809-555-5678", "carlos@email.com", "\U0001F468"),
                new Contacto(3, "Luis Fernandez", "809-555-9012", "luis@email.com", "\U0001F468"),
                new Contacto(4, "Ana Rodriguez", "809-555-3456", "ana@email.com", "\U0001F469\u200D\U0001F4BB"),
                new Contacto(5, "Miguel Santos", "809-555-7890", "miguel@email.com", "\U0001F468\u200D\U0001F4BB")
            };

            ContactosFiltrados = new ObservableCollection<Contacto>(_todosLosContactos);

            AgregarContactoCommand = new Command(AgregarContacto);
            LimpiarBusquedaCommand = new Command(LimpiarBusqueda);
        }

        private void FiltrarContactos()
        {
            if (string.IsNullOrWhiteSpace(TextoBusqueda))
            {
                ContactosFiltrados = new ObservableCollection<Contacto>(_todosLosContactos);
            }
            else
            {
                var busqueda = TextoBusqueda.ToLower();
                var resultados = _todosLosContactos
                    .Where(c => c.Nombre.ToLower().Contains(busqueda) ||
                                c.Telefono.Contains(busqueda) ||
                                c.Email.ToLower().Contains(busqueda))
                    .ToList();

                ContactosFiltrados = new ObservableCollection<Contacto>(resultados);
            }
        }

        private async void MostrarDetalleContacto(Contacto contacto)
        {
            string detalle = "ID: " + contacto.Id + "\n" +
                             "Telefono: " + contacto.Telefono + "\n" +
                             "Email: " + contacto.Email;

            await Application.Current.MainPage.DisplayAlert(
                contacto.Avatar + " " + contacto.Nombre,
                detalle,
                "Cerrar");

            ContactoSeleccionado = null;
        }

        private async void AgregarContacto()
        {
            var page = Application.Current.MainPage;

            string nombre = await page.DisplayPromptAsync(
                "Nuevo Contacto",
                "Ingresa el nombre completo:",
                "Siguiente",
                "Cancelar",
                "Ej: Juan Perez");

            if (string.IsNullOrWhiteSpace(nombre)) return;

            string telefono = await page.DisplayPromptAsync(
                "Nuevo Contacto",
                "Ingresa el telefono:",
                "Siguiente",
                "Cancelar",
                "Ej: 809-555-0000",
                keyboard: Keyboard.Telephone);

            if (string.IsNullOrWhiteSpace(telefono)) return;

            string email = await page.DisplayPromptAsync(
                "Nuevo Contacto",
                "Ingresa el correo electronico:",
                "Agregar",
                "Cancelar",
                "Ej: correo@email.com",
                keyboard: Keyboard.Email);

            if (string.IsNullOrWhiteSpace(email)) return;

            string[] emojis = { "\U0001F600", "\U0001F60E", "\U0001F913", "\U0001F9D1", "\U0001F464" };
            var random = new Random();
            string avatar = emojis[random.Next(emojis.Length)];

            var nuevoContacto = new Contacto(
                _nextId++,
                nombre.Trim(),
                telefono.Trim(),
                email.Trim(),
                avatar
            );

            _todosLosContactos.Add(nuevoContacto);
            FiltrarContactos();

            await page.DisplayAlert(
                "Exito",
                "Contacto '" + nuevoContacto.Nombre + "' agregado correctamente.",
                "OK");
        }

        private void LimpiarBusqueda()
        {
            TextoBusqueda = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
