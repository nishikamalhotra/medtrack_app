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

namespace MedTrack.Service
{
    class DeviceBootReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action.Equals("android.intent.action.BOOT_COMPLETED"))
            {
                ///* Setting the alarm here */
                //Intent alarmIntent = new Intent(context, typeof(AlarmManagerBroadcastReceiver));
                //PendingIntent pendingIntent = PendingIntent.GetBroadcast(context, 0, alarmIntent, 0);

                //AlarmManager manager = (AlarmManager)context.GetSystemService(Context.AlarmService);
                //int interval = 8000;
                //manager.SetInexactRepeating(AlarmType.RtcWakeup, SystemClock.CurrentThreadTimeMillis(), interval, pendingIntent);

                Toast.MakeText(context, "MedTrack has restarted", ToastLength.Short).Show();
                //AlarmHelper.setAllAlarms(context);
            }
        }
    }
}

