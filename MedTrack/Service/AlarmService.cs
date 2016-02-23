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
using MedTrack.Library;
using System.Threading.Tasks;

namespace MedTrack.Service
{
    class AlarmService
    {
        DateTime currentDate;
        //String month, day, year, date;


        public async Task<string> getMedicine()
        {
            currentDate = DateTime.Today;
            string month = currentDate.Month.ToString();
            string day = currentDate.Day.ToString();
            string year = currentDate.Year.ToString();
            string date = month + day + year;

            PrescriptionLibrary lib = new PrescriptionLibrary();
            Task<string> medName = lib.FindPrescriptionByCurrentDate(date);

            string med = await medName;

            return med;

        }




    }
}