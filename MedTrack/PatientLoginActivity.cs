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
    [Activity(Label = "PatientLoginActivity")]
    public class PatientLoginActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.PatientLogin);
            Button addPrescription = FindViewById<Button>(Resource.Id.addPrescription);
            Button refillPrescription = FindViewById<Button>(Resource.Id.refillPrescription);

            addPrescription.Click += delegate
            {
                StartActivity(typeof(AddPrescriptionActivity));
            };
            refillPrescription.Click += delegate
            {
                StartActivity(typeof(RefillScanActivity));
            };
        }
    }
}