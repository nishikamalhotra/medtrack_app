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
    [Activity(Label = "RepeatAfternoonAlarmActivity")]
    public class RepeatAfternoonAlarmActivity : Activity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AlarmLayout);
            Context context = this.ApplicationContext;
            Intent intent = new Intent(context, typeof(AlarmReceiver));

            PendingIntent pendingIntent_afternoon;

            AlarmService service = new AlarmService();
            string med = await service.getMedicine();
            string[] values = Regex.Split(med, "&");
            Console.WriteLine(string.Format("Medicine: {0}", med));

            intent.PutExtra("MedicineName", values[0]);
            if(Convert.ToInt32(values[1].Trim()) > 3)
            {
                intent.PutExtra("NumberofTimes", (values[1].Trim()));
                intent.PutExtra("afternoon", "false");
                intent.PutExtra("evening", "true");
                intent.PutExtra("check", "false");
            } else
            {
                intent.PutExtra("NumberofTimes", "2");
            }
            
            Locale loc = new Locale("en", "us");
            // Create Pacific time zone with -8 hours offset:
            Java.Util.TimeZone tz = new SimpleTimeZone(-28800000, "America/Los_Angeles");

            Calendar cal_alarm_afternoon = Calendar.GetInstance(tz, loc);
            cal_alarm_afternoon.Set(CalendarField.HourOfDay, 16);
            cal_alarm_afternoon.Set(CalendarField.Minute, 58);
            cal_alarm_afternoon.Set(CalendarField.Second, 0);

            pendingIntent_afternoon = PendingIntent.GetBroadcast(context, 0, intent, PendingIntentFlags.UpdateCurrent);
            AlarmManager manager = (AlarmManager)GetSystemService(Context.AlarmService);
            manager.SetRepeating(AlarmType.RtcWakeup, cal_alarm_afternoon.TimeInMillis, Android.App.AlarmManager.IntervalDay, pendingIntent_afternoon);
            Toast.MakeText(this, "Alarm Set for afternoon", ToastLength.Long).Show();
        }
    }
}