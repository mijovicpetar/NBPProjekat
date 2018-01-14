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
using NBP_Neo4j_Redis.Adapters;
namespace NBP_Neo4j_Redis.Activities
{
    [Activity(Theme = "@android:style/Theme.NoTitleBar", Icon = "@drawable/user", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ImageActivity : Activity
    {
        private ImageView profilna;
        private TextView opis;
        private TextView lokacija;
        private TextView lajkovi;
        private ImageView lajk;
        private ListView osobe;
        private EditText edit_opis_slike;
        private EditText edit_lokacija;
        private ImageView edit_slike;
        private ImageView ok;
        private ImageView delete;

        private List<string> users = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Image);
            // Create your application here
            PoveziKomponente();

            OsposobiAdapter();

            opis.Text = "opis slike";
            lokacija.Text = "Blace";
            lajkovi.Text = lajkovi.Text + "36";

            PrikaziPoljaZaEditovanje();
        }
        public void PoveziKomponente()
        {
            profilna = FindViewById<ImageView>(Resource.Id.profilnaSlika);
            opis = FindViewById<TextView>(Resource.Id.opis_slike);
            lokacija = FindViewById<TextView>(Resource.Id.lokacija);
            lajkovi = FindViewById<TextView>(Resource.Id.lajkovi);
            lajk = FindViewById<ImageView>(Resource.Id.lajk);
            osobe = FindViewById<ListView>(Resource.Id.list_of_persons);
            edit_opis_slike = FindViewById<EditText>(Resource.Id.edit_opis_slike);
            edit_lokacija = FindViewById<EditText>(Resource.Id.edit_lokacija);
            edit_slike = FindViewById<ImageView>(Resource.Id.imageEdit);
            delete = FindViewById<ImageView>(Resource.Id.imageDelete);
            ok = FindViewById<ImageView>(Resource.Id.imageOk);
        }
        private void OsposobiAdapter()
        {
            for (int i = 0; i < 10; i++)
            {
                string s = "Melanija Krstojevic" + i;
                users.Add(s);
            }


            osobe.Adapter = new ImageAdapter(this, users);
        }

        public void PrikaziPoljaZaEditovanje()
        {
            opis.Visibility = ViewStates.Invisible;
            lokacija.Visibility = ViewStates.Invisible;

            edit_lokacija.Visibility = ViewStates.Visible;
            edit_opis_slike.Visibility = ViewStates.Visible;

            ok.Visibility = ViewStates.Visible;
            edit_slike.Visibility = ViewStates.Invisible;
        }

        public void PrikaziPoljaZaPregled()
        {
            opis.Visibility = ViewStates.Visible;
            lokacija.Visibility = ViewStates.Visible;

            edit_lokacija.Visibility = ViewStates.Invisible;
            edit_opis_slike.Visibility = ViewStates.Invisible;

            ok.Visibility = ViewStates.Invisible;
            edit_slike.Visibility = ViewStates.Visible;
        }
    }
}