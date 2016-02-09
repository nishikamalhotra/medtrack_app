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
    class PhysicianLibrary
    {
        private readonly DynamoDBService _dynamoDBService;

        public PhysicianLibrary()
        {
            _dynamoDBService = new DynamoDBService();
        }

        /// <summary>
        ///  AddPhysician will accept a physician object and creates an Item on Amazon DynamoDB
        /// </summary>
        /// <param name="physician"></param>
        public async void AddPhysician(Physician physician)
        {
            Task<int> idTask = _dynamoDBService.GetAllPhysician<Physician>("Physician");
            int id = await idTask;
            physician.PhysicianID = id;
            _dynamoDBService.Store(physician);
        }

        /// <summary>
        /// ModifyPhysician tries to load an existing Physician, modifies and saves it back. If the Item doesn’t exist, it raises an exception
        /// </summary>
        /// <param name="patient"></param>
        public void ModifyPhysician(Physician physician)
        {
            _dynamoDBService.UpdateItem(physician);
        }

    }
}