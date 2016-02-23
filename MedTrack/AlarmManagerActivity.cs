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
using Java.Util;
using MedTrack.Library;
using System.Threading.Tasks;
using MedTrack.Entity;
using System.Text.RegularExpressions;
using Java.Lang;

namespace MedTrack
{
    [Activity(Label = "AlarmManagerActivity")]
    public class AlarmManagerActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AlarmLayout);
            Context context = this.ApplicationContext;
            Intent intent = new Intent(context, typeof(AlarmReceiver));
            PendingIntent pendingIntent_morning;
            Button alarmset = FindViewById<Button>(Resource.Id.AlarmSet);

            alarmset.Click += async delegate
            {

                AlarmService service = new AlarmService();               
                string med = await service.getMedicine();       
                string[] values = Regex.Split(med, "&");
                Console.WriteLine(string.Format( "Medicine: {0}", med));

                intent.PutExtra("MedicineName", values[0]);
                intent.PutExtra("NumberofTimes", values[1].Trim());
                intent.PutExtra("check", "true");
                var numOfTime = values[1].Trim();

                Locale loc = new Locale("en", "us");
                // Create Pacific time zone with -8 hours offset:
                Java.Util.TimeZone tz = new SimpleTimeZone(-28800000, "America/Los_Angeles");

                //morning alarm
                Calendar cal_alarm_morning =  Calendar.GetInstance(tz, loc);
                cal_alarm_morning.Set(CalendarField.HourOfDay, 09);
                cal_alarm_morning.Set(CalendarField.Minute, 00);
                cal_alarm_morning.Set(CalendarField.Second, 0);

                pendingIntent_morning = PendingIntent.GetBroadcast(context, 0, intent, 0);
                AlarmManager manager = (AlarmManager)GetSystemService(Context.AlarmService);
                manager.SetRepeating(AlarmType.RtcWakeup, cal_alarm_morning.TimeInMillis, Android.App.AlarmManager.IntervalDay, pendingIntent_morning);
                Toast.MakeText(this, "Alarm Set", ToastLength.Long).Show();
            };          
        }
    }
}