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
using MedTrack.Service;
using MedTrack.Entity;
using System.Threading.Tasks;

namespace MedTrack.Library
{
    class PharmacyLibrary
    {
        private readonly DynamoDBService _dynamoDBService;

        public PharmacyLibrary()
        {
            _dynamoDBService = new DynamoDBService();
        }

        /// <summary>
        ///  AddLocation will accept a Location object and creates an Item on Amazon DynamoDB
        /// </summary>
        /// <param name="pharmacy"></param>
        public async void AddPharmacy(Pharmacy pharmacy)
        {
            Task<int> idTask = _dynamoDBService.GetAllPharmacy<Pharmacy>("Pharmacy");
            int id = await idTask;
            pharmacy.PharmacyID = id;
            _dynamoDBService.Store(pharmacy);
        }
    }
}