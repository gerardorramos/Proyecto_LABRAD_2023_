using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyPRAD.Controllers;
using ProyPRAD.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using ProyPRAD.Models;


namespace ProyPRAD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaContactos : ContentPage
    {

        
        string Number;
        
        double telefono;
        
        

        public ListaContactos()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            listacontactos.ItemsSource = await DateBase.ObtenerListaContactos();
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageUpdContact());
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageDelContact());
        }

        private async void listacontactos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            {
                Models.Contacts Contacto = (Models.Contacts)e.CurrentSelection.FirstOrDefault();
                Number = Contacto.Telefono.ToString();
                
                telefono = Contacto.Telefono;
                
            }

            var Answer = await Application.Current.MainPage.DisplayAlert("Informacion", "Desea llamar al Contacto?", "Si", "No");
            if (Answer == true)
            {
                try
                {
                    PlacePhoneCall(Number);

                }
                catch
                 (System.Exception)
                {
                    _ = DisplayAlert("No se puede hacer llamada", "intente de nuevo", "ok");
                }
            }
            else

            {
                return;

            }

        }

        private void btnllamar_Clikced(object sender, EventArgs e)
        {
            PlacePhoneCall(Number);
        }

        public void PlacePhoneCall(string number)
        {
            try
            {
                PhoneDialer.Open(number);
            }
            catch (ArgumentNullException anEx)
            {
                
                DisplayAlert("Advertencia", Convert.ToString(anEx), "Ok");
            }
            catch (FeatureNotSupportedException ex)
            {
                
                DisplayAlert("Advertencia", Convert.ToString(ex), "Ok");
            }
            catch (Exception ex)
            {
                
                DisplayAlert("Advertencia", Convert.ToString(ex), "Ok");
            }
        }

    }
}