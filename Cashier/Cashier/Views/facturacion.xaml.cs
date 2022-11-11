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
    public partial class facturacion : ContentPage
    {
        public facturacion()
        {
            InitializeComponent();
        }

        private async void btnCobrar_Clicked(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
            await App.masterDetail.Detail.Navigation.PushAsync(new facturacionCobros());
        }

        private async void btnFcturas_Clicked(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
            await App.masterDetail.Detail.Navigation.PushAsync(new facturacionPagadas());
        }
    }
}