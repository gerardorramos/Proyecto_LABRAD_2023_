using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyPRAD.Views;
using System.IO;
using ProyPRAD.Controllers;

namespace ProyPRAD
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DateBase.Conexion(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DBPR.db3"));

            MainPage = new NavigationPage(new PagePrincipal());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
