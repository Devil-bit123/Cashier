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
            ventasHoy();
            ventasAyer();
            UltimaVenta();
        }
        protected override void OnAppearing()
        {
            //Write the code of your page here
            ventasHoy();
            ventasAyer();
            UltimaVenta();
            base.OnAppearing();
        }

        #region funciones
        private async void UltimaVenta()
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
                            lblUventa.Text = "$ "+ tot.ToString();                            
                        }
                    }

                }
            }
            else
            {
                lblUventa.Text = "$ 0";
            }
           
        }

        private async void ventasAyer()
        {
            decimal tot=0;
            string ayer = DateTime.Now.AddDays(-1).ToShortDateString();
            var Vayer = await App.SQLiteDB.recuperarFacturasPagadasxFechaAssync(ayer);
            if (Vayer!=null)
            {
                foreach (var v in Vayer)
                {
                    var carrito = await App.SQLiteDB.recuperarDetallesAsync(v.numFac);
                    if (carrito!=null)
                    {
                        foreach (var c in carrito)
                        {
                            tot+=c.total;
                            lblVayer.Text ="$ "+ tot.ToString();
                        }
                    }

                }
            }
            else
            {
                lblVayer.Text = "$ 0";
            }

       
        }



        private async void ventasHoy()
        {
            decimal tot = 0;
            string ayer = DateTime.Now.ToShortDateString();
            var Vayer = await App.SQLiteDB.recuperarFacturasPagadasxFechaAssync(ayer);
            if (Vayer != null)
            {
                foreach (var v in Vayer)
                {
                    var carrito = await App.SQLiteDB.recuperarDetallesAsync(v.numFac);
                    if (carrito != null)
                    {
                        foreach (var c in carrito)
                        {
                            tot += c.total;
                            lblVhoy.Text ="$ "+ tot.ToString();
                        }
                    }

                }
            }
            else
            {
               lblVhoy.Text = "$ 0";
            }


        }

        #endregion
    }
}