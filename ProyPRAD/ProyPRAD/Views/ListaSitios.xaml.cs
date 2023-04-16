using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyPRAD.Controllers;
using ProyPRAD.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyPRAD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaSitios : ContentPage
    {
        ///variables para ver el sitio del mapa
        public int id;
        public double lat;
        public double lon;
        public string pais;

        public ListaSitios()
        {
            InitializeComponent();
        }

        private async void listasitios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Answer = await Application.Current.MainPage.DisplayAlert("Informacion", "Desea ver la ubicación?", "Si", "No");
            if (e.CurrentSelection != null)

                if (Answer == true)
                {
                    {
                        var Site = new Sites()
                        {
                            Id = id,
                            Longitud = lon,
                            Latitud = lat,
                            Pais = pais

                        };

                        Models.Sites sitio = (Models.Sites)e.CurrentSelection.FirstOrDefault();

                        var mappage = new Views.PageMaps();
                        mappage.BindingContext = sitio;
                        await Navigation.PushAsync(mappage);
                    }
                }
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            listasitios.ItemsSource = await DateBase.ObtenerListaSitios();
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageUpdSites());
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageDelSites());
        }

        private async void btnvermapa_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageMaps());
        }
    }
}