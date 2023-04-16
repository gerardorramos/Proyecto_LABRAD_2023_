using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyPRAD.Models;
using ProyPRAD.Controllers;
using Plugin.Media;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading;

namespace ProyPRAD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSitios : ContentPage
    {
        Plugin.Media.Abstractions.MediaFile photo = null;
        public PageSitios()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            CancellationTokenSource cts;

            try
            {
                var location = await Geolocation.GetLocationAsync();

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    latitud.Text = Convert.ToString(location.Latitude);
                    longitud.Text = Convert.ToString(location.Longitude);
                }
                else
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                    cts = new CancellationTokenSource();
                    location = await Geolocation.GetLocationAsync(request, cts.Token);

                    if (location != null)
                    {
                        latitud.Text = Convert.ToString(location.Latitude);
                        longitud.Text = Convert.ToString(location.Longitude);
                    }
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                fnsEx.ToString();
                // Handle not supported on device exception
            }
        }

        private Byte[] traeImagenByteArray()
        {
            if (photo != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = photo.GetStream();
                    stream.CopyTo(memory);
                    return memory.ToArray();
                }
            }
            return null;
        }

        private async Task<bool> Validador()
        {
            if (String.IsNullOrWhiteSpace(nombre_sitio.Text))
            {
                await this.DisplayAlert("Advertencia", "El campo Nombre Sitio esta vacio", "Ok");
                return false;
            }
            if (string.IsNullOrWhiteSpace(latitud.Text))
            {
                await this.DisplayAlert("Advertencia", "El campo Latitud es Obligatorio", "Ok");
                return false;
            }
            if (string.IsNullOrWhiteSpace(longitud.Text))
            {
                await this.DisplayAlert("Advertencia", "El campo Longitud es Obligatorio", "Ok");
                return false;
            }
            if (string.IsNullOrWhiteSpace(nota.Text))
            {
                await this.DisplayAlert("Advertencia", "El campo Nota es Obligatorio", "Ok");
                return false;
            }
            return true;
        }

        private async void btnAgregar_Clicked(object sender, EventArgs e)
        {
            if (photo == null)
                return;
            if (await Validador())
            {
                await DisplayAlert("Exito", "Información Coherente", "Ok");
                var site = new Sites
                {
                    Nombre_sitio = nombre_sitio.Text,
                    Longitud = Convert.ToDouble(longitud.Text),
                    Latitud = Convert.ToDouble(latitud.Text),
                    Foto = traeImagenByteArray(),
                    Pais = pais.SelectedItem.ToString(),
                    Nota = nota.Text,
                };

                if (await DateBase.AddSitio(site) > 0)
                    await DisplayAlert("Aviso", "Registro Adicionado", "OK");
                else
                    await DisplayAlert("Aviso", "ha ocurrido un error", "OK");
                nombre_sitio.Text = "";
                latitud.Text = "";
                longitud.Text = "";
                nota.Text = "";
            }
        }


            private async void btnfoto_Clicked(object sender, EventArgs e)
            {
                photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "FotosApp",
                    Name = "test.jpg",
                    SaveToAlbum = true
                });

                if (photo != null)
                {
                    Foto.Source = ImageSource.FromStream(() =>
                    {
                        return photo.GetStream();
                    });
                }
            }
        }
    
}