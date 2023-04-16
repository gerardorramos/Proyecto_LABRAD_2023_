using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyPRAD.Models;
using ProyPRAD.Controllers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyPRAD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageDelSites : ContentPage
    {
        public PageDelSites()
        {
            InitializeComponent();
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var sitio = new Sites
            {
                Id = Convert.ToInt32(id.Text),
            };

            await DateBase.DelSitio(sitio);

            if (await DateBase.DelSitio(sitio) == 0)
                await DisplayAlert("Aviso", "Sitio Eliminado", "OK");
            else
                await DisplayAlert("Aviso", "ha ocurrido un error", "OK");
        }
    }
}