using Android.App;
using Android.Content;
using Android.Support.V4.App;
using System.Globalization;
using System;

namespace MedTrack.Service
{
    //not using this class currently- not working
    public class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            throw new NotImplementedException();
        }

        public void onReceive(Context context, Intent intent)
        {

            var message = intent.GetStringExtra("message");
            var title = intent.GetStringExtra("title");
            var notIntent = new Intent(context, typeof(MainActivity));
            var contentIntent = PendingIntent.GetActivity(context, 0, notIntent, PendingIntentFlags.CancelCurrent);
            var style = new NotificationCompat.BigTextStyle();
            style.BigText(message);
            var manager = NotificationManagerCompat.From(context);
            var label = 0;
            var builder = new NotificationCompat.Builder(context)
                            .SetContentIntent(contentIntent)
                            .SetSmallIcon(Resource.Drawable.Icon)
                            .SetContentTitle(title)
                            .SetContentText(message)
                            .SetStyle(style)
                            .SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
                            .SetAutoCancel(true);

            var notification = builder.Build();
            manager.Notify(0, notification);

        }
    }
}
 



