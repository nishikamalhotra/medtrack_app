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
    class PrescriptionLibrary
    {
        private readonly DynamoDBService _dynamoDBService;

        public PrescriptionLibrary()
        {
            _dynamoDBService = new DynamoDBService();
        }

        /// <summary>
        ///  AddPrescription will accept a Prescription object and creates an Item on Amazon DynamoDB
        /// </summary>
        /// <param name="prescription"></param>
        public async void AddPrescription(Prescription prescription)
        {
            Task<int> idTask = _dynamoDBService.GetAllPrescription<Prescription>("Prescription");
            int id = await idTask;
            prescription.PrescriptionID = id;
            _dynamoDBService.Store(prescription);
        }
    }
}