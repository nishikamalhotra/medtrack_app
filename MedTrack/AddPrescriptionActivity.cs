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
using ZXing.Mobile;
using MedTrack.Library;
using MedTrack.Entity;
using Android.Support.V4.App;
using Java.Util;

namespace MedTrack
{
    [Activity(Label = "AddPrescriptionActivity")]
    public class AddPrescriptionActivity : Activity
    {
        private long barcode;
        private string code;
        MobileBarcodeScanner scanner;
        private DatePicker datePickerValue;
        private string date;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Initialize the scanner first so we can track the current context
            MobileBarcodeScanner.Initialize(Application);
            scanner = new MobileBarcodeScanner();

            SetContentView(Resource.Layout.Patient);
            // Get button clicks and user inputs from the layout resource,
            // and attach an event to it
            EditText patientID = FindViewById<EditText>(Resource.Id.patientIDNumber);
            EditText rxNumber = FindViewById<EditText>(Resource.Id.rxNumber);
            EditText numberofDays = FindViewById<EditText>(Resource.Id.numOfDayText);
            EditText numberofTimes = FindViewById<EditText>(Resource.Id.numOfTimesText);
            EditText physician = FindViewById<EditText>(Resource.Id.physicianName);
            Button scan = FindViewById<Button>(Resource.Id.scanAndAdd);         

            scan.Click += async delegate
            {
                //Tell our scanner to use the default overlay
                scanner.UseCustomOverlay = false;

                //We can customize the top and bottom text of the default overlay
                scanner.TopText = "Hold the camera up to the barcode\nAbout 6 inches away";
                scanner.BottomText = "Wait for the barcode to automatically scan!";

                //Start scanning
                var results = await scanner.Scan();

                HandleScanResult(results);

                //add prescription to db
                Prescription prescription = new Prescription();
                int patient, numOfTime, numOfDay;
                int.TryParse(patientID.Text.ToString(), out patient);
                prescription.PatientID = patient;
                prescription.rxNumber = rxNumber.Text.ToString();

                prescription.Barcode = barcode;
                int.TryParse(numberofDays.Text.ToString(), out numOfDay);
                prescription.NumberOfDays = numOfDay;
                int.TryParse(numberofTimes.Text.ToString(), out numOfTime);
                prescription.NumberOfTime = numOfTime;
                setCurrentDateOnView();
                prescription.StartDate = date;
                prescription.PrescribedBy = physician.Text.ToString();

                PrescriptionLibrary lib = new PrescriptionLibrary();
                try
                {
                    lib.AddPrescription(prescription);
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    // Create empty event handlers, we will override them manually instead of letting the builder handling the clicks.
                    alert.SetPositiveButton("Okay", (EventHandler<DialogClickEventArgs>)null);
                    AlertDialog alertDialog = alert.Create();
                    alertDialog.SetTitle("Success");
                    alertDialog.SetIcon(Android.Resource.Drawable.IcDialogAlert);
                    alertDialog.SetMessage("Prescription added successfully.");
                    alertDialog.Show();
                    // Get the buttons.
                    var okButton = alertDialog.GetButton((int)DialogButtonType.Positive);
                    // Assign our handlers.
                    okButton.Click += (sender, args) =>
                    {
                        StartActivity(typeof(PatientLoginActivity));
                    };
                } catch(Exception e)
                {
                    //set alert for executing the task
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    // Create empty event handlers, we will override them manually instead of letting the builder handling the clicks.
                    alert.SetPositiveButton("Okay", (EventHandler<DialogClickEventArgs>)null);
                    alert.SetNegativeButton("Cancel", (EventHandler<DialogClickEventArgs>)null);
                    AlertDialog alertDialog = alert.Create();
                    alertDialog.SetTitle("Error");
                    alertDialog.SetIcon(Android.Resource.Drawable.IcDialogAlert);
                    alertDialog.SetMessage("Error Adding the Prescription. Please try again");
                    alertDialog.Show();
                    // Get the buttons.
                    var okButton = alertDialog.GetButton((int)DialogButtonType.Positive);
                    var cancelButton = alertDialog.GetButton((int)DialogButtonType.Negative);

                    // Assign our handlers.
                    okButton.Click += (sender, args) =>
                    {
                        StartActivity(typeof(AddPrescriptionActivity));
                    };
                    cancelButton.Click += (sender, args) =>
                    {
                        StartActivity(typeof(PatientLoginActivity));
                    };

                }
                

                // add code to display a succes message box or error if adding prescription fails.

                
            };         
        }

        // display current date
        public void setCurrentDateOnView()
        {
            datePickerValue = FindViewById<DatePicker>(Resource.Id.datePicker1);
            int year = datePickerValue.Year;
            int day = datePickerValue.DayOfMonth;
            int month = datePickerValue.Month + 1;
            string m, d;
            if(day.ToString().Length == 1)
            {
                d = "0" + day.ToString();
            } else
            {
                d = day.ToString();
            }
            if(month.ToString().Length == 1)
            {
                m = "0" + month.ToString(); 
            } else
            {
                m = month.ToString();
            }
             
            
            date = m + d + year.ToString();
        }


        void HandleScanResult(ZXing.Result result)
        {
            string msg = "";

            if (!(result.Equals(null)) && (!string.IsNullOrEmpty(result.Text))) { 
                msg = "Found Barcode: " + result.Text;
                code = result.Text;

                barcode = Convert.ToInt64(code);
            }
            else
                msg = "Scanning Canceled!";

            this.RunOnUiThread(() => Toast.MakeText(this, msg, ToastLength.Short).Show());
        }
    }
}