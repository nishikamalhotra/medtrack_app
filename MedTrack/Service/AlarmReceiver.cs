using Android.App;
using Android.Content;
using Android.Support.V4.App;
using System.Globalization;
using System;
using Android.Media;
using Android.Widget;
using MedTrack.Entity;

namespace MedTrack.Service
{
    [BroadcastReceiver]
    public class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Toast.MakeText(context, "Alarm showing now", ToastLength.Long).Show();
            var med = intent.GetStringExtra("MedicineName");
            int number = Convert.ToInt32(intent.GetStringExtra("NumberofTimes"));
            var title = intent.GetStringExtra("title");
            var notIntent = new Intent(context, typeof(MainActivity));
            var contentIntent = PendingIntent.GetActivity(context, 0, notIntent, PendingIntentFlags.CancelCurrent);
            var style = new NotificationCompat.BigTextStyle();
            var check = intent.GetStringExtra("CheckValue");
            NotificationManager manager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            PendingIntent resultPendingIntent = PendingIntent.GetActivity(context, 0, intent, Android.App.PendingIntentFlags.UpdateCurrent);

            Android.Net.Uri alarmSound = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
            NotificationCompat.Builder builder = new NotificationCompat.Builder(context)
                            .SetSmallIcon(Resource.Drawable.Icon)
                            .SetContentTitle("MedTrack Notification")
                            .SetContentText(String.Format("It's time to take {0}- {1} times today", med, number))
                            .SetSound(alarmSound)
                            .SetStyle(style)
                            .SetAutoCancel(true)
                            .SetContentIntent(resultPendingIntent);

            Notification notification = builder.Build();
            manager.Notify(0, notification);
            
            if(number == 2 || (intent.GetStringExtra("evening") == "true"))
            {
                var intentNew = new Intent(context, typeof(RepeatEveningAlarmActivity));
                intentNew.AddFlags(ActivityFlags.NewTask);
                context.StartActivity(intentNew);
            }
            else if((number ==3) || ((number >3 && (intent.GetStringExtra("check") == "true")))
                ||  (number >3 && (intent.GetStringExtra("afternoon") == "true")))
            {            
                var intentNew = new Intent(context, typeof(RepeatAfternoonAlarmActivity));
                intentNew.AddFlags(ActivityFlags.NewTask);             
                context.StartActivity(intentNew);
            }
        }
    }
}
 



