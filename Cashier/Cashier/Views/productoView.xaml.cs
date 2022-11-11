using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cashier.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.NetworkInformation;

namespace Cashier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class productoView : ContentPage
    {
       
        public productoView()
        {
            InitializeComponent();
            llenarPicks();
        }




        #region Validaciones
        private bool validarTXT()
        {
            bool res;
            if (string.IsNullOrEmpty(txtNombreProd.Text))
            {
                res = false;
            } else if (string.IsNullOrEmpty(txtDescProd.Text))
            {
                res = false;
            }
            else if (string.IsNullOrEmpty(txtPrecioCompraProd.Text))
            {
                res = false;
            } else if (string.IsNullOrEmpty(txtPrecioVentaProd.Text))
            {
                res = false;
            }
            else if (string.IsNullOrEmpty(txtExistenciaProd.Text))
            {
                res = false;
            } else if (string.IsNullOrEmpty(txtCodProd.Text))
            {
                res = false;
            }
            else if (string.IsNullOrEmpty(txtDirImagen.Text))
            {
                res = false;
            } else if (string.IsNullOrEmpty(pickCategoriaProd.SelectedItem.ToString()))
            {
                res = false;
            } else if (string.IsNullOrEmpty(pickEstadoProducto.SelectedItem.ToString()))
            {
                res = false;
            } else if (string.IsNullOrEmpty(pickProveedorProd.SelectedItem.ToString()))
            {
                res = false;
            }
            else
            {
                res = true;
            }
            return res;
        }

        private async void llenarPicks()
        {
            var cat = await App.SQLiteDB.recuperarCategoriaACTVAsync();
            var prov = await App.SQLiteDB.recuperarProveedorACTVAsync();
            if (cat != null)
            {
                foreach (var c in cat)
                {
                    pickCategoriaProd.Items.Add(c.nomCat);
                }

            }
            if (prov != null)
            {
                foreach (var p in prov)
                {
                    pickProveedorProd.Items.Add(p.nomProveedor + " " + p.apeProveedor + " @ " + p.idEmpresa);
                }
            }

        }


        private void vaciarTXT()
        {
            txtIdProd.Text = "";
            txtNombreProd.Text = "";
            txtDescProd.Text = "";
            txtPrecioCompraProd.Text = "";
            txtPrecioVentaProd.Text = "";
            txtExistenciaProd.Text = "";
            pickCategoriaProd.SelectedIndex = -1;
            pickProveedorProd.SelectedIndex = -1;
            txtCodProd.Text = "";
            txtDirImagen.Text = "";
            pickEstadoProducto.SelectedIndex = -1;
            txtIndFoto.Text = "La foto se cargara atomaticamente despues de ser tomada, tenga paciencia.";
            image.Source = null;
        }

        #endregion

        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                txtCodProd.Text = result.Text;

            });


        }

        private async void btnFoto_Clicked(object sender, EventArgs e)
        {
            try
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
            }
            catch (Exception)
            {

            }

        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            if (validarTXT())
            {
                producto prod = new producto
                {
                    nomProducto = txtNombreProd.Text,
                    descProducto = txtDescProd.Text,
                    preCompraProd = Convert.ToDecimal(txtPrecioCompraProd.Text),
                    preVentaProd = Convert.ToDecimal(txtPrecioVentaProd.Text),
                    existProd = Convert.ToInt32(txtExistenciaProd.Text),
                    idCategoria = pickCategoriaProd.SelectedItem.ToString(),
                    idProveedor = pickProveedorProd.SelectedItem.ToString(),
                    barCodeProd = txtCodProd.Text,
                    imageProd = txtDirImagen.Text,
                    estadoProd = pickEstadoProducto.SelectedItem.ToString()
                };
                await App.SQLiteDB.crearProductoAsync(prod);
                await DisplayAlert("OK!", "Producto creado", "OK");
                vaciarTXT();

            }
            else
            {
                await DisplayAlert("ERROR!", "Debe rellenar los campos", "OK");
            }

        }



        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            vaciarTXT();
            await App.masterDetail.Detail.Navigation.PopAsync();
        }

        private async void btnList_Clicked(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
            Navigation.RemovePage(this);
            await App.masterDetail.Detail.Navigation.PushAsync(new productoList());
           
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {

        }
    }
}