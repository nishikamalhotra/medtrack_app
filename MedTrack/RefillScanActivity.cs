using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using ZXing.Mobile;
using MedTrack.Library;
using System.Threading.Tasks;

namespace MedTrack
{
    [Activity(Label = "RefillScanActivity")]
    public class RefillScanActivity : Activity
    {
        MobileBarcodeScanner scanner;
        private long barcode;
        private string code;
        private int patientID;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            MobileBarcodeScanner.Initialize(Application);
            scanner = new MobileBarcodeScanner();

            SetContentView(Resource.Layout.RefillScanLayout);

            EditText id = FindViewById<EditText>(Resource.Id.patientIDNumber);
            Button scan = FindViewById<Button>(Resource.Id.scanAndAdd);
           
            int.TryParse(id.Text.ToString(), out patientID);

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

                PrescriptionLibrary lib = new PrescriptionLibrary();
                try
                {
                    Task<string> rx = lib.FindRxByBarcode(barcode, patientID);
                    string rxNumber = await rx;

                    //start new activity passing the rxNumber to the new activity intent
                    Context context = Application.ApplicationContext;
                    var intentNew = new Intent(context, typeof(WebViewActivity));
                    intentNew.PutExtra("rxNumber", rxNumber);
                    intentNew.AddFlags(ActivityFlags.NewTask);
                    context.StartActivity(intentNew);                    
                }
                catch
                {

                }
            };
        }


        protected void HandleScanResult(ZXing.Result result)
        {
            string msg = "";

            if (!(result.Equals(null)) && (!string.IsNullOrEmpty(result.Text)))
            {
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