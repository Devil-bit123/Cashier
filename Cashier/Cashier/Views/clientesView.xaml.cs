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
    public partial class clientesView : ContentPage
    {
        public clientesView()
        {
            InitializeComponent();
            llenarCV();
        }

        protected async override void OnAppearing()
        {
            if ( CVclientesA.SelectedItem != null)
            {
                CVclientesA.SelectedItem = null;
            }

            if (CVclientesI.SelectedItem!=null)
            {
                CVclientesI.SelectedItem = null;
            }
            base.OnAppearing();

        }


        #region Validaciones
        private bool valiadrTXT()
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
            else if (string.IsNullOrEmpty(pickEstadoCliente.SelectedItem.ToString()))
            {
                res = false;
            }
            else
            {
                res = true;
            }
            return res;
        }

        private void vaciarTXT()
        {
            txtIdCliente.Text = "";
            txtNomCliente.Text = "";
            txtApeCliente.Text = "";
            txtCedCliente.Text = "";
            txtDirCliente.Text = "";
            txTelfCliente.Text = "";
            pickEstadoCliente.SelectedIndex = -1;
        }

        private async void llenarCV()
        {
            var cliA = await App.SQLiteDB.recuperarClienteACTVAsync();
            var cliI = await App.SQLiteDB.recuperarClienteINACTVsync();
            if (cliA!=null)
            {
                CVclientesA.ItemsSource = cliA;
            }
            if (cliI != null)
            {
                CVclientesI.ItemsSource = cliI;
            }
        }
            
        #endregion


        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            if (valiadrTXT())
            {
                cliente cli = new cliente
                {
                    nomCliente=txtNomCliente.Text,
                    apeCliente=txtApeCliente.Text,
                    cedCliente=txtCedCliente.Text,
                    dirCliente=txtDirCliente.Text,
                    telfCliente=txTelfCliente.Text,
                    estadoCliente=pickEstadoCliente.SelectedItem.ToString()
                };
                await App.SQLiteDB.crearClienteAsync(cli);
                await DisplayAlert("OK!", "Cliente creado.", "OK");
                vaciarTXT();
                llenarCV();

            }
            else
            {
                await DisplayAlert("ERROR!", "Debe llenar todos los campos", "OK");
            }

        }

        private async void btnEditar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdCliente.Text))
            {
                cliente cli = new cliente
                {
                    idCliente = Convert.ToInt32(txtIdCliente.Text),
                    nomCliente=txtNomCliente.Text,
                    apeCliente=txtApeCliente.Text,
                    cedCliente=txtCedCliente.Text,
                    dirCliente=txtDirCliente.Text,
                    telfCliente=txTelfCliente.Text,
                    estadoCliente=pickEstadoCliente.SelectedItem.ToString()
                };
                await App.SQLiteDB.editarClienteAsync(cli);
                await DisplayAlert("OK!", "Cliente editado", "OK");
                btnEditar.IsVisible = false;
                btnEliminar.IsVisible = false;
                btnGuardar.IsVisible = true;
                llenarCV();
                vaciarTXT();
            }

        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            vaciarTXT();
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var cli = await App.SQLiteDB.recuperarClienteXidAsync(Convert.ToInt32(txtIdCliente.Text));
            if(cli != null)
            {
                await App.SQLiteDB.eliminiarClienteAsync(cli);
                await DisplayAlert("OK!", "Cliente eliminado", "OK");
                btnEliminar.IsVisible = false;
                btnEditar.IsVisible = false;
                btnGuardar.IsVisible = true;
                vaciarTXT();
                llenarCV();
            }
        }

        private async void CVclientesA_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCli = e.CurrentSelection.FirstOrDefault() as cliente;
            btnGuardar.IsVisible = false;
            btnEditar.IsVisible = true;
            btnEliminar.IsVisible = true;
            if (!string.IsNullOrEmpty(selectedCli.idCliente.ToString()))
            {
                var cli = await App.SQLiteDB.recuperarClienteXidAsync(selectedCli.idCliente);
                if (cli!=null)
                {
                    txtIdCliente.Text = cli.idCliente.ToString();
                    txtNomCliente.Text = cli.nomCliente.ToString();
                    txtApeCliente.Text = cli.apeCliente.ToString();
                    txtCedCliente.Text = cli.cedCliente.ToString();
                    txtDirCliente.Text = cli.dirCliente.ToString();
                    txTelfCliente.Text = cli.telfCliente.ToString();
                    pickEstadoCliente.SelectedItem = cli.estadoCliente;
                }
            }

        }

        private async void CVclientesI_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCli = e.CurrentSelection.FirstOrDefault() as cliente;
            btnGuardar.IsVisible = false;
            btnEditar.IsVisible = true;
            btnEliminar.IsVisible = true;
            if (!string.IsNullOrEmpty(selectedCli.idCliente.ToString()))
            {
                var cli = await App.SQLiteDB.recuperarClienteXidAsync(selectedCli.idCliente);
                if (cli != null)
                {
                    txtIdCliente.Text = cli.idCliente.ToString();
                    txtNomCliente.Text = cli.nomCliente.ToString();
                    txtApeCliente.Text = cli.apeCliente.ToString();
                    txtCedCliente.Text = cli.cedCliente.ToString();
                    txtDirCliente.Text = cli.dirCliente.ToString();
                    txTelfCliente.Text = cli.telfCliente.ToString();
                    pickEstadoCliente.SelectedItem = cli.estadoCliente;
                }
            }
        }
    }
}