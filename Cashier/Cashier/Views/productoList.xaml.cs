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
    public partial class productoList : ContentPage
    {
        public productoList()
        {
            InitializeComponent();
            LLenarCV();
            
           
        }

        protected async override void OnAppearing()
        {
            CVprodA.SelectedItem = null;
            
            LLenarCV();
            base.OnAppearing();
           
        }

        #region Funciones
        private async void LLenarCV()
        {
            var prodA = await App.SQLiteDB.recuperarProductoACTVAsync();
            if (prodA != null)
            {
                CVprodA.ItemsSource = prodA;
            }
            var prodI = await App.SQLiteDB.recuperarProductoINACTVsync();
            if (prodI!=null)
            {
                CVprodI.ItemsSource = prodI;
            }
        }
        #endregion

        private void CVprodA_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedProd = e.CurrentSelection.FirstOrDefault() as producto;
            if (selectedProd != null)
            {
                if (!string.IsNullOrEmpty(selectedProd.idProducto.ToString()))
                {
                    productoDetail detail = new productoDetail(selectedProd.idProducto);
                    this.Navigation.PushModalAsync(detail);
                }

            }
            
           
           
        }

        private async void CVprodI_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedProd = e.CurrentSelection.FirstOrDefault() as producto;
            if (!string.IsNullOrEmpty(selectedProd.idProducto.ToString()))
            {
                productoDetail detail = new productoDetail(selectedProd.idProducto);
                await this.Navigation.PushModalAsync(detail);
            }
            else
            {

            }

        }
    }
}