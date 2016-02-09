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
    [Activity(Label = "NewMemberRegisterActivity")]
    public class NewMemberRegisterActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.NewMemberRegister);

            // Get our button from the layout resource,
            // and attach an event to it
            Button patientButton = FindViewById<Button>(Resource.Id.PatientLogin);
            Button physicianButton = FindViewById<Button>(Resource.Id.PhysicianLogin);
            Button pharmacyButton = FindViewById<Button>(Resource.Id.PharmacyLogin);

            patientButton.Click += delegate
            {
                StartActivity(typeof(PatientRegisterActivity));
            };

            physicianButton.Click += delegate
            {
                StartActivity(typeof(PhysicianRegisterActivity));
            };
            pharmacyButton.Click += delegate
            {
                StartActivity(typeof(PharmacyRegisterActivity));
            };
        }
    }
}