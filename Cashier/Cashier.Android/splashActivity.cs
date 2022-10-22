using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cashier.Droid
{
    
    [Activity(Label = "Cashier", Icon = "@mipmap/icon",
        Theme = "@style/splashTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize )]
    public class splashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            string c = "#fb8c00";
            Window.SetStatusBarColor(Android.Graphics.Color.ParseColor(c));
            // Create your application here
        }
    }
}