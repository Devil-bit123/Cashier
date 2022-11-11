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
    public partial class editCarrito : ContentPage
    {
        public editCarrito()
        {
            InitializeComponent();
        }

        private async void btnCobrar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblIDProd.Text))
            {
                detFac detalle = new detFac
                {
                    idDet=Convert.ToInt32(lblIDProd.Text),
                    numFac=lblNF.Text,
                    nomProducto=lblNom.Text,
                    preVentaProducto=Convert.ToDecimal(lblPVP.Text),
                    cantidad=Convert.ToInt32(lblCant.Text),
                    total=Convert.ToDecimal(lblTot.Text)
                };
                await App.SQLiteDB.editarDetallesAsync(detalle);               
                var newStock = await App.SQLiteDB.recuperarProductoXNombreAsync(lblNom.Text);
                if (newStock != null)
                {
                    int stockA = newStock.existProd;
                    int ns=stockA+Convert.ToInt32(lblCant.Text);
                    producto nstock = new producto
                    {
                        idProducto = newStock.idProducto,
                        nomProducto = newStock.nomProducto,
                        descProducto = newStock.descProducto,
                        preCompraProd = newStock.preCompraProd,
                        preVentaProd = newStock.preVentaProd,
                        existProd = ns,
                        idCategoria = newStock.idCategoria,
                        idProveedor = newStock.idProveedor,
                        barCodeProd = newStock.barCodeProd,
                        imageProd = newStock.imageProd,
                        estadoProd = newStock.estadoProd,
                    };
                    await App.SQLiteDB.editarProductoAsync(nstock);
                }
                await DisplayAlert("OK!", "Producto Editado", "OK");
            }

        }

        private void stpCant_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            lblCant.Text= e.NewValue.ToString();
            decimal pvp = Convert.ToDecimal(lblPVP.Text);
            int cant = Convert.ToInt32(lblCant.Text);
            decimal tot = pvp * cant;
            lblTot.Text= tot.ToString();
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopModalAsync();
        }

  
    }
}