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
    public partial class proveedorVer : ContentPage
    {
        public proveedorVer()
        {
            InitializeComponent();
        }

        #region funciones

        private async void llenarCV()
        {
            var prodA = await App.SQLiteDB.recuperarProductoACTVAsync();
            if(prodA != null)
            {
                CVprodA.ItemsSource=prodA;
            }
        }
        #endregion
    }
}