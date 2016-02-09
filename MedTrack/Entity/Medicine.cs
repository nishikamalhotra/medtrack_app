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
    class Medicine
    {
        [DynamoDBHashKey]
        public long MedicineID { get; set; }

        [DynamoDBRangeKey]
        public long Barcode { get; set; }

        public string Name { get; set; }

        public string Make { get; set; }

        public string ManufactureDate { get; set; }

        public string ExpirationDate { get; set; }

        public string DiseaseTargetted { get; set; }

        public int PriceInDollars { get; set; }


    }
}