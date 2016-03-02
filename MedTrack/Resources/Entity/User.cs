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

namespace MedTrack.Resources.Entity
{
    [DynamoDBTable("User")]
    class User
    {
        [DynamoDBHashKey]
        public string Username { get; set; }

        public byte[] Password { get; set; }
    }
}