using System;
using Android.App;
using Android.Content;
using Android.OS;
using MedTrack;

namespace MedTrack
{
    [Activity(Label = "CompletedRefillActivity")]
    public class CompletedRefillActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Android.Net.Uri url = Intent.Data;
            if(url!=null && url.Scheme.Equals("refillByScan"))
            {
                string action = url.GetQueryParameter("callBackAction");
                Intent intent_new = new Intent();
                if (action.Equals("back"))
                {
                    Console.WriteLine(string.Format("Inside handla back"));
                    StartActivity(typeof(PatientLoginActivity));
                }
                else if (action.Equals("cancel"))
                {

                }
                else if (action.Equals("close"))
                {

                }
                else if (action.Equals("fillAnother"))
                {

                }
                else if (action.Equals("refillTryAgain"))
                {

                }


            }   
            // Create your application here

            Intent mainActivityIntent = new Intent(this, typeof(MainActivity));
        StartActivity(mainActivityIntent);

        SetContentView(Resource.Layout.activity_completed_refill);
    }
    }
}