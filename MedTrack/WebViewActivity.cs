using System;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Webkit;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using MedTrack.Service;

namespace MedTrack
{
    [Activity(Label = "WebViewActivity")]
    public class WebViewActivity : Activity
    {
        public static string FIND_URL = "https://services-qa.walgreens.com/api/util/mweb5url";
        private static string TAG = "WebViewActiviy";
        WebView myWebView;
        string landingURL, token_response;
        string rxNumber;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_web_view);

             rxNumber = Intent.GetStringExtra("rxNumber");
            string response = HttpPostRequest(FIND_URL);
            if (response != null && response != "")
            {
                ResponseObject obj = JsonConvert.DeserializeObject<ResponseObject>(response);
                landingURL = obj.landingUrl;
                token_response = obj.token;
                myWebView = FindViewById<WebView>(Resource.Id.webview);
                myWebView.Settings.JavaScriptEnabled = true;
                myWebView.LoadDataWithBaseURL(landingURL, response, "text/html", "utf-8", "");

                string res = HttpPostRequest(landingURL);
                if (res != null && res != "")
                {
                    myWebView = FindViewById<WebView>(Resource.Id.webview);
                    myWebView.Settings.JavaScriptEnabled = true;
                    myWebView.LoadDataWithBaseURL(landingURL, res, "text/html", "utf-8", "");
                }
            }         
        }

        private string HttpPostRequest(string url)
        {
            LandingRequestObject landingReqObj = null;
            RefillRequestObject refillReqObj = null;
            string str;
            RequestHelper helper = new RequestHelper();
            if (url.Equals(FIND_URL, StringComparison.OrdinalIgnoreCase))
            {
                landingReqObj = helper.PopulateLandingRequestObject();
            }
            else
            {
                refillReqObj = helper.PopulateRefillRequestObject();
                refillReqObj.rxNo = rxNumber;
                refillReqObj.token = token_response;
            }

            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                if (landingReqObj != null)
                {
                    str = JsonConvert.SerializeObject(landingReqObj);
                }
                else
                {
                    str = JsonConvert.SerializeObject(refillReqObj);
                }

                Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(str);

                myHttpWebRequest.Method = "POST";
                myHttpWebRequest.ContentType = "application/json";
                myHttpWebRequest.ContentLength = data.Length;
                Stream stream = myHttpWebRequest.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();

                //fetching the response back
                HttpWebResponse response = (HttpWebResponse)myHttpWebRequest.GetResponse();
                string s = response.ToString();
                // String s = JsonConvert.DeserializeObject<RequestObject>(s);
                Console.WriteLine(s);
                StreamReader reader = new StreamReader(response.GetResponseStream());
                String jsonresponse = "";
                String temp = null;
                while ((temp = reader.ReadLine()) != null)
                {
                    jsonresponse += temp;
                }
                return jsonresponse;
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    string text;
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        text = reader.ReadToEnd();
                        Console.WriteLine(text);
                    }
                    return text;
                }
            }
        }

        public class HelloWebViewClient : WebViewClient
        {
            public override bool ShouldOverrideUrlLoading(WebView view, string url)
            {
                var uri = Android.Net.Uri.Parse(url);
                return true;
            }
        }

    }
}