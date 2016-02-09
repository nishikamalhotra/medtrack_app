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
using MedTrack.Entity;
using MedTrack.Library;

namespace MedTrack
{
    [Activity(Label = "PhysicianRegister")]
    public class PhysicianRegisterActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.PhysicianRegister);
            // Create your application here
            EditText firstName = FindViewById<EditText>(Resource.Id.fNameText);
            EditText lastName = FindViewById<EditText>(Resource.Id.lNameText);
            EditText zipcode = FindViewById<EditText>(Resource.Id.zipText);
            EditText contactNumber = FindViewById<EditText>(Resource.Id.phoneText);
            EditText speciality = FindViewById<EditText>(Resource.Id.speciality);
            EditText yearsOfPractice = FindViewById<EditText>(Resource.Id.yearsText);

            Button registerPhysician = FindViewById<Button>(Resource.Id.registerPhysician);

            registerPhysician.Click += delegate
            {
                Physician physician = new Physician();
                physician.FirstName = firstName.Text.ToString();
                physician.LastName = lastName.Text.ToString();
                int zip, contact, year;
                int.TryParse(zipcode.Text.ToString(), out zip);
                physician.OfficeZipcode = zip;
                int.TryParse(contactNumber.Text.ToString(), out contact);
                physician.ContactNumber = contact;
                physician.Speciality = speciality.Text.ToString();
                int.TryParse(yearsOfPractice.Text.ToString(), out year);
                physician.YearsOfPractice = year;
                physician.PhysicianID = 1;

                PhysicianLibrary lib = new PhysicianLibrary();
                lib.AddPhysician(physician);


            };

        }
    }
}