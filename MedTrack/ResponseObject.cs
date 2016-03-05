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
    class ResponseObject
    {
        public string landingUrl { get; set; }

        public string template { get; set; }

        public string uploadLimit { get; set; }

        public string token { get; set; }

        public string err { get; set; }
    }
}