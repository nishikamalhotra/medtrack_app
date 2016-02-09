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

namespace MedTrack
{
    [Activity(Label = "AddPrescriptionActivity")]
    public class AddPrescriptionActivity : Activity
    {
        private long barcode;
        private string code;
        MobileBarcodeScanner scanner;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Initialize the scanner first so we can track the current context
            MobileBarcodeScanner.Initialize(Application);
            scanner = new MobileBarcodeScanner();

            SetContentView(Resource.Layout.Patient);
            // Get button clicks and user inputs from the layout resource,
            // and attach an event to it
            EditText patientID = FindViewById<EditText>(Resource.Id.patientIdText);
            EditText startDate = FindViewById<EditText>(Resource.Id.startDate);
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
                prescription.Barcode = barcode;
                int.TryParse(numberofDays.Text.ToString(), out numOfDay);
                prescription.NumberOfDays = numOfDay;
                int.TryParse(numberofTimes.Text.ToString(), out numOfTime);
                prescription.NumberOfTime = numOfTime;
                prescription.StartDate = DateTime.Parse(startDate.Text);
                prescription.PrescribedBy = physician.Text.ToString();

                PrescriptionLibrary lib = new PrescriptionLibrary();
                lib.AddPrescription(prescription);


                // MedicineLibrary medLibrary = new MedicineLibrary();
                //int.TryParse(result.ToString(), out barcode);

                // medLibrary.FindMedicineWithBarcode(barcode);

                
            };         
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