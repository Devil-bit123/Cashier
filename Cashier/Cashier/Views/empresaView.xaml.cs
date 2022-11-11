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
    public partial class empresaView : ContentPage
    {
        public empresaView()
        {
            InitializeComponent();
            llenarCV();
            
        }
        #region Validaciones

        private bool validarText()
        {
            bool respuesta;

            if (string.IsNullOrEmpty(txtNombreEmpresa.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtDireccionEmpresa.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtTelfEmpresa.Text))
            {
                respuesta = false;
            }
            if (string.IsNullOrEmpty(pickEstadoEmpresa.SelectedItem.ToString()))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }

        private void vaciarTexto()
        {
            txtNombreEmpresa.Text = "";
            txtDireccionEmpresa.Text = "";
            txtTelfEmpresa.Text = "";
            pickEstadoEmpresa.SelectedIndex = -1;
        }

        private async void llenarCV() { 
        
            var empA = await App.SQLiteDB.recuperarEmpresaACTVAsync();
            var empI = await App.SQLiteDB.recuperarEmpresaDACTVAsync();
            if (empA!=null)
            {
                CVEmpAct.ItemsSource = empA; 
            }
            if (empI != null)
            {
                CVEmpIact.ItemsSource = empI;
            }
        }

        #endregion

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
           
            
                if (validarText())
                {
                    empresa empresa = new empresa
                    {
                        nomEmpresa = txtNombreEmpresa.Text,
                        dirEmpresa = txtDireccionEmpresa.Text,
                        teflEmpresa=txtTelfEmpresa.Text,
                        estadoEmpresa=pickEstadoEmpresa.SelectedItem.ToString(),
                    };
                    await App.SQLiteDB.crearEmpresaAsync(empresa);
                    await DisplayAlert("OK!", "Empresa creada", "OK");
                    llenarCV();
                    vaciarTexto();

                }
                else
                {
                    await DisplayAlert("ERROR!", "Debe llenar todos los datos de la empresa", "OK");
                }

            
        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            vaciarTexto();
        }

        private async void CVEmpAct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedEmpresa=e.CurrentSelection.FirstOrDefault() as empresa;
            btnGuardar.IsVisible = false;
            btnEditar.IsVisible = true;
            btnEliminar.IsVisible = true;
            if (!string.IsNullOrEmpty(selectedEmpresa.idEmpresa.ToString())) 
            {
                var empresa = await App.SQLiteDB.recuperarEmpresaXid(selectedEmpresa.idEmpresa);
                if (empresa != null)
                {
                    txtIdEmpresa.Text = empresa.idEmpresa.ToString();
                    txtNombreEmpresa.Text = empresa.nomEmpresa.ToString();
                    txtDireccionEmpresa.Text = empresa.dirEmpresa.ToString();
                    txtTelfEmpresa.Text = empresa.teflEmpresa.ToString();
                    pickEstadoEmpresa.SelectedItem = empresa.estadoEmpresa;
                }
            }


        }

        private async void btnEditar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdEmpresa.Text))
            {
                empresa Objempresa = new empresa()
                {
                    idEmpresa = Convert.ToInt32(txtIdEmpresa.Text),
                    nomEmpresa = txtNombreEmpresa.Text,
                    dirEmpresa = txtDireccionEmpresa.Text,
                    teflEmpresa = txtTelfEmpresa.Text,
                    estadoEmpresa = pickEstadoEmpresa.SelectedItem.ToString()
                };
                
                    await App.SQLiteDB.editarEmpresaAsync(Objempresa);
                    await DisplayAlert("OK!","Empresa editada","OK");
                    btnEditar.IsVisible = false;
                    btnGuardar.IsVisible = true;
                    llenarCV();
                    vaciarTexto();                    

            }
        }

        private async void CVEmpIact_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedEmpresa = e.CurrentSelection.FirstOrDefault() as empresa;
            btnGuardar.IsVisible = false;
            btnEditar.IsVisible = true;
            if (!string.IsNullOrEmpty(selectedEmpresa.idEmpresa.ToString()))
            {
                var empresa = await App.SQLiteDB.recuperarEmpresaXid(selectedEmpresa.idEmpresa);
                if (empresa != null)
                {
                    txtIdEmpresa.Text = empresa.idEmpresa.ToString();
                    txtNombreEmpresa.Text = empresa.nomEmpresa.ToString();
                    txtDireccionEmpresa.Text = empresa.dirEmpresa.ToString();
                    txtTelfEmpresa.Text = empresa.teflEmpresa.ToString();
                    pickEstadoEmpresa.SelectedItem = empresa.estadoEmpresa;
                }
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var emp = await App.SQLiteDB.recuperarEmpresaXid(Convert.ToInt32(txtIdEmpresa.Text));
            if (emp != null)
            {
                await App.SQLiteDB.eliminiarEmpresaAsync(emp);
            }
            await DisplayAlert("OK!", "Empresa eliminada", "OK");
            btnEditar.IsVisible = false;
            btnEliminar.IsVisible = false;
            btnGuardar.IsVisible = true;
            llenarCV();
            vaciarTexto();
        }
    }
}