using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cashier.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cashier
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : ContentPage
    {
        public Master()
        {
            InitializeComponent();
        }

        private async void btnCategorias_Clicked(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
            await App.masterDetail.Detail.Navigation.PushAsync(new categoria());
        }

        private async void btnProductos_Clicked(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
        }
    }
}