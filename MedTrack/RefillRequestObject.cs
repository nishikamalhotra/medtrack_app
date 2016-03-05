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
    class RefillRequestObject
    {
        public string appId { get; set; }

        public string affId { get; set; }

        public string token { get; set; }

        public string rxNo { get; set; }

        public string appCallBackScheme { get; set; }

        public string appCallBackAction { get; set; }

        public string act { get; set; }

        public string devinf { get; set; }

        public string appver { get; set; }

        public string lat { get; set; }

        public string lng { get; set; }
    }
}