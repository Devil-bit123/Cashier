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
    public partial class categoriaView : ContentPage
    {
        public categoriaView()
        {
            InitializeComponent();
            llenarCV();
        }

        protected async override void OnAppearing()
        {
            if (CVcategoriasA.SelectedItem!=null)
            {
                CVcategoriasA.SelectedItem = null;
            }

            if (CVcategoriasI.SelectedItem!=null)
            {
                CVcategoriasI.SelectedItem = null;
            }            
            base.OnAppearing();

        }

        #region Validaciones
        private bool ValidarTXT()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtNomCat.Text))
            {
                respuesta = false;
            }else if (string.IsNullOrEmpty(txtDesCat.Text))
            {
                respuesta = false;
            }else if (string.IsNullOrEmpty(pickEstadoCategoria.SelectedItem.ToString()))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }

        private void vaciarTXT()
        {
            txtIdCat.Text = "";
            txtNomCat.Text = "";
            txtDesCat.Text = "";
            pickEstadoCategoria.SelectedIndex = -1;            
        }

        private async void llenarCV()
        {
            var catA = await App.SQLiteDB.recuperarCategoriaACTVAsync();
            var catI = await App.SQLiteDB.recuperarCategoriaDactvAsync();
            if (catA!=null)
            {
                CVcategoriasA.ItemsSource = catA;
            }
            if (catI!=null)
            {
                CVcategoriasI.ItemsSource = catI;
            }

        }

        #endregion



        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            if (ValidarTXT())
            {
                categoria cat = new categoria
                {
                    nomCat = txtNomCat.Text,
                    descCat=txtDesCat.Text,
                    estadoCat=pickEstadoCategoria.SelectedItem.ToString()
                };
                await App.SQLiteDB.crearCategoriaAsync(cat);
                await DisplayAlert("OK!","Categoria creada","OK");
                llenarCV();
                vaciarTXT();
                
            }

        }

        private async void btnEditar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdCat.Text))
            {
                categoria objCat = new categoria
                {
                    Id = Convert.ToInt32(txtIdCat.Text),
                    nomCat=txtNomCat.Text,
                    descCat=txtDesCat.Text,
                    estadoCat=pickEstadoCategoria.SelectedItem.ToString()
                };
                await App.SQLiteDB.editarCategoriaAsync(objCat);
                await DisplayAlert("OK!", "Categoria editada", "OK");
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

        private async void CVcategoriasA_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCategoria = e.CurrentSelection.FirstOrDefault() as categoria;
            btnGuardar.IsVisible = false;
            btnEditar.IsVisible = true;
            btnEliminar.IsVisible = true;
            if (!string.IsNullOrEmpty(selectedCategoria.Id.ToString()))
            {
                var cat = await App.SQLiteDB.recuperarCategoriaXid(selectedCategoria.Id);
                if (cat!=null)
                {
                    txtIdCat.Text = cat.Id.ToString();
                    txtNomCat.Text = cat.nomCat.ToString();
                    txtDesCat.Text = cat.descCat.ToString();
                    pickEstadoCategoria.SelectedItem = cat.estadoCat;
                }
            }

        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var cat = await App.SQLiteDB.recuperarCategoriaXid(Convert.ToInt32(txtIdCat.Text));
            if (cat != null)
            {
                await App.SQLiteDB.eliminiarCategoriaAsync(cat);
                await DisplayAlert("OK!", "Categoria eliminada", "OK");
                btnEliminar.IsVisible = false;
                btnEditar.IsVisible = false;
                btnGuardar.IsVisible = true;
                vaciarTXT();
                llenarCV();
            }
        }

        private async void CVcategoriasI_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCategoria = e.CurrentSelection.FirstOrDefault() as categoria;
            btnGuardar.IsVisible = false;
            btnEditar.IsVisible = true;
            btnEliminar.IsVisible = true;
            if (!string.IsNullOrEmpty(selectedCategoria.Id.ToString()))
            {
                var cat = await App.SQLiteDB.recuperarCategoriaXid(selectedCategoria.Id);
                if (cat != null)
                {
                    txtIdCat.Text = cat.Id.ToString();
                    txtNomCat.Text = cat.nomCat.ToString();
                    txtDesCat.Text = cat.descCat.ToString();
                    pickEstadoCategoria.SelectedItem = cat.estadoCat;
                }
            }

        }
    }
}