using Cashier.Models;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cashier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class proveedorView : ContentPage
    {
        public proveedorView()
        {
            InitializeComponent();
            llenarPickerEmp();
            llenarCV();
        }

        #region funcionalidad
        private async void llenarPickerEmp()
        {
            var emp = await App.SQLiteDB.recuperarEmpresaACTVAsync();
            foreach (var e in emp)
            {
                pickEmpresa.Items.Add(e.nomEmpresa.ToString());
            }


        }

        private async void llenarCV()
        {
            var provA = await App.SQLiteDB.recuperarProveedorACTVAsync();
            var provI= await App.SQLiteDB.recuperarProveedorDACTVAsync();
            if (provA != null)
            {
                CVProvAct.ItemsSource = provA;
            }
            if (provI != null)
            {
                CVProvInac.ItemsSource = provI;
            }
        }

        #endregion
        #region Validaciones
        private bool validarTxt()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtNombreProveedor.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtApellidoProveedor.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtCedProveedor.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtTelfProveedor.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(pickEmpresa.SelectedItem.ToString()))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(pickEstadoProveedor.SelectedItem.ToString()))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;

        }

        private void VaciarTXT()
        {
            txtIdProveedor.Text = "";
            txtNombreProveedor.Text = "";
            txtApellidoProveedor.Text = "";
            txtCedProveedor.Text = "";
            txtTelfProveedor.Text = "";
            pickEmpresa.SelectedIndex = -1;
            pickEstadoProveedor.SelectedIndex = -1;
        }
        #endregion
        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            if (validarTxt())
            {
                proveedor prov = new proveedor
                {
                    nomProveedor = txtNombreProveedor.Text,
                    apeProveedor = txtApellidoProveedor.Text,
                    cedProveedor = txtCedProveedor.Text,
                    telfProveedor=txtTelfProveedor.Text,
                    estadoProveedor = pickEstadoProveedor.SelectedItem.ToString(),
                    idEmpresa = pickEmpresa.SelectedItem.ToString()
                };
                await App.SQLiteDB.crearProveedorAsync(prov);
                await DisplayAlert("OK!","Proveedor creado","OK");
                llenarCV();
                
            }
            else
            {
                await DisplayAlert("ERROR!", "Debe llenar los campos", "OK");
            }
        }
       
        

        private async void btnEditar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdProveedor.Text))
            {
                proveedor prov = new proveedor
                {
                    idProveedor = Convert.ToInt32(txtIdProveedor.Text),
                    nomProveedor=txtNombreProveedor.Text,
                    apeProveedor=txtApellidoProveedor.Text,
                    cedProveedor=txtCedProveedor.Text,
                    telfProveedor=txtTelfProveedor.Text,
                    estadoProveedor=pickEstadoProveedor.SelectedItem.ToString(),
                    idEmpresa=pickEmpresa.SelectedItem.ToString()
                };
                await App.SQLiteDB.editarProveedoresAsync(prov);
                await DisplayAlert("OK!", "Proveedor editado", "OK");
                btnEditar.IsVisible = false;
                btnBorrar.IsVisible = false;
                btnGuardar.IsVisible = true;
                VaciarTXT();
                llenarCV();
            }
            else
            {
                await DisplayAlert("ERROR!", "No se pudo editar proveedor", "OK");
            }
        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            VaciarTXT();
        }

        private async void CVProvAct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedProveedor = e.CurrentSelection.FirstOrDefault() as proveedor;
            btnGuardar.IsVisible = false;
            btnEditar.IsVisible = true;
            btnBorrar.IsVisible = true;
            if (!string.IsNullOrEmpty(selectedProveedor.idProveedor.ToString()))
            {
                var proveedor = await App.SQLiteDB.recuperarProveedorXidAsync(selectedProveedor.idProveedor);
                if(proveedor != null)
                {
                    txtIdProveedor.Text=proveedor.idProveedor.ToString();
                    txtNombreProveedor.Text = proveedor.nomProveedor.ToString();
                    txtApellidoProveedor.Text = proveedor.apeProveedor.ToString();
                    txtCedProveedor.Text = proveedor.cedProveedor.ToString();
                    txtTelfProveedor.Text = proveedor.telfProveedor.ToString();
                    pickEmpresa.SelectedItem = proveedor.idEmpresa;
                    pickEstadoProveedor.SelectedItem = proveedor.estadoProveedor;
                }

            }
        }

        private async void btnBorrar_Clicked(object sender, EventArgs e)
        {
            var prov = await App.SQLiteDB.recuperarProveedorXidAsync(Convert.ToInt32(txtIdProveedor.Text));
            if (prov!=null)
            {
                await App.SQLiteDB.eliminiarProveedorAsync(prov);
            }            
            await DisplayAlert("OK!", "Proveedor eliminado", "OK");
            btnEditar.IsVisible = false;
            btnBorrar.IsVisible = false;
            btnGuardar.IsVisible = true;
            VaciarTXT();
            llenarCV();
        }

        private async void CVProvInac_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedProveedor = e.CurrentSelection.FirstOrDefault() as proveedor;
            btnGuardar.IsVisible = false;
            btnEditar.IsVisible = true;
            btnBorrar.IsVisible = true;
            if (!string.IsNullOrEmpty(selectedProveedor.idProveedor.ToString()))
            {
                var proveedor = await App.SQLiteDB.recuperarProveedorXidAsync(selectedProveedor.idProveedor);
                if (proveedor != null)
                {
                    txtIdProveedor.Text = proveedor.idProveedor.ToString();
                    txtNombreProveedor.Text = proveedor.nomProveedor.ToString();
                    txtApellidoProveedor.Text = proveedor.apeProveedor.ToString();
                    txtCedProveedor.Text = proveedor.cedProveedor.ToString();
                    txtTelfProveedor.Text = proveedor.telfProveedor.ToString();
                    pickEmpresa.SelectedItem = proveedor.idEmpresa;
                    pickEstadoProveedor.SelectedItem = proveedor.estadoProveedor;
                }

            }

        }
    }
}