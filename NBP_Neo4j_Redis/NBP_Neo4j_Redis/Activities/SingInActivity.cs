using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Net;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Java.IO;
using Android.Graphics;
using Android.Provider;
using Android.Content.PM;
using NBP_Neo4j_Redis.Controllers;
using Android.Graphics.Drawables;
using CombinedAPI.Entities;
using CombinedAPI;
namespace NBP_Neo4j_Redis.Activities
{
    [Activity(MainLauncher = true, Theme = "@android:style/Theme.NoTitleBar", Icon = "@drawable/user", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class SingInActivity : Activity
    {
        #region Komponente
        EditText _ime;
        EditText _prezime;
        EditText _username;
        EditText _password;
        EditText _pol;
        EditText _datumRodjenja;
        EditText _mestoStanovanja;
        Button _signIn;
        ImageView _profilnaSlika;
        #endregion

        #region readonly
        // prosledjuje se kao requestCode u slucaju da je slika ucitana iz galerije
        public static readonly int PickImageId = 1000;
        //prosledjuje se kao requestCode u slucaju da je slika upravno napravljena
        public static readonly int PickImageFromCameraId = 1001;
        #endregion

        #region Override
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SignIn);
            // Create your application here

            PoveziKomponente();
         //   UcitajPocetnuSliku();
            
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == PickImageId && resultCode == Result.Ok && data != null)
            {
                Android.Net.Uri uri = data.Data;
                Bitmap bitmap = Android.Provider.MediaStore.Images.Media.GetBitmap(this.ContentResolver, uri);
                PostaviSliku(bitmap);
            }
            else if (PickImageFromCameraId == requestCode && resultCode == Result.Ok && data != null)
            {
                Bitmap bitmap = (Bitmap)data.Extras.Get("data");
                PostaviSliku(bitmap);
            }
        }
        #endregion

        #region Metode

        private void PoveziKomponente()
        {
            _ime = FindViewById<EditText>(Resource.Id.editTextIme);
            _prezime = FindViewById<EditText>(Resource.Id.editTextPrezime);
            _username = FindViewById<EditText>(Resource.Id.editTextUsername);
            _password = FindViewById<EditText>(Resource.Id.editTextPassword);
            _pol = FindViewById<EditText>(Resource.Id.editTextPol);
            _datumRodjenja = FindViewById<EditText>(Resource.Id.editTextDatumRodjenja);
            _mestoStanovanja = FindViewById<EditText>(Resource.Id.editTextMestoStanovanja);

            _profilnaSlika = FindViewById<ImageView>(Resource.Id.profilnaSlika);
            _profilnaSlika.Click += _profilnaSlika_Click;

            _signIn = FindViewById<Button>(Resource.Id.btnSignIn);
            _signIn.Click += _signIn_Click;
        }

        #region Funkcije vezane za profilnu sliku  
        private void NapraviSliku()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, PickImageFromCameraId);

        }
        public void PrikaziLokalneSlike()
        {
            Intent = new Intent();
            Intent.SetType("image/*");
            Intent.SetAction(Intent.ActionGetContent);

            StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
        }
        public void PostaviSliku(Bitmap bitmap)
        {
            _profilnaSlika.SetImageBitmap(bitmap);
            string array = BitmapConverter.ConvertBitmapToString(bitmap);
            SignLogInController.Instance.MojProfil.Profilna.Sadrzaj = array;            
        }
        public void UcitajPocetnuSliku()
        {
            Bitmap pocetnaSlika = BitmapFactory.DecodeResource(Resources, Resource.Drawable.user);
            _profilnaSlika.SetImageBitmap(pocetnaSlika);
            SignLogInController.Instance.MojProfil.Profilna.Sadrzaj = BitmapConverter.ConvertBitmapToString(pocetnaSlika);
        }
        #endregion

        #region Validacija i ucitavanje vrednosti
        private void UcitajVrednosti()
        {
            SignLogInController.Instance.MojProfil.Ime = _ime.Text;
            SignLogInController.Instance.MojProfil.Prezime = _prezime.Text;
            SignLogInController.Instance.MojProfil.Lozinka = _password.Text;
            SignLogInController.Instance.MojProfil.MestoStanovanja = _mestoStanovanja.Text;
            SignLogInController.Instance.MojProfil.Pol = _pol.Text;
            SignLogInController.Instance.MojProfil.KorisnickoIme = _username.Text;

        }
        private bool Validacija()
        {
            var upozorenje = new AlertDialog.Builder(this);
            upozorenje.SetTitle("Upozorenje!");
            upozorenje.SetNeutralButton("Ok", Ok_Click);
            string poruka = "Morate uneti ";
            if (string.IsNullOrEmpty(_ime.Text))
            {
                poruka += "ime.";
                upozorenje.SetMessage(poruka);
                upozorenje.Show();
                return false;
            }
            else if (string.IsNullOrEmpty(_ime.Text))
            {
                poruka += "ime.";
                upozorenje.SetMessage(poruka);
                upozorenje.Show();
                return false;
            }
            else if(string.IsNullOrEmpty(_prezime.Text))
            {
                poruka += "prezime.";
                upozorenje.SetMessage(poruka);
                upozorenje.Show();
                return false;
            }
            else if(string.IsNullOrEmpty(_username.Text))
            {
                poruka += "korisnicko ime.";
                upozorenje.SetMessage(poruka);
                upozorenje.Show();
                return false;
            }
            else if(string.IsNullOrEmpty(_password.Text))
            {
                poruka += "lozinku.";
                upozorenje.SetMessage(poruka);
                upozorenje.Show();
                return false;
            }
            else if (string.IsNullOrEmpty(_pol.Text))
            {
                poruka += "pol.";
                upozorenje.SetMessage(poruka);
                upozorenje.Show();
                return false;
            }
            else if (string.IsNullOrEmpty(_mestoStanovanja.Text))
            {
                poruka += "mesto stanovanja.";
                upozorenje.SetMessage(poruka);
                upozorenje.Show();
                return false;
            }
            else if (string.IsNullOrEmpty(_datumRodjenja.Text))
            {
                poruka += "datum rodjenja.";
                upozorenje.SetMessage(poruka);
                upozorenje.Show();
                return false;
            }
            return true;
        }
        #endregion

        #endregion
        
        #region Event Handlers
        private void Ok_Click(object sender, DialogClickEventArgs e)
        {
        }
        public void _profilnaSlika_Click(object sender, EventArgs e)
        {
            PopupMenu popup = new PopupMenu(this, _profilnaSlika);
            popup.Inflate(Resource.Layout.PopUpProfilnaSlikaMenu);

            try { popup.MenuItemClick -= PopUpMenuItemClick; }
            finally { popup.MenuItemClick += PopUpMenuItemClick; }

            popup.Show();
            
        }
        private void PopUpMenuItemClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            if (e.Item.ItemId == Resource.Id.itemKamera)
            {
                NapraviSliku();
            }
            else if (e.Item.ItemId == Resource.Id.itemOdaberi)
            {
                PrikaziLokalneSlike();
            }
        }

        private void _signIn_Click(object sender, EventArgs e)
        {
            if (Validacija())
            {
                UcitajVrednosti();
                DataController.Instance.OdabraniProfil = SignLogInController.Instance.MojProfil;

                string result = SignLogInController.Instance.RegistrujSe();

                if (result != null)
                {
                    var upozorenje = new AlertDialog.Builder(this);
                    upozorenje.SetTitle("Upozorenje!");
                    upozorenje.SetNeutralButton("Ok", Ok_Click);
                    upozorenje.SetMessage(result);
                    upozorenje.Show();
                }
                else
                {
                    bool prijavljivanje = SignLogInController.Instance.PrijaviSe();
                    if(prijavljivanje)
                        StartActivity(typeof(UsersActitity));
                }
            }
        }

        #endregion
    }
}