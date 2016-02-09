using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Walgreen
{
    [Activity(Label = "Walgreen", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public string EXTRA_MESSAGE = "Walgreen.MESSAGE";
        public string EXTRA_LATITUDE = "Walgreen.LATITUDE";
        public string EXTRA_LONGITUDE = "Walgreen.LONGITUDE";

        private int MY_PERMISSION_ACCESS_COARSE_LOCATION = 1234;
        private string TAG = "MainActivity";
       // public LocationService locationService = null;
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
        }
    }
}

