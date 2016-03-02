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

            string m, d;
            if (day.ToString().Length == 1)
            {
                d = "0" + day.ToString();
            }
            else
            {
                d = day.ToString();
            }
            if (month.ToString().Length == 1)
            {
                m = "0" + month.ToString();
            }
            else
            {
                m = month.ToString();
            }
            string date = m + d + year;

            PrescriptionLibrary lib = new PrescriptionLibrary();
            Task<string> medName = lib.FindPrescriptionByCurrentDate(date);

            string med = await medName;

            return med;

        }




    }
}