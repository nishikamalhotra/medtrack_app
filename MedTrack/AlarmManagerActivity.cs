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

namespace MedTrack
{
    [Activity(Label = "AlarmManagerActivity")]
    public class AlarmManagerActivity : Activity
    {
        private AlarmManagerBroadcastReceiver alarm;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AlarmLayout);
            Context context = this.ApplicationContext;
            Intent intent = new Intent(context, typeof(AlarmManagerBroadcastReceiver));
            PendingIntent pi = PendingIntent.GetBroadcast(context, 0, intent, 0);
            // Create your application here
            alarm = new AlarmManagerBroadcastReceiver();
            Button alarmset = FindViewById<Button>(Resource.Id.AlarmSet);
            alarmset.Click += delegate
            {
                AlarmManager manager = (AlarmManager)GetSystemService(Context.AlarmService);
                int interval = 8000;

                manager.SetInexactRepeating(AlarmType.RtcWakeup, SystemClock.CurrentThreadTimeMillis(), interval, pi);
                Toast.MakeText(this, "Alarm Set", ToastLength.Long).Show();
            };

            Button alarmsetTime = FindViewById<Button>(Resource.Id.AlarmSetatTime);
            alarmsetTime.Click += delegate
            {
                AlarmManager manager = (AlarmManager)GetSystemService(Context.AlarmService);
                int interval = 1000 * 60;
                Locale loc = new Locale("en", "us");

                Calendar calendar = Calendar.GetInstance(loc);

                calendar.Set(CalendarField.HourOfDay, 16);
                calendar.Set(CalendarField.Minute, 00);
                manager.SetRepeating(AlarmType.RtcWakeup, SystemClock.CurrentThreadTimeMillis(), interval, pi);
            };
        }

        //protected void setRepeatingAlarm(View view)
        //{
        //    Context context = this.ApplicationContext;
        //    if (alarm != null)
        //    {
        //        alarm.SetAlarm(context);
        //    }
        //    else
        //    {
        //        Toast.MakeText(context, "Alarm is null", ToastLength.Long).Show();
        //    }

        //}
        //Context context = this.ApplicationContext;

        //AlarmManager am = (AlarmManager)context.GetSystemService(Context.AlarmService);
        //Intent intent = new Intent(context, typeof(AlarmManagerBroadcastReceiver));

        //intent.PutExtra("message", "Hi this is an alarm");
        //PendingIntent pi = PendingIntent.GetBroadcast(context, 0, intent, 0);
        ////After after 30 seconds
        //long interval = 6000;
        //am.SetRepeating(AlarmType.ElapsedRealtimeWakeup, SystemClock.CurrentThreadTimeMillis(), interval, pi);
        //Toast.MakeText(this, "Alarm set in 5 seconds",ToastLength.Long).Show();

            //if (alarm != null)
            //{
            //    alarm.SetAlarm(context);
            //}
            //else
            //{
            //    Toast.MakeText(context, "Alarm is null", ToastLength.Short).Show();
            //}
        


    }
}