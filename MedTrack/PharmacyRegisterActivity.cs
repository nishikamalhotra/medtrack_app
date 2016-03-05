
using Android.App;
using Android.OS;
using Android.Widget;
using MedTrack.Entity;
using MedTrack.Library;

namespace MedTrack
{
    [Activity(Label = "PharmacyRegisterActivity")]
    public class PharmacyRegisterActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.PharmacyRegister);
            // Create your application here
            EditText pharmacyName = FindViewById<EditText>(Resource.Id.pharmacyText);
            EditText zipcode = FindViewById<EditText>(Resource.Id.pharmacyZipcodeText);
            EditText address = FindViewById<EditText>(Resource.Id.pharmacyAddressText);
            EditText state = FindViewById<EditText>(Resource.Id.stateText);
            Button button = FindViewById<Button>(Resource.Id.resgisterPharmacy);

            button.Click += delegate
            {
                Pharmacy pharmacy = new Pharmacy();
                Location location = new Location();
                pharmacy.PharmacyName = pharmacyName.Text.ToString();
                int zip;
                int.TryParse(zipcode.Text.ToString(), out zip);
                location.Zipcode = zip;
                location.AddressLine1 = address.Text.ToString();
                location.State = state.Text.ToString();
                LocationLibrary locationLib = new LocationLibrary();
                locationLib.AddLocation(location, pharmacy);

                PharmacyLibrary pharmacyLib = new PharmacyLibrary();
                pharmacyLib.AddPharmacy(pharmacy);
            };

        }
    }
}