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
using Org.Json;

namespace MedTrack
{
    [Activity(Label = "WebViewActivity")]
    public class WebViewActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // build post data for http request
            JSONObject postDataJSON = new JSONObject();
            try
            {
                postDataJSON.Put("apiKey", GetString(1));
                postDataJSON.Put("affId", GetString(2)); //don't have an affId yet
                postDataJSON.Put("transaction", "refillByScan");
                postDataJSON.Put("act", "mweb5Url");
                postDataJSON.Put("view", "mweb5UrlJSON");
                postDataJSON.Put("devinf", "Android,2.3.3");
                postDataJSON.Put("appver", "3.1");
            }
            catch (JSONException e)
            {
                //(TAG, "error building json request", e);
            }

            
            // Create your application here
        }
    }
}