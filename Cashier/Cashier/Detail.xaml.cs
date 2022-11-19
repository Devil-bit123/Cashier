using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        float a ;
        float h ;
        
       

        public Detail()
        {
            InitializeComponent();
            ventasHoy();
            ventasAyer();
            UltimaVenta();
            if (!string.IsNullOrEmpty(lblVayer.Text) && !string.IsNullOrEmpty(lblVhoy.Text))
            {
                btnAnalisis.IsVisible = true;
                btnAnalisis.IsEnabled = true;
            }
            else
            {
                btnAnalisis.IsVisible = false;
                btnAnalisis.IsEnabled = false;
            }
            
        }

        protected override async void OnAppearing()
        {
            //Write the code of your page here
            ventasHoy();
            ventasAyer();
            UltimaVenta();
            if (!string.IsNullOrEmpty(lblVayer.Text) && !string.IsNullOrEmpty(lblVhoy.Text))
            {
                btnAnalisis.IsVisible = true;
                btnAnalisis.IsEnabled = true;
            }
            else
            {
                btnAnalisis.IsVisible = false;
                btnAnalisis.IsEnabled = false;
            }
            base.OnAppearing();
        }

        #region funciones
        private async void UltimaVenta()
        {
            decimal tot = 0;
            var todosEnc = await App.SQLiteDB.recuperarFacturasPagadaAssync();
            if (todosEnc != null)
            {
                var UltEnc = todosEnc.LastOrDefault();
                if (UltEnc != null)
                {
                    var carrito = await App.SQLiteDB.recuperarDetallesAsync(UltEnc.numFac);
                    if (carrito != null)
                    {
                        foreach (var c in carrito)
                        {
                            tot = tot + c.total;
                            lblUventa.Text = tot.ToString();
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
            decimal tot = 0;
            string ayer = DateTime.Now.AddDays(-1).ToShortDateString();
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
                            lblVayer.Text = tot.ToString();
                            
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
                            lblVhoy.Text =tot.ToString();
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

       

        private void btnAnalisis_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblVayer.Text) && !string.IsNullOrEmpty(lblVhoy.Text))
            {
                PruebaA.Text=lblVayer.Text;
                PruebaH.Text = lblVhoy.Text;
                ChartPannel.IsVisible = true;
               
                a = float.Parse(PruebaA.Text);
                h = float.Parse(PruebaH.Text);
                var entries = new[]
                {
                    new ChartEntry(a)
                    {
                        Label="V. Ayer",
                        Color=SKColor.Parse("#c25e00"),
                        ValueLabel="$ "+PruebaA.Text,
                    },
                    new ChartEntry(h)
                    {
                          Label="V. Hoy",
                        Color=SKColor.Parse("#ffbd45"),
                        ValueLabel="$ "+PruebaH.Text,
                    }
                };
                BarChart.Chart = new BarChart { Entries=entries, ValueLabelOrientation=Orientation.Horizontal,LabelTextSize=30, LabelOrientation=Orientation.Horizontal, MaxValue=100};

                
            }
        }

        private void btnClose_Clicked(object sender, EventArgs e)
        {
            ChartPannel.IsVisible = false;
        }
    }
}