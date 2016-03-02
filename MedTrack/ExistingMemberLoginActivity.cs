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

namespace MedTrack
{
    [Activity(Label = "ExistingMemberLoginActivity")]
    public class ExistingMemberLoginActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ExistingMemberLogin);

            // Get button clicks and user inputs from the layout resource,
            // and attach an event to it
           
            EditText username = FindViewById<EditText>(Resource.Id.username);
            EditText password = FindViewById<EditText>(Resource.Id.password);
            Button loginButton = FindViewById<Button>(Resource.Id.loginButton);
            bool result = false;
            loginButton.Click += async delegate
            {
                LoginService loginService = new LoginService();
                result = await loginService.loginAuthentication(username.Text.ToString(), password.Text.ToString());
               
                if (result)
                {
                    StartActivity(typeof(PatientLoginActivity));
                }
                else
                {
                    //set alert for executing the task
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    // Create empty event handlers, we will override them manually instead of letting the builder handling the clicks.
                    alert.SetPositiveButton("Okay", (EventHandler<DialogClickEventArgs>)null);
                    alert.SetNegativeButton("Cancel", (EventHandler<DialogClickEventArgs>)null);
                    AlertDialog alertDialog = alert.Create();
                    alertDialog.SetTitle("Login Error!");
                    alertDialog.SetIcon(Android.Resource.Drawable.IcDialogAlert);
                    alertDialog.SetMessage("LoginId/ Username and Password do not match, please try again!");
                    alertDialog.Show();
                    // Get the buttons.
                    var okButton = alertDialog.GetButton((int)DialogButtonType.Positive);
                    var cancelButton = alertDialog.GetButton((int)DialogButtonType.Negative);

                    // Assign our handlers.
                    okButton.Click += (sender, args) =>
                    {
                        StartActivity(typeof(ExistingMemberLoginActivity));
                    };
                    cancelButton.Click += (sender, args) =>
                    {
                        StartActivity(typeof(MainActivity));
                    };
                }
            };
        }
    }
}