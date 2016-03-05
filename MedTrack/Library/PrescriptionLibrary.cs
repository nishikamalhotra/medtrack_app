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

        public async Task<string> FindPrescriptionByCurrentDate(string date)
        {
            //string date = "2162016";
            Task<string> medicineName = _dynamoDBService.FindPrescriptionForCurrentDate(date);
            string med = await medicineName;
            return med;
        }

        public async Task<string> FindRxByBarcode(long barcode, int patientID)
        {
            //string date = "2162016";
            Task<string> rxNum = _dynamoDBService.FindRxNumberByBarcode(barcode, patientID);
            string rxNumber = await rxNum;
            return rxNumber;
        }
    }
}