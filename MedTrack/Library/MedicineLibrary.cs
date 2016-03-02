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

namespace MedTrack.Library
{
    class MedicineLibrary
    {
        private readonly DynamoDBService _dynamoDBService;

        public MedicineLibrary()
        {
            _dynamoDBService = new DynamoDBService();
        }


        //public async void FindMedicineWithBarcode(long barcode)
        //{
        //    string tablename = "Medicine";
        //    _dynamoDBService.FindMedicineWithBarcode(barcode, tablename);
        //    //int id = await idTask;
        //    //location.LocationID = id;
        //    //pharmacy.LocationID = id;
        //    //_dynamoDBService.Store(location);
        //}
    }
}