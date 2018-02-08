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
using NBP_Neo4j_Redis.Controllers;
using CombinedAPI.Entities;
using CombinedAPI;

namespace NBP_Neo4j_Redis.Activities
{
    [Activity(Theme = "@android:style/Theme.NoTitleBar", Icon = "@drawable/user", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ImageActivity : Activity
    {
        #region Controls
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
        #endregion

        #region Atributes
        private List<string> users = new List<string>();
        #endregion

        #region Overrides
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Image);
            // Create your application here
            PoveziKomponente();

        }
        #endregion

        #region Methods
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

            try { edit_slike.Click -= Edit_slike_Click; }
            finally { edit_slike.Click += Edit_slike_Click; }
            try { delete.Click -= Delete_Click; }
            finally { delete.Click += Delete_Click; }
            try { ok.Click -= Ok_Click; }
            finally { ok.Click += Ok_Click; }
            try { lajk.Click -= Lajk_Click; }
            finally { lajk.Click += Lajk_Click; }

            PrikaziPodatke();

            PrikaziPoljaZaPregled();
        }

        private void Lajk_Click(object sender, EventArgs e)
        {
            DataController.Instance.NapraviLajk();
            // PrikaziPodatke();
            //StartActivity(typeof(ImageActivity));
            DataController.Instance.ProfiliLajkovi.Clear();
            ViewController.Instance.ListaProfilaLajkovi.Adapter = new ImageAdapter(this, DataController.Instance.ProfiliLajkovi);
            lajkovi.Text = "Broj osoba kojima se sviđa slika: " + DataController.Instance.ProfiliLajkovi.Count.ToString();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            DataController.Instance.OdabranaSlika.Opis = edit_opis_slike.Text;

            string lokacija = edit_lokacija.Text;
            DataController.Instance.OdabranaSlika.LokacijaSlike = new Lokacija();
            DataController.Instance.OdabranaSlika.LokacijaSlike.Naziv = lokacija;
            DataController.Instance.IzmeniLokaciju();
            if (DataController.Instance.IzmeniOdabranuSliku())
                StartActivity(typeof(ImageActivity));
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            DataController.Instance.ObrisiOdabranuSliku();
            StartActivity(typeof(ProfileActivity));
        }

        private void Edit_slike_Click(object sender, EventArgs e)
        {
            PrikaziPoljaZaEditovanje();
        }

        private void PrikaziPodatke()
        {
            ViewController.Instance.Context = this;
            ViewController.Instance.ListaProfilaLajkovi = osobe;
            ViewController.Instance.RenederujProfileLajkovi();

            opis.Text = DataController.Instance.OdabranaSlika.Opis;
            if (DataController.Instance.OdabranaSlika.LokacijaSlike != null)
                 lokacija.Text = DataController.Instance.OdabranaSlika.LokacijaSlike.Naziv + " " + DataController.Instance.OdabranaSlika.LokacijaSlike.Grad;
            if(DataController.Instance.OdabranaSlika.Sadrzaj != null)
            {
                profilna.SetImageBitmap(BitmapConverter.ConvertStringToBitmap(DataController.Instance.OdabranaSlika.Sadrzaj));
            }
            lajkovi.Text = "Broj osoba kojima se sviđa slika: " + DataController.Instance.ProfiliLajkovi.Count.ToString();
        }
        public void PrikaziPoljaZaEditovanje()
        {
            if (DataController.Instance.OdabranaSlika.Username == SignLogInController.Instance.MojProfil.KorisnickoIme)
            {
                opis.Visibility = ViewStates.Invisible;
                lokacija.Visibility = ViewStates.Invisible;

                edit_lokacija.Visibility = ViewStates.Visible;
                edit_opis_slike.Visibility = ViewStates.Visible;

                ok.Visibility = ViewStates.Visible;
                edit_slike.Visibility = ViewStates.Invisible;

                delete.Visibility = ViewStates.Visible;
            }
        }
        public void PrikaziPoljaZaPregled()
        {
            if (DataController.Instance.OdabranaSlika.Username == SignLogInController.Instance.MojProfil.KorisnickoIme)
            {
                opis.Visibility = ViewStates.Visible;
                lokacija.Visibility = ViewStates.Visible;

                edit_lokacija.Visibility = ViewStates.Invisible;
                edit_opis_slike.Visibility = ViewStates.Invisible;

                ok.Visibility = ViewStates.Invisible;
                edit_slike.Visibility = ViewStates.Visible;
                delete.Visibility = ViewStates.Visible;
            }
            else
            {
                edit_lokacija.Visibility = ViewStates.Invisible;
                edit_opis_slike.Visibility = ViewStates.Invisible;
                edit_slike.Visibility = ViewStates.Invisible;
                ok.Visibility = ViewStates.Invisible;
                delete.Visibility = ViewStates.Invisible;
                opis.Visibility = ViewStates.Visible;
                lokacija.Visibility = ViewStates.Visible;
                
            }
        }

        public override void OnBackPressed()
        {
            StartActivity(typeof(ProfileActivity));
        }
        #endregion
    }
}