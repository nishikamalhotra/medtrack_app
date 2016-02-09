using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MedTrack.Service;

namespace MedTrack
{
    [Activity(Label = "MedTrack", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button loginButton = FindViewById<Button>(Resource.Id.MemberLogin);
            Button newRegisterButton = FindViewById<Button>(Resource.Id.NewRegister);
            Button alarm = FindViewById<Button>(Resource.Id.Alarm);


            loginButton.Click += delegate 
            {
                StartActivity(typeof(ExistingMemberLoginActivity));
            };

            newRegisterButton.Click += delegate
            {
                StartActivity(typeof(NewMemberRegisterActivity));
            };

            alarm.Click += delegate
            {
                StartActivity(typeof(AlarmManagerActivity));
            };
            
            //Intent alarmIntent = new Intent(Application.Context, typeof(AlarmReceiver));
            //alarmIntent.PutExtra("message", "Hi this is an alarm");
            //alarmIntent.PutExtra("title", "alarm");

            //PendingIntent pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
            //AlarmManager alarmManager = (AlarmManager)Application.Context.GetSystemService(Context.AlarmService);

            ////TODO: For demo set after 5 seconds.
            //alarmManager.SetRepeating(AlarmType.ElapsedRealtimeWakeup, SystemClock.CurrentThreadTimeMillis(), 5000, pendingIntent);



            
    }
    }
}

