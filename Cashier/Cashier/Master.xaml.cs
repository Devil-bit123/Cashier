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
            Button btn = (Button)sender;
          
            App.masterDetail.IsPresented = false;
            await App.masterDetail.Detail.Navigation.PushAsync(new categoriaView());
        }
    

        private async void btnEmpresas_Clicked(object sender, EventArgs e)
        {
            
            App.masterDetail.IsPresented = false;
            await App.masterDetail.Detail.Navigation.PushAsync(new empresaView());
        }

        private async void btnProveedores_Clicked(object sender, EventArgs e)
        {
           
            App.masterDetail.IsPresented = false;
            await App.masterDetail.Detail.Navigation.PushAsync(new proveedorView());
        }

        private async void btnProductos_Clicked(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
            await App.masterDetail.Detail.Navigation.PushAsync(new productoView());
        }

        private async void btnClientes_Clicked(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
            await App.masterDetail.Detail.Navigation.PushAsync(new clientesView());

        }

        private async void btnFacturacion_Clicked(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
            await App.masterDetail.Detail.Navigation.PushAsync(new facturacion());
        }
    }
}