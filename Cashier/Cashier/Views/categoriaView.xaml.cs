using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cashier.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cashier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public  partial class categoria : ContentPage
    {
       
        public categoria()
        {
            InitializeComponent();
            llenarListVw();
            
        }
     
        public async void llenarListVw()
        {
            var catActvList = await App.SQLiteDB.recuperarCategoriaACTVAsync();
            var catDactvList= await App.SQLiteDB.recuperarCategoriaDactvAsync();
            if (catActvList != null)
            {

               collVWcatACTV.ItemsSource = catActvList;
               collVWcatDactv.ItemsSource= catDactvList;
            }
        }

        private async void btnCrear_Clicked(object sender, EventArgs e)
        {
            if (validar())
            {
                Models.categoria cat = new Models.categoria
                {
                    nomCat = txtNomCat.Text,
                    descCat = txtDesCat.Text,
                    estadoCat = PickEstadoCAt.SelectedItem.ToString()
                };
                await App.SQLiteDB.crearCategoriaAsync(cat); ;
                txtDesCat.Text = "";
                txtNomCat.Text = "";                
                await DisplayAlert("Correcto", "Datos Ingresados", "OK");
                llenarListVw();
            }
            else
            {
                await DisplayAlert("Advertiecia", "Debe ingresar los datos", "OK");
            }
        }

        public bool validar()
        {
            bool res;
            if (string.IsNullOrEmpty(txtNomCat.Text))
            {
                res = false;
            }
            if (string.IsNullOrEmpty(txtDesCat.Text))
            {
                res = false;
            }
            if (string.IsNullOrEmpty(PickEstadoCAt.SelectedItem.ToString()))
            {
                res = false;
            }
            else
            {
                res = true;
            }
            return res;
        }

        private async void btnEditar_Clicked(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtIdCat.Text))
            {
                Models.categoria categoria = new Models.categoria()
                {
                    Id = Convert.ToInt32(txtIdCat.Text),
                    nomCat=txtNomCat.Text,
                    descCat=txtDesCat.Text,
                    estadoCat=PickEstadoCAt.SelectedItem.ToString()
                };
                await App.SQLiteDB.editarCategoriaAsync(categoria);
                txtDesCat.Text = "";
                txtNomCat.Text = "";
                llenarListVw();

            }
        }

        private async void collVWcat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectecCat = e.CurrentSelection[0] as Models.categoria;
            btnCrear.IsVisible = false;
            btnEditar.IsVisible = true;
            if(selectecCat != null)
            {
                var categoria = await App.SQLiteDB.recuperarCategoriaXid(selectecCat.Id);
                if (categoria != null)
                {
                    txtIdCat.Text = selectecCat.Id.ToString();
                    txtNomCat.Text=selectecCat.nomCat;
                    txtDesCat.Text=selectecCat.descCat;
                    
                }
            }
            
        }

        private async void collVWcatDactv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectecCat = e.CurrentSelection[0] as Models.categoria;
            btnCrear.IsVisible = false;
            btnEditar.IsVisible = true;
            if (selectecCat != null)
            {
                var categoria = await App.SQLiteDB.recuperarCategoriaXid(selectecCat.Id);
                if (categoria != null)
                {
                    txtIdCat.Text = selectecCat.Id.ToString();
                    txtNomCat.Text = selectecCat.nomCat;
                    txtDesCat.Text = selectecCat.descCat;

                }
            }

        }
    }
}