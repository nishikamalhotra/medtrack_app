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
using System.Threading.Tasks;

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

            registerButton.Click += async delegate
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
                try
                {
                    Task<int> patientId = patientLibrary.AddPatient(patient);
                    int id = await patientId;

                    //set alert for executing the task
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    // Create empty event handlers, we will override them manually instead of letting the builder handling the clicks.
                    alert.SetPositiveButton("Okay", (EventHandler<DialogClickEventArgs>)null);
                    // alert.SetNegativeButton("Cancel", (EventHandler<DialogClickEventArgs>)null);
                    AlertDialog alertDialog = alert.Create();
                    alertDialog.SetTitle("Registration Successfull");
                    alertDialog.SetIcon(Android.Resource.Drawable.IcDialogAlert);
                    alertDialog.SetMessage("Patient registered successfully. Your Patient ID is " + id.ToString());
                    alertDialog.Show();
                    // Get the buttons.
                    var okButton = alertDialog.GetButton((int)DialogButtonType.Positive);


                    // Assign our handlers.
                    okButton.Click += (sender, args) =>
                    {
                        StartActivity(typeof(PatientLoginActivity));
                    };

                }
                catch (Java.Lang.Exception e)
                {
                    //set alert for executing the task
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    // Create empty event handlers, we will override them manually instead of letting the builder handling the clicks.
                    alert.SetPositiveButton("Okay", (EventHandler<DialogClickEventArgs>)null);
                    alert.SetNegativeButton("Cancel", (EventHandler<DialogClickEventArgs>)null);
                    AlertDialog alertDialog = alert.Create();
                    alertDialog.SetTitle("Registration Error!");
                    alertDialog.SetIcon(Android.Resource.Drawable.IcDialogAlert);
                    alertDialog.SetMessage("Patient not registered. Please try again.");
                    alertDialog.Show();
                    // Get the buttons.
                    var okButton = alertDialog.GetButton((int)DialogButtonType.Positive);
                    var cancelButton = alertDialog.GetButton((int)DialogButtonType.Negative);

                    // Assign our handlers.
                    okButton.Click += (sender, args) =>
                    {
                        StartActivity(typeof(PatientRegisterActivity));
                    };
                    cancelButton.Click += (sender, args) =>
                    {
                        StartActivity(typeof(NewMemberRegisterActivity));
                    };
                }



            };

        }
    }
}