using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyPRAD.Models;
using ProyPRAD.Controllers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using System.IO;


namespace ProyPRAD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageDelContact : ContentPage
    {
   
        public PageDelContact()
        {
            InitializeComponent();
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var contactos = new Contacts
            {
                Id = Convert.ToInt32(id.Text),
            };

            await DateBase.DelContacto(contactos);

            if (await DateBase.DelContacto(contactos) == 0)
                await DisplayAlert("Aviso", "Contacto Eliminado", "OK");
            
            else
                await DisplayAlert("Aviso", "ha ocurrido un error", "OK");
            
        }
    }
}    
 