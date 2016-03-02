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

namespace MedTrack.Entity
{
    class IntentObject
    {
        public string MedicineName { get; set; }

        public int NumberOfTimes { get; set; }

        public int NumberOfDays { get; set; }
    }
}