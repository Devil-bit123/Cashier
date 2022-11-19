using Cashier.Models;
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
    public partial class facturacionPagadas : ContentPage
    {
        public facturacionPagadas()
        {
            InitializeComponent();
            llenarValores();
        }

        protected async override void OnAppearing()
        {
            if (CVFActuras.SelectedItem != null)
            {
                CVFActuras.SelectedItem = null;
            }
           

        }

        #region funcoines

        private async void llenarValores()
        {
            var grid = await App.SQLiteDB.recuperarFacturasPagadaAssync();
            if (grid!=null)
            {
                CVFActuras.ItemsSource = grid;
            }
        }
        #endregion
      

        

        private void CVFActuras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var itemselected = CVFActuras.SelectedItem as encFactura;
            if (itemselected!=null)
            {
                lblNFAc.Text = itemselected.numFac;
                facturaPagadaDetalle pagadaDetalle = new facturaPagadaDetalle();
                pagadaDetalle.BindingContext = itemselected;
                this.Navigation.PushModalAsync(pagadaDetalle);
                CVFActuras.SelectedItem = null;
            }
        }
    }
}