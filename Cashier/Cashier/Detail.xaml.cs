using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cashier
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Detail : ContentPage
    {
        public Detail()
        {
            InitializeComponent();
            Ventas();
        }
        protected override void OnAppearing()
        {
            //Write the code of your page here
            Ventas();
            base.OnAppearing();
        }

        #region funciones
        private async void Ventas()
        {
            decimal tot = 0;
            var todosEnc = await App.SQLiteDB.recuperarFacturasPagadaAssync();
            if (todosEnc!=null)
            {
                var UltEnc = todosEnc.LastOrDefault();
                if (UltEnc!=null)
                {
                    var carrito = await App.SQLiteDB.recuperarDetallesAsync(UltEnc.numFac);
                    if (carrito!=null)
                    {
                        foreach (var c in carrito)
                        {
                            tot = tot + c.total;
                            lblUventa.Text = "$ "+tot.ToString();                            
                        }
                    }

                }
            }
            else
            {
                lblUventa.Text = "No se pudo cargar venta";
            }
           
        }

        #endregion
    }
}