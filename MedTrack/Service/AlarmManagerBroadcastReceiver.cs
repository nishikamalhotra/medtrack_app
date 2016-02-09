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
using Java.Util;

namespace MedTrack.Service
{
    class AlarmManagerBroadcastReceiver : BroadcastReceiver
    {
        public static String ONE_TIME = "onetime";

        public override void OnReceive(Context context, Intent intent)
        {
            // should go off repeatedly
            Toast.MakeText(context, "I am running!!!!.",ToastLength.Long).Show();
      
            //PowerManager pm = (PowerManager)context.GetSystemService(Context.PowerService);
            //PowerManager.WakeLock wl = pm.NewWakeLock(WakeLockFlags.Partial, "YOUR TAG");
            ////Acquire the lock
            // wl.Acquire();

            

            //Toast.MakeText(context, "alarm going off", ToastLength.Long);
            //wl.Release();
            // throw new NotImplementedException();
            
        }

        //public void SetAlarm(Context context, PendingIntent pi)
        //{
        //    AlarmManager am = (AlarmManager)context.GetSystemService(Context.AlarmService);
        //    int interval = 1000 * 60;
        //    // Create a locale:
        //    Locale loc = new Locale("en", "us");

        //    Calendar calendar = Calendar.GetInstance(loc);

        //    calendar.Set(CalendarField.HourOfDay, 13);
        //    calendar.Set(CalendarField.Minute, 30);

        //    // Intent intent = new Intent(context, typeof(AlarmManagerBroadcastReceiver));
        //    // intent.PutExtra(ONE_TIME, false);
        //    // intent.PutExtra("message", "Hi this is an alarm");
        //    // PendingIntent pi = PendingIntent.GetBroadcast(context, 0, intent, 0);
        //    //After after 10 seconds
        //    am.SetRepeating(AlarmType.RtcWakeup, SystemClock.CurrentThreadTimeMillis(),interval, pi);
        //}
    }
}