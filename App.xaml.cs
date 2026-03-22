using AgendaContactos.Views;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using static System.Net.Mime.MediaTypeNames;
using Application = Microsoft.Maui.Controls.Application;

namespace AgendaContactos
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            return new Window(new NavigationPage(new ContactosPage()));
        }
    }
}
