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
using Amazon.DynamoDBv2.DocumentModel;
using System.Threading.Tasks;

namespace MedTrack.Library
{
    class PatientLibrary
    {
        private readonly DynamoDBService _dynamoDBService;

        public PatientLibrary()
        {
            _dynamoDBService = new DynamoDBService();
        }

        /// <summary>
        ///  AddPatient will accept a Patient object and creates an Item on Amazon DynamoDB
        /// </summary>
        /// <param name="patient"></param>
        public async Task<int> AddPatient(Patient patient)
        {
            Task<int> idTask = _dynamoDBService.GetAllPatient<Patient>("Patient");
            int id = await idTask;
            patient.PatientID = id;
            _dynamoDBService.Store(patient);
            return id;
        }


        /// <summary>
        /// ModifyPatient tries to load an existing Patient, modifies and saves it back. If the Item doesn’t exist, it raises an exception
        /// </summary>
        /// <param name="patient"></param>
        public void ModifyPatient(Patient patient)
        {
            _dynamoDBService.UpdateItem(patient);
        }
    }
}