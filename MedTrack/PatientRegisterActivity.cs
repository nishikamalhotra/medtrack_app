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
using Java.Lang;

namespace MedTrack
{
    [Activity(Label = "PatientLoginActivity")]
    public class PatientRegisterActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.PatientRegister);

            // Create your application here
            EditText firstName = FindViewById<EditText>(Resource.Id.firstNameText);
            EditText lastName = FindViewById<EditText>(Resource.Id.lastNameText);
            EditText address = FindViewById<EditText>(Resource.Id.addressText);
            EditText zipcode = FindViewById<EditText>(Resource.Id.zipcodeText);
            EditText contactNumber = FindViewById<EditText>(Resource.Id.contactText);
            EditText emergencyContact = FindViewById<EditText>(Resource.Id.emergencyContactText);
            EditText dob = FindViewById<EditText>(Resource.Id.dobText);
            EditText ageInYears = FindViewById<EditText>(Resource.Id.ageText);
            EditText gender = FindViewById<EditText>(Resource.Id.genderText);
            EditText insuranceID = FindViewById<EditText>(Resource.Id.insuranceIDText);
            EditText insuranceProvider = FindViewById<EditText>(Resource.Id.insuranceProviderText);
            EditText pastAilments = FindViewById<EditText>(Resource.Id.pastAilmentsText);
            EditText disability = FindViewById<EditText>(Resource.Id.disabilityText);
            Button registerButton = FindViewById<Button>(Resource.Id.register);

            registerButton.Click += delegate
            {
                Patient patient = new Patient();
                patient.FirstName = firstName.Text.ToString();
                patient.LastName = lastName.Text.ToString();
                patient.Address = address.Text.ToString();
                int zip, contact, emergencyNumber, DOB, age, insurance;
                int.TryParse(zipcode.Text.ToString(), out zip);
                patient.Zipcode = zip;
                int.TryParse(contactNumber.Text.ToString(), out contact);
                patient.ContactNumber = contact;
                int.TryParse(emergencyContact.Text.ToString(), out emergencyNumber);
                patient.EmergencyContact = emergencyNumber;
                int.TryParse(dob.Text.ToString(), out DOB);
                patient.DateOfBirth = DOB;
                int.TryParse(ageInYears.Text.ToString(), out age);
                patient.Age = age;
                patient.Gender = Char.Parse(gender.Text.ToString());
                int.TryParse(insuranceID.Text.ToString(), out insurance);
                patient.InsuranceID = insurance;
                patient.InsuranceProvider = insuranceProvider.Text.ToString();
                patient.PastAilments = pastAilments.Text.ToString();
                patient.Disability = disability.Text.ToString();

                patient.PatientID = 1;

                PatientLibrary patientLibrary = new PatientLibrary();
                patientLibrary.AddPatient(patient);

                //Location location = new Location();
                //location.AddressLine1 = address.Text.ToString();
                //ToString();
                //location.Zipcode = 1111;
                //location.LocationID = 1;

                //var locationLibrary = new LocationLibrary();

                //locationLibrary.AddLocation(location);
            };

        }
    }
}