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
            int NC;
            int AE;
            int Dif;
            int NE;

            if (!string.IsNullOrEmpty(lblIDdet.Text))
            {
                var prod = await App.SQLiteDB.recuperarProductoXNombreAsync(lblNom.Text);
                if (prod!=null)
                {
                    AE = prod.existProd;
                    NC = Convert.ToInt32(stpCant.Value);
                    Dif = Convert.ToInt16(Aexistencias.Text) - NC;
                    NE = AE + Dif;
                    producto nuevoProd = new producto
                    {
                        idProducto = prod.idProducto,
                        nomProducto=prod.nomProducto,
                        descProducto=prod.descProducto,
                        preCompraProd=prod.preCompraProd,
                        preVentaProd=prod.preVentaProd,
                        existProd=NE,
                        idCategoria=prod.idCategoria,
                        idProveedor=prod.idProveedor,
                        barCodeProd=prod.barCodeProd,
                        imageProd=prod.imageProd,
                        estadoProd=prod.estadoProd
                    };
                    var Nprod = await App.SQLiteDB.editarProductoAsync(nuevoProd);

                    detFac det = new detFac
                    {
                        idDet = Convert.ToInt32(lblIDdet.Text),
                        numFac= lblNF.Text,
                        nomProducto = lblNom.Text,
                        preVentaProducto=Convert.ToDecimal(lblPVP.Text),
                        cantidad=Convert.ToInt32(lblCant.Text),
                        total=Convert.ToDecimal(lblTot.Text)
                    };
                    var Ncarrito = await App.SQLiteDB.editarDetallesAsync(det);
                    await DisplayAlert("OK!", "Producto editado", "OK");
                    await this.Navigation.PopModalAsync();
                   
                }
                
            }

        }

        private void stpCant_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            lblCant.Text= e.NewValue.ToString();
            decimal pvp = Convert.ToDecimal(lblPVP.Text);
            decimal cant = Convert.ToDecimal(lblCant.Text);
            decimal tot = pvp * cant;
            lblTotal.Text= tot.ToString();
            lblTot.Text= tot.ToString();
        }

       

  
    }
}