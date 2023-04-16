using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyPRAD.Models;
using ProyPRAD.Controllers;
using Plugin.Media;
using System.IO;

namespace ProyPRAD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageContactos : ContentPage
    {

        Plugin.Media.Abstractions.MediaFile photo = null;
        public PageContactos()
        {
            InitializeComponent();
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
            if (String.IsNullOrWhiteSpace(nombres.Text))
            {
                await this.DisplayAlert("Advertencia", "El campo Nombres esta vacio", "Ok");
                return false;
            }
            if (string.IsNullOrWhiteSpace(apellidos.Text))
            {
                await this.DisplayAlert("Advertencia", "El campo Apellidos es Obligatorio", "Ok");
                return false;
            }
            if (string.IsNullOrWhiteSpace(telefono.Text))
            {
                await this.DisplayAlert("Advertencia", "El campo Telefono es Obligatorio", "Ok");
                return false;
            }
            if (string.IsNullOrWhiteSpace(edad.Text))
            {
                await this.DisplayAlert("Advertencia", "El campo edad es Obligatorio", "Ok");
                return false;
            }

            if (string.IsNullOrWhiteSpace(nota.Text))
            {
                await this.DisplayAlert("Advertencia", "El campo Nota es Obligatorio", "Ok");
                return false;
            }
            return true;
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

        private async void btSalvar_Clicked(object sender, EventArgs e)
        {
            if (photo == null)
                return;

            if (await Validador())
            {
                await DisplayAlert("Exito", "Información Coherente", "Ok");
                var contactos = new Contacts()

                {
                    Foto = traeImagenByteArray(),
                    Nombres = nombres.Text,
                    Apellidos = apellidos.Text,
                    Telefono = Convert.ToDouble(telefono.Text),
                    Edad = Convert.ToDouble(edad.Text),
                    Pais = pais.SelectedItem.ToString(),
                    Nota = nota.Text,
                };

                await DateBase.AddContacto(contactos);

                if (await DateBase.AddContacto(contactos) > 0)
                    await DisplayAlert("Aviso", "Contacto Adicionado", "OK");
                else
                    await DisplayAlert("Aviso", "ha ocurrido un error", "OK");
                nombres.Text = "";
                apellidos.Text = "";
                telefono.Text = "";
                edad.Text = "";
                nota.Text = "";
                

            }



        }
    }
}
