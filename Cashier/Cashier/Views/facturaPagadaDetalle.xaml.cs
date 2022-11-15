using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cashier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class facturaPagadaDetalle : ContentPage
    {
        public facturaPagadaDetalle()
        {
            InitializeComponent();
            llenarCV();
        }
        protected override void OnAppearing()
        {
            //Write the code of your page here
            llenarCV();
            base.OnAppearing();
        }

        #region funcoines
        private async void llenarCV()
        {
            decimal acumulador = 0;
            var carrito = await App.SQLiteDB.recuperarDetallesAsync(lblNF.Text);
            if (carrito != null)
            {
                CVcarrito.ItemsSource = carrito;
                foreach (var c in carrito)
                {
                                    
                    
                        acumulador += c.total;
                   
                    lblTotal.Text = acumulador.ToString();
                }
            }
        }

        #endregion

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopModalAsync();
        }
    }
}