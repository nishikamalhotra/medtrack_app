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
    [DynamoDBTable("Pharmacy")]
    public class Pharmacy
    {
        [DynamoDBHashKey]
        public int PharmacyID { get; set; }

        [DynamoDBRangeKey]
        public string PharmacyName { get; set; }

        public int LocationID { get; set; }

        public override string ToString()
        {
            return string.Format(@" PharmacyID- {0} PharmacyName- {1} LocationID- {2}", 
                PharmacyID, PharmacyName, LocationID);
        }
    }
}