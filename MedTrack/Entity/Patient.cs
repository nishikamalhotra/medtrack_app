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
    [DynamoDBTable("Patient")]
    public class Patient
    {
        [DynamoDBHashKey]
        public int PatientID { get; set; }

        [DynamoDBRangeKey]
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Address { get; set; }

        public int Zipcode { get; set; }

        public int ContactNumber { get; set; }

        public int EmergencyContact { get; set; }

        public int Age { get; set; }

        public int DateOfBirth { get; set; }

        public char Gender { get; set; }

        public int InsuranceID { get; set; }

        public string InsuranceProvider { get; set; }

        public int PhysicianID { get; set; }

        public string PastAilments { get; set; }

        public string Disability { get; set; }


        public override string ToString()
        {
            return string.Format(@" PatientID- {0} LastName- {1} FirstName- {2} Address- {3}
                Zipcode- {4} ContactNumber- {5} EmergencyContact- {6} Age- {7} DateOfBirth- {8}
                Gender- {9} InsuranceID- {10} InsuranceProvider- {11} PhysicianID- {12}
                PastAilments- {13} Disability- {14}", PatientID, LastName, FirstName, Address,
                Zipcode, ContactNumber, EmergencyContact, Age, DateOfBirth, Gender, InsuranceID,
                InsuranceProvider, PhysicianID, PastAilments, Disability);
        }

    }
}