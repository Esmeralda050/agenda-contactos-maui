namespace AgendaContactos.Models
{
    public class Contacto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }

        public Contacto(int id, string nombre, string telefono, string email, string avatar)
        {
            Id = id;
            Nombre = nombre;
            Telefono = telefono;
            Email = email;
            Avatar = avatar;
        }
    }
}
