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
    class LocationLibrary
    {
        private readonly DynamoDBService _dynamoDBService;

        public LocationLibrary()
        {
            _dynamoDBService = new DynamoDBService();
        }

        /// <summary>
        ///  AddLocation will accept a Location object and creates an Item on Amazon DynamoDB
        /// </summary>
        /// <param name="location"></param>
        public async void AddLocation(Location location, Pharmacy pharmacy)
        {
            Task<int> idTask = _dynamoDBService.GetAllLocation<Location>("Location");
            int id = await idTask;
            location.LocationID = id;
            pharmacy.LocationID = id;
            _dynamoDBService.Store(location);
        }
    }
}