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
using System.Security.Cryptography;
using MedTrack.Service;
using System.Threading.Tasks;
using MedTrack.Resources.Entity;

namespace MedTrack
{
    [Activity(Label = "NewUserRegistration")]
    public class NewUserRegistration : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.NewUserRegistration);

            EditText username = FindViewById<EditText>(Resource.Id.newusername);
            EditText password = FindViewById<EditText>(Resource.Id.newpassword);
            Button registerButton = FindViewById<Button>(Resource.Id.registerButton);

            registerButton.Click += delegate
            {
                string u_name = username.Text.ToString();
                string new_passowrd = password.Text.ToString();
                if (!(String.IsNullOrWhiteSpace(u_name) || String.IsNullOrWhiteSpace(new_passowrd)))
                {
                    var data = Encoding.ASCII.GetBytes(new_passowrd);
                    var sha1 = new SHA1CryptoServiceProvider();
                    var sha1data = sha1.ComputeHash(data);

                    LoginService service = new LoginService();
                    User user = new User();
                    user.Username = u_name;
                    user.Password = sha1data;
                    bool ret = service.SaveNewUserDetails(user);
                    if (ret)
                    {
                        //set alert for executing the task
                        AlertDialog.Builder alert = new AlertDialog.Builder(this);
                        alert.SetPositiveButton("Okay", (EventHandler<DialogClickEventArgs>)null);
                        AlertDialog alertDialog = alert.Create();
                        alertDialog.SetTitle("Registration Successfull!");
                        alertDialog.SetIcon(Android.Resource.Drawable.IcDialogAlert);
                        alertDialog.SetMessage("New user added successfully");
                        alertDialog.Show();
                        // Get the buttons.
                        var okButton = alertDialog.GetButton((int)DialogButtonType.Positive);

                        // Assign our handlers.
                        okButton.Click += (sender, args) =>
                        {
                            StartActivity(typeof(NewMemberRegisterActivity));
                        };
                    }
                } else
                {
                    //set alert for executing the task
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetPositiveButton("Okay", (EventHandler<DialogClickEventArgs>)null);
                    alert.SetNegativeButton("Cancel", (EventHandler<DialogClickEventArgs>)null);
                    AlertDialog alertDialog = alert.Create();
                    alertDialog.SetTitle("Registration Error!");
                    alertDialog.SetIcon(Android.Resource.Drawable.IcDialogAlert);
                    alertDialog.SetMessage("Could not register, please try again!");
                    alertDialog.Show();
                    var okButton = alertDialog.GetButton((int)DialogButtonType.Positive);
                    var cancelButton = alertDialog.GetButton((int)DialogButtonType.Negative);

                    // Assign our handlers.
                    okButton.Click += (sender, args) =>
                    {
                        StartActivity(typeof(NewUserRegistration));
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