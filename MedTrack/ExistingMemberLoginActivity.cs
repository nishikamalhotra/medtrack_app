using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MedTrack
{
    [Activity(Label = "ExistingMemberLoginActivity")]
    public class ExistingMemberLoginActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ExistingMemberLogin);

            // Get button clicks and user inputs from the layout resource,
            // and attach an event to it
            EditText loginID = FindViewById<EditText>(Resource.Id.loginID);
            EditText password = FindViewById<EditText>(Resource.Id.password);
            Button loginButton = FindViewById<Button>(Resource.Id.loginButton);
           
            loginButton.Click += delegate
            {
                StartActivity(typeof(AddPrescriptionActivity));
            };

        }
    }
}