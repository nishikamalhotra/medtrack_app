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
    class LandingRequestObject
    {
        public string apiKey { get; set; }

        public string affId { get; set; }

        public string transaction { get; set; }

        public string act { get; set; }

        public string view { get; set; }

        public string devinf { get; set; }

        public string appver { get; set; }
    }
}