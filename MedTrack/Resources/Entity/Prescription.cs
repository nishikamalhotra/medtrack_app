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
using Amazon.DynamoDBv2.DataModel;

namespace MedTrack.Entity
{
    class Prescription
    {
        [DynamoDBHashKey]
        public int PrescriptionID { get; set; }

        [DynamoDBRangeKey]
        public int PatientID { get; set; }

        public long Barcode { get; set; }       

        public string StartDate { get; set; }

        public int NumberOfDays { get; set; }

        public int NumberOfTime { get; set; }

        public string PrescribedBy { get; set; }

    }
}