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
    [DynamoDBTable("Physician")]
    public class Physician
    {
        [DynamoDBHashKey]
        public int PhysicianID { get; set; }

        [DynamoDBRangeKey]
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public int ContactNumber { get; set; }

        public int OfficeZipcode { get; set; }

        public string Speciality { get; set; }

        public int YearsOfPractice { get; set; }

        public override string ToString()
        {
            return string.Format(@" PhysicianID- {0} LastName- {1} FirstName- {2} ContactNumber- {3}
                OfficeZipcode- {4} Speciality- {5} YearsOfPratice- {6}", PhysicianID, LastName, FirstName, 
                ContactNumber, OfficeZipcode, Speciality, YearsOfPractice);
        }
    }
}