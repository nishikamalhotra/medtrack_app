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
    [DynamoDBTable("Location")]
    public class Location
    {
        [DynamoDBHashKey]
        public int LocationID { get; set; }

        [DynamoDBRangeKey]
        public int Zipcode { get; set; }

        public string AddressLine1 { get; set; }

        public string State { get; set; }

        public override string ToString()
        {
            return string.Format(@" LocationID- {0} Zipcode- {1} AddressLine1- {2} State- {3}", LocationID, Zipcode, AddressLine1, State);
        }
    }
}