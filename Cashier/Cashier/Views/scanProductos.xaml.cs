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
    public partial class scanProductos : ContentPage
    {
        public scanProductos()
        {
            InitializeComponent();
            lblCantidad.Text = stpCantidad.Value.ToString();
        }

        #region funcoines
        private void limpiarResBus()
        {
            lblIdProd.Text = "";
            ImgProd.Source = "";
            lblprodN.Text = "";
            lblprodD.Text = "";
            lblprodPC.Text = "";
            lblprodPV.Text = "";
            lblprodEx.Text = "";
            lblprodCat.Text = "";
            lblprodProv.Text = "";
            lblprodCod.Text = "";            
            lblCantidad.Text = "1";
            lblTotal.Text = "";
            stpCantidad.Value = 1;
            txtBusquedaNomProd.Text = "";

        }

        #endregion
        private void btnBack_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopModalAsync();
            limpiarResBus();

        }

        private void SwitchBusquedaProds_Toggled(object sender, ToggledEventArgs e)
        {
            var state = e.Value;
            if (state == false)
            {
                ScanPannel.IsVisible = false;               
                lblAvisoScan.Text = "Cambie el switch para scannear el codigo del producto*";
            }
            if (state == true)
            {
                ScanPannel.IsVisible = true;               
                lblAvisoScan.Text = "Cambie el switch para ingresar el nombre del producto*";
            }
        }

        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(()=>
            {
                txtBusquedaNomProd.Text = result.Text;
            });
        }

        private async void txtBusquedaNomProd_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (SwitchBusquedaProds.IsToggled == false)
            {
                var prod = await App.SQLiteDB.recuperarProductoXNombreAsync(txtBusquedaNomProd.Text);
                if (prod != null)
                {
                    lblIdProd.Text = prod.idProducto.ToString();
                    ImgProd.Source = prod.imageProd;
                    lblprodN.Text = prod.nomProducto;
                    lblprodD.Text = prod.descProducto;
                    lblprodPC.Text = prod.preCompraProd.ToString();
                    lblprodPV.Text = prod.preVentaProd.ToString();
                    lblprodEx.Text = prod.existProd.ToString();
                    lblprodCat.Text = prod.idCategoria;
                    lblprodProv.Text = prod.idProveedor;
                    lblprodCod.Text = prod.barCodeProd;
                    decimal PV = Convert.ToDecimal(lblprodPV.Text);
                    decimal cant = Convert.ToDecimal(lblCantidad.Text);
                    decimal tot=PV*cant;
                    lblTotal.Text = tot.ToString(); 

                }
            }
            else if (SwitchBusquedaProds.IsToggled == true) {
                var prodCod = await App.SQLiteDB.recuperarProductoXCodigoAsync(txtBusquedaNomProd.Text);
                if (prodCod!=null)
                {
                    lblIdProd.Text = prodCod.idProducto.ToString();
                    ImgProd.Source = prodCod.imageProd;
                    lblprodN.Text = prodCod.nomProducto;
                    lblprodD.Text = prodCod.descProducto;
                    lblprodPC.Text = prodCod.preCompraProd.ToString();
                    lblprodPV.Text = prodCod.preVentaProd.ToString();
                    lblprodEx.Text = prodCod.existProd.ToString();
                    lblprodCat.Text = prodCod.idCategoria;
                    lblprodProv.Text = prodCod.idProveedor;
                    lblprodCod.Text = prodCod.barCodeProd;

                }
            
            
            }
            

            

        }

        private void stpCantidad_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(lblprodPV.Text))
            {
                lblCantidad.Text = e.NewValue.ToString();
                int cant = Convert.ToInt32(lblCantidad.Text);
                decimal precV = Convert.ToDecimal(lblprodPV.Text);
                lblTotal.Text = Convert.ToString(precV * cant);
            }
            else
            {
                lblprodPV.Text = "0";
            }
            
        }

        private async void btnCobrar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblIdProd.Text))
            {
                detFac detalle = new detFac
                {
                    numFac = lblNumFac.Text,
                    nomProducto = lblprodN.Text,
                    preVentaProducto = Convert.ToDecimal(lblprodPV.Text),
                    cantidad = Convert.ToInt32(lblCantidad.Text),
                    total = Convert.ToDecimal(lblTotal.Text)
                };
                await App.SQLiteDB.facturarProductoAsync(detalle);
                var newStock = await App.SQLiteDB.recuperarProductoXidAsync(Convert.ToInt32(lblIdProd.Text));
                if (newStock != null)
                {
                    int ns = Convert.ToInt32(lblprodEx.Text)-Convert.ToInt32(lblCantidad.Text);
                    producto nstock = new producto
                    {
                        idProducto=newStock.idProducto,
                        nomProducto=newStock.nomProducto,
                        descProducto=newStock.descProducto,
                        preCompraProd=newStock.preCompraProd,
                        preVentaProd=newStock.preVentaProd,
                        existProd=ns,
                        idCategoria=newStock.idCategoria,
                        idProveedor=newStock.idProveedor,
                        barCodeProd=newStock.barCodeProd,                        
                        imageProd=newStock.imageProd,
                        estadoProd = newStock.estadoProd,
                    };
                    await App.SQLiteDB.editarProductoAsync(nstock);
                }
                await DisplayAlert("OK!","Producto Facturado","OK");
                limpiarResBus();
            }
            else
            {
                await DisplayAlert("ERROR!", "Primero debe facturar productos", "OK");
            }

        }
    }
}

