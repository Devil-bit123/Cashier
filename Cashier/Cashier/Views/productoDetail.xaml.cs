using Cashier.Models;
using Java.Lang;
using Plugin.Media.Abstractions;
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
    public partial class productoDetail : ContentPage
    {

        public productoDetail(int id)
        {
            InitializeComponent();
            txtIdProd.Text = Convert.ToString(id);
            llenarCV();
            llenarPICKS();
            EditPannel.IsVisible = false;
            EditPannel.IsEnabled = false;            
        }




        #region Funciones

        private async void llenarCV()
        {
            var prod = await App.SQLiteDB.recuperarProductoXidAsync(Convert.ToInt32(txtIdProd.Text));
            if (prod != null)
            {
                DTImage.Source = prod.imageProd;
                lblNomProd.Text = prod.nomProducto;
                lblDescProd.Text = prod.descProducto;
                lblPCompraProd.Text = prod.preCompraProd.ToString();
                lblPVentaProd.Text = prod.preVentaProd.ToString();
                lblExisProd.Text = prod.existProd.ToString();
                lblCatProd.Text = prod.idCategoria;
                lblProvProd.Text = prod.idProveedor;
                lblNBcodProd.Text = prod.barCodeProd;
                lblEstadoProd.Text = prod.estadoProd;
            }
           
           
        }
        private async void llenarPICKS()
        {
            var cat = await App.SQLiteDB.recuperarCategoriaACTVAsync();
            if (cat != null)
            {
                foreach (var c in cat)
                {
                    pickCategoriaProd.Items.Add(c.nomCat);
                }
            }
            var prov = await App.SQLiteDB.recuperarProveedorACTVAsync();
            if (prov != null)
            {
                foreach (var p in prov)
                {
                    pickProveedorProd.Items.Add(p.nomProveedor + " " + p.apeProveedor + " @ " + p.idEmpresa);
                }
            }
        }

        private void limpiarEDitPannel()
        {
            txtNombreProd.Text = "";
            txtDescProd.Text = "";
            txtPrecioCompraProd.Text = "";
            txtPrecioVentaProd.Text = "";
            txtExistenciaProd.Text = "";
            pickCategoriaProd.SelectedItem = -1;
            pickProveedorProd.SelectedItem = -1;
            pickEstadoProducto.SelectedItem = -1;
            txtCodProd.Text = "";
            txtIndFoto.Text = "La foto se cargara atomaticamente despues de ser tomada, tenga paciencia.";
        }


        #endregion



        private async void btnEdit_Clicked(object sender, EventArgs e)
        {
            DetailPannel.IsVisible = false;
            EditPannel.IsVisible = true;
            EditPannel.IsEnabled = true;
            btnDelete.IsVisible = false;
            btnEdit.IsVisible = false;
            btnBack.IsVisible = false;
            if (!string.IsNullOrEmpty(txtIdProd.Text))
            {
                var prod = await App.SQLiteDB.recuperarProductoXidAsync(Convert.ToInt32(txtIdProd.Text));
                if (prod != null)
                {
                    txtNombreProd.Text = prod.nomProducto;
                    txtDescProd.Text = prod.descProducto;
                    txtPrecioCompraProd.Text = prod.preCompraProd.ToString();
                    txtPrecioVentaProd.Text = prod.preVentaProd.ToString();
                    txtExistenciaProd.Text = prod.existProd.ToString();
                    pickCategoriaProd.SelectedItem = prod.idCategoria;
                    pickProveedorProd.SelectedItem = prod.idProveedor;
                    txtCodProd.Text = prod.barCodeProd;
                    image.Source = prod.imageProd;
                    txtDirImagen.Text = prod.imageProd;
                    pickEstadoProducto.SelectedItem = prod.estadoProd;
                }
            }
        }

        private void btnDelete_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnBack_Clicked(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
            await this.Navigation.PopModalAsync();
        }



        private void btnNoFoto_Clicked(object sender, EventArgs e)
        {
            txtDirImagen.Text = "smile128.png";
            image.Source = txtDirImagen.Text;
            txtIndFoto.Text = "A seleccionado no tomar foto, se genera una imagen por defecto";
        }


        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdProd.Text))
            {
                var prod = await App.SQLiteDB.recuperarProductoXidAsync(Convert.ToInt32(txtIdProd.Text));
                if (prod!=null)
                {
                    producto Nprod = new producto
                    {
                        idProducto = Convert.ToInt32(txtIdProd.Text),
                        nomProducto = txtNombreProd.Text,
                        descProducto = txtDescProd.Text,
                        preCompraProd = Convert.ToDecimal(txtPrecioCompraProd.Text),
                        preVentaProd = Convert.ToDecimal(txtPrecioVentaProd.Text),
                        existProd = Convert.ToInt32(txtExistenciaProd.Text),
                        idCategoria = pickCategoriaProd.SelectedItem.ToString(),
                        idProveedor = pickProveedorProd.SelectedItem.ToString(),
                        barCodeProd = txtCodProd.Text,
                        imageProd = txtDirImagen.Text,
                        estadoProd=pickEstadoProducto.SelectedItem.ToString()
                    };
                    await App.SQLiteDB.editarProductoAsync(Nprod);
                    await DisplayAlert("OK","Producto editado","OK");
                    limpiarEDitPannel();
                    EditPannel.IsVisible = false;
                    DetailPannel.IsVisible=true;
                    await this.Navigation.PopModalAsync();
                }
            }
        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            DetailPannel.IsVisible = true;
            EditPannel.IsVisible = false;
            EditPannel.IsEnabled = false;
            btnDelete.IsVisible = true;
            btnEdit.IsVisible = true;
            btnBack.IsVisible = true;
            limpiarEDitPannel();
        }

        private async void btnFoto_Clicked(object sender, EventArgs e)
        {

            var cameraOptions = new StoreCameraMediaOptions();
            cameraOptions.SaveToAlbum = true;
            cameraOptions.DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front;
            cameraOptions.PhotoSize = PhotoSize.Small;
            cameraOptions.Name = txtNombreProd.Text + DateTime.Now.ToShortDateString();
            var foto = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(cameraOptions);
            if (foto != null)
            {
                string direccion = foto.Path.ToString();
                txtDirImagen.Text = direccion;
                image.Source = ImageSource.FromFile(direccion);
            }
            else
            {
                return;
            }
            txtIndFoto.Text = "La foto se cargara atomaticamente despues de ser tomada, tenga paciencia.";
        }



        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                txtCodProd.Text = result.Text;

            });

        }
    }
}