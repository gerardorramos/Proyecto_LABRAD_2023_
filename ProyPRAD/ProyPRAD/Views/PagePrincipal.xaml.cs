using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyPRAD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePrincipal : ContentPage
    {
        public PagePrincipal()
        {
            InitializeComponent();
        }

        private async void btn_places_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaSitios());
        }

        private async void btn_contacts_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaContactos());
        }

        private async void btn_AddContacto_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageContactos());
        }

        private async void btn_AddSitio_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageSitios());
        }
    }
}