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
using NBP_Neo4j_Redis.Controllers;
using NBP_Neo4j_Redis.Adapters;

namespace NBP_Neo4j_Redis.Activities
{
    [Activity(Theme = "@android:style/Theme.NoTitleBar", Icon = "@drawable/user", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ProfileActivity : Activity
    {
        ImageView _profilnaSlika;
        TextView _textImePrezime;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Profile);
            // Create your application here
            PoveziKomponente();
            UcitajProfilnePodatke();
        }
        public void PoveziKomponente()
        {
              _profilnaSlika = FindViewById<ImageView>(Resource.Id.PprofilnaSlika);
             _textImePrezime = FindViewById<TextView>(Resource.Id.TextViewImePrezime);
        }
        public void UcitajProfilnePodatke()
        {
            if (DataController.Instance.OdabraniProfil.Profilna.Sadrzaj != null)
                _profilnaSlika.SetImageBitmap(DataController.Instance.OdabraniProfil.Profilna.Sadrzaj);
            string imePrezime = DataController.Instance.OdabraniProfil.Ime + " " + DataController.Instance.OdabraniProfil.Prezime;
            _textImePrezime.Text = imePrezime;
        }
    }
}