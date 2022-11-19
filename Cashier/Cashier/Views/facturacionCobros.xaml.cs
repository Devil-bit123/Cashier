using Cashier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cashier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class facturacionCobros : ContentPage
    {
        public facturacionCobros()
        {
            InitializeComponent();            
            lblFechaFac.Text = DateTime.Now.ToString();
            if (SwitchBusqueda.IsToggled == false)
            {
                txtBusquedaCli.Keyboard = Keyboard.Numeric;
            }
            else if (SwitchBusqueda.IsToggled == true)
            {
                txtBusquedaCli.Keyboard = Keyboard.Default;
            }
            if (string.IsNullOrEmpty(txtBusquedaCli.Text))
            {
                CVcarrito.ItemsSource = null;
            }
            NumFac();
            llenarCV();
        }

        protected async override void OnAppearing()
        {
            decimal acumulador = 0;
            //Write the code of your page here        
            var carrito = await App.SQLiteDB.recuperarDetallesAsync(lblNumFac.Text);
            if (carrito!=null)
            {
                CVcarrito.ItemsSource = carrito;
                foreach (var c in carrito)
                {
                    acumulador += c.total;
                    lblTotal.Text = acumulador.ToString();
                }
            }
            if (CVcarrito.SelectedItem!=null)
            {
                CVcarrito.SelectedItem = null;
            }
            base.OnAppearing();
        }
        #region funciones

        private async void NumFac()
        {
            var Nfac = await App.SQLiteDB.recuperarFacturasPagadaAssync();
            var n = Nfac.Count() + 1;
            lblNumFac.Text = n.ToString();
        }


        private void limpiarResBusqueda()
        {
            txtBusquedaCli.Text = "";
            lblNomCli.Text = "";
            lblApeCli.Text = "";
            lblCedCli.Text = "";
            lblDirCli.Text = "";
            lblteflCli.Text = "";
        }

        private void limpiarAddPannel()
        {
            txtNomCliente.Text = "";
            txtApeCliente.Text = "";
            txtCedCliente.Text = "";
            txtDirCliente.Text = "";
            txTelfCliente.Text = "";
        }

        private void limpiarCarrito()
        {

            lblNumFac.Text = "";
        }
        private bool validarTxtAddPannel()
        {
            bool res;
            if (string.IsNullOrEmpty(txtNomCliente.Text))
            {
                res = false;
            }
            else if (string.IsNullOrEmpty(txtApeCliente.Text))
            {
                res = false;
            }
            else if (string.IsNullOrEmpty(txtCedCliente.Text))
            {
                res = false;
            }
            else if (string.IsNullOrEmpty(txtDirCliente.Text))
            {
                res = false;
            }
            else if (string.IsNullOrEmpty(txTelfCliente.Text))
            {
                res = false;
            }
            else
            {
                res = true;
            }
            return res;

        }

        private bool validarTxtResultados()
        {
            bool res;
            if (string.IsNullOrEmpty(lblNomCli.Text))
            {
                res = false;
            }
            else if (string.IsNullOrEmpty(lblApeCli.Text))
            {
                res = false;
            }
            else if (string.IsNullOrEmpty(lblCedCli.Text))
            {
                res = false;
            }
            else if (string.IsNullOrEmpty(lblDirCli.Text))
            {
                res = false;
            }
            else if (string.IsNullOrEmpty(lblteflCli.Text))
            {
                res = false;
            }
            else
            {
                res = true;
            }
            return res;
        }

        private async void llenarCV()
        {
            decimal acumulador = 0;
            var carrito = await App.SQLiteDB.recuperarDetallesAsync(lblNumFac.Text);
            if(carrito != null)
            {
                CVcarrito.ItemsSource = carrito;
                foreach (var c in carrito)
                {
                   
                    for (int i = 0; i < carrito.Count; i++)
                    {

                        acumulador += c.total;

                    }
                    lblTotal.Text = acumulador.ToString();
                }
            }
        }

        #endregion
        private async void txtBusquedaCli_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (SwitchBusqueda.IsToggled == false)
            {
                txtBusquedaCli.MaxLength = 10;
                var cedCli = await App.SQLiteDB.recuperarClienteXCedAsync(txtBusquedaCli.Text);
                if (cedCli != null)
                {
                    lblNomCli.Text = cedCli.nomCliente.ToString();
                    lblApeCli.Text = cedCli.apeCliente.ToString();
                    lblCedCli.Text = cedCli.cedCliente.ToString();
                    lblDirCli.Text = cedCli.dirCliente.ToString();
                    lblteflCli.Text = cedCli.telfCliente.ToString();
                }
            }

            if (SwitchBusqueda.IsToggled == true)
            {
                var nomCli = await App.SQLiteDB.recuperarClienteXNomAsync(txtBusquedaCli.Text);
                if (nomCli != null)
                {
                    lblNomCli.Text = nomCli.nomCliente.ToString();
                    lblApeCli.Text = nomCli.apeCliente.ToString();
                    lblCedCli.Text = nomCli.cedCliente.ToString();
                    lblDirCli.Text = nomCli.dirCliente.ToString();
                    lblteflCli.Text = nomCli.telfCliente.ToString();
                }
            }
            if (string.IsNullOrEmpty(txtBusquedaCli.Text))
            {
                limpiarResBusqueda();
            }

        }
        private void SwitchBusqueda_Toggled(object sender, ToggledEventArgs e)
        {

            var state = e.Value;
            if (state == false)
            {
                txtBusquedaCli.Placeholder = "Cedula del cliente";
                txtBusquedaCli.Keyboard = Keyboard.Numeric;
                limpiarResBusqueda();
            }
            if (state == true)
            {
                txtBusquedaCli.Placeholder = "Nombre del cliente";
                txtBusquedaCli.Keyboard = Keyboard.Default;
                limpiarResBusqueda();
            }


        }

        private void btnAddCli_Clicked(object sender, EventArgs e)
        {
            limpiarResBusqueda();
            addCliPanel.IsVisible = true;
            REsultadosPannel.IsVisible = false;
            BtnScanProd.IsVisible = false;
            BtnCancelFac.IsVisible = false;
            txtBusquedaCli.IsEnabled = false;

        }

        private async void BtncAcceptPannel_Clicked(object sender, EventArgs e)
        {
            
            if (validarTxtAddPannel())
            {
                cliente cli = new cliente
                {
                    nomCliente=txtNomCliente.Text,
                    apeCliente=txtApeCliente.Text,
                    cedCliente=txtCedCliente.Text,
                    dirCliente=txtDirCliente.Text,
                    telfCliente=txTelfCliente.Text,
                    estadoCliente= "Activo"
                };
                await App.SQLiteDB.crearClienteAsync(cli);
                await DisplayAlert("OK!","Cliente creado","OK");
                limpiarAddPannel();
                addCliPanel.IsVisible = false;
                REsultadosPannel.IsVisible = true;
                txtBusquedaCli.IsEnabled = true;

            }
            else
            {
                await DisplayAlert("ERROR!", "Cliente no pudo ser creado", "OK");
            }


        }

        private void BtnnCancelPanel_Clicked(object sender, EventArgs e)
        {

            addCliPanel.IsVisible = false;
            REsultadosPannel.IsVisible = true;
            BtnScanProd.IsVisible = true;
            BtnCancelFac.IsVisible = true;
            limpiarAddPannel();
            txtBusquedaCli.IsEnabled = true;


        }

        private async void BtnScanProd_Clicked(object sender, EventArgs e)
        {
            if (validarTxtResultados())
            {
                encFactura encabezado = new encFactura
                {
                    numFac = lblNumFac.Text,
                    fechaEnc=lblFechaFac.Text,
                    nomCli=lblNomCli.Text,
                    apeCli=lblApeCli.Text,
                    cedCli=lblCedCli.Text,
                    dirCliente=lblDirCli.Text,
                    telfCliente=lblteflCli.Text,
                    estadoEnc="Abierta"
                };
                await App.SQLiteDB.crearEncFActuraeAsync(encabezado);
                scanProductos modal = new scanProductos();
                modal.BindingContext = encabezado;
                await this.Navigation.PushModalAsync(modal);
            }
            else
            {
               await DisplayAlert("ERROR!", "Necesita agregar un clietne primero", "OK");
            } 


        }

        private async void BtnCancelFac_Clicked(object sender, EventArgs e)
        {
           bool res = await DisplayAlert("ESTA SEGURO", "Desea cancelar la venta", "SI", "NO");
            if (res == true)
            {


                var carrito = await App.SQLiteDB.recuperarDetallesAsync(lblNumFac.Text);
                if (carrito!=null)
                {
                    foreach (var c in carrito)
                    {
                        int devolver = c.cantidad;
                        var dev = await App.SQLiteDB.recuperarProductoXNombreAsync(c.nomProducto);
                        if(dev != null)
                        {
                            int devolucion = dev.existProd + devolver;
                            dev.idProducto = dev.idProducto;
                            dev.nomProducto = dev.nomProducto;
                            dev.descProducto = dev.descProducto;
                            dev.preCompraProd = dev.preCompraProd;
                            dev.preVentaProd = dev.preVentaProd;
                            dev.existProd = devolucion;
                            dev.idCategoria = dev.idCategoria;
                            dev.idProveedor = dev.idProveedor;
                            dev.barCodeProd=dev.barCodeProd;
                            dev.imageProd = dev.imageProd;
                            dev.estadoProd = dev.estadoProd;
                        }
                        await App.SQLiteDB.editarProductoAsync(dev);
                        await App.SQLiteDB.eliminiarCarritoAsync(c);
                    }
                }
                var enc = await App.SQLiteDB.recuperarEcnabezadoXnumFAcAsync(lblNumFac.Text);
                if (enc != null)
                {                   
                    await App.SQLiteDB.eliminiarEncabezadoAsync(enc);
                }
                await DisplayAlert("OK!", "Se cancelo la venta", "OK");
                await Navigation.PopToRootAsync();
            }else if(res==false){
                //no cancelo la venta
            }
            
        }

        private async void CVcarrito_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedProd = e.CurrentSelection.FirstOrDefault() as detFac;
            if (selectedProd!=null)
            {
                var edProd = await App.SQLiteDB.recuperarDetalleXIdAsync(selectedProd.idDet,selectedProd.numFac);
                if (edProd!=null)
                {
                    editCarrito modal = new editCarrito();
                    modal.BindingContext = edProd;
                    await this.Navigation.PushModalAsync(modal);
                }
            }

        }

        private async void BtnCash_Clicked(object sender, EventArgs e)
        {
            var enc = await App.SQLiteDB.recuperarEcnabezadoXnumFAcAsync(lblNumFac.Text);
            if (enc != null)
            {
                encFactura cierre = new encFactura
                {
                    idEnc = enc.idEnc,
                    numFac = enc.numFac,
                    fechaEnc = enc.fechaEnc,
                    nomCli = enc.nomCli,
                    apeCli = enc.apeCli,
                    cedCli = enc.cedCli,
                    dirCliente = enc.dirCliente,
                    telfCliente = enc.telfCliente,
                    estadoEnc = "Pagada"
                };
                await App.SQLiteDB.editarEncabezadoAsync(cierre);
                await DisplayAlert("OK!","Venta realizada","OK");
                limpiarResBusqueda();
                CVcarrito.ItemsSource = null;
                limpiarCarrito();
                await this.Navigation.PopToRootAsync();
                Navigation.RemovePage(this);
            }
            else
            {
                await DisplayAlert("ERROR!", "Primero debe agregar un cliente y facturar productos", "OK");
            }
            

        }
    }
}

