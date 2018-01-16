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
using Android.Graphics;
using NBP_Neo4j_Redis.NecessaryClasses;
using CombinedAPI.Entities;
using NBP_Neo4j_Redis.TLEntities;
using Android.Provider;

namespace NBP_Neo4j_Redis.Activities
{
    [Activity(Theme = "@android:style/Theme.NoTitleBar", Icon = "@drawable/user", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ProfileActivity : Activity
    {
        #region Controls
        ImageView _profilnaSlika;
        ImageView _imageEdit;
        ImageView _imageOk;
        TextView _textImePrezime;
        TextView _textDatumRodjenja;
        TextView _textMestoStanovanja;
        TextView _textPol;
        ListView _listaSlika;
        EditText _editDatumRodjenja;
        EditText _editPol;
        EditText _editMestoStanovanja;

        Button _btnDodateFotografije;
        Button _btnOznaceneFotografije;
        Button _btnDodajFotografiju;
        #endregion

        #region Overrides
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Profile);

            PoveziKomponente();

            UcitajProfilnePodatke();
            PrikaziPoljaZaPregled();
            PrikazKorisnikovogProfila();

            OsposobiAdapter(DataController.Instance.OdabraniProfil.DodateSlike);
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == SingInActivity.PickImageId && resultCode == Result.Ok && data != null)
            {
                Android.Net.Uri uri = data.Data;
                Bitmap bitmap = Android.Provider.MediaStore.Images.Media.GetBitmap(this.ContentResolver, uri);
                DodajSliku(bitmap);
            }
            else if (SingInActivity.PickImageFromCameraId == requestCode && resultCode == Result.Ok && data != null)
            {
                Bitmap bitmap = (Bitmap)data.Extras.Get("data");
                DodajSliku(bitmap);
            }
        }
        #endregion

        #region Methodes
        public void PoveziKomponente()
        {
            _profilnaSlika = FindViewById<ImageView>(Resource.Id.PprofilnaSlika);
            _profilnaSlika.Click += _profilnaSlika_Click;

            _textImePrezime = FindViewById<TextView>(Resource.Id.TextViewImePrezime);
            _textDatumRodjenja = FindViewById<TextView>(Resource.Id.TextViewDatumRodjenja);
            _textMestoStanovanja = FindViewById<TextView>(Resource.Id.TextViewMestoStanovanja);
            _textPol = FindViewById<TextView>(Resource.Id.TextViewPol);
            _listaSlika = FindViewById<ListView>(Resource.Id.ListViewImages);
            _editDatumRodjenja = FindViewById<EditText>(Resource.Id.EditTextDatumRodjenja);
            _editMestoStanovanja = FindViewById<EditText>(Resource.Id.EditTextMestoStanovanja);
            _editPol = FindViewById<EditText>(Resource.Id.EditTextPol);

            _imageEdit = FindViewById<ImageView>(Resource.Id.imageViewEdit);
            _imageOk = FindViewById<ImageView>(Resource.Id.imageViewOk);
            _imageEdit.Click += _imageEdit_Click;
            _imageOk.Click += _imageOk_Click;

            _btnDodateFotografije = FindViewById<Button>(Resource.Id.buttonIzbaceneSlike);
            _btnOznaceneFotografije = FindViewById<Button>(Resource.Id.buttonOznaceneSlike);
            _btnDodajFotografiju = FindViewById<Button>(Resource.Id.buttonDodajFotografiju);
            _btnDodajFotografiju.Click += _btnDodajFotografiju_Click;
            _btnOznaceneFotografije.Click += _btnOznaceneFotografije_Click;
            _btnDodateFotografije.Click += _btnDodateFotografije_Click;
        }
        public void DodajSliku(Bitmap bitmap)
        {
            DataController.Instance.DodajNovuSliku(bitmap);
            OsposobiAdapter(SignLogInController.Instance.MojProfil.DodateSlike);
        }       
        public void OsposobiAdapter(List<TLSlika> slike)
        {
            List<TwoImages> _listOfTwoImages = new List<TwoImages>();
            for (int i = 0; i < slike.Count / 2; i++)
            {
                TwoImages images = new TwoImages();
                images.Slika1 = slike[i * 2];
                images.Slika2 = slike[i * 2 + 1];
                _listOfTwoImages.Add(images);
            }
            if (slike.Count % 2 != 0)
            {
                TwoImages images = new TwoImages();
                images.Slika1 = slike[slike.Count - 1];
                _listOfTwoImages.Add(images);
            }

            _listaSlika.Adapter = new UserImagesAdapter(this, _listOfTwoImages);
        }
        public void OsposobiProbniAdapter()
        {
            List<TwoImages> slike = new List<TwoImages>();
            for (int i = 0; i < 10; i++)
            {
                slike.Add(new TwoImages());
            }
            _listaSlika.Adapter = new UserImagesAdapter(this, slike);
        }
        public void PrikaziPoljaZaEditovanje()
        {
            _textDatumRodjenja.Visibility = ViewStates.Invisible;
            _textMestoStanovanja.Visibility = ViewStates.Invisible;
            _textPol.Visibility = ViewStates.Invisible;
            _editPol.Visibility = ViewStates.Visible;
            _editDatumRodjenja.Visibility = ViewStates.Visible;
            _editMestoStanovanja.Visibility = ViewStates.Visible;

            _imageOk.Visibility = ViewStates.Visible;
            _imageEdit.Visibility = ViewStates.Invisible;
            _btnDodajFotografiju.Visibility = ViewStates.Invisible;

            _editDatumRodjenja.Text = SignLogInController.Instance.MojProfil.DatumRodjenja.ToShortDateString();
            _editMestoStanovanja.Text = SignLogInController.Instance.MojProfil.MestoStanovanja;
            _editPol.Text = SignLogInController.Instance.MojProfil.Pol;
        }
        public void PrikaziPoljaZaPregled()
        {
            _textDatumRodjenja.Visibility = ViewStates.Visible;
            _textMestoStanovanja.Visibility = ViewStates.Visible;
            _textPol.Visibility = ViewStates.Visible;
            _editPol.Visibility = ViewStates.Invisible;
            _editDatumRodjenja.Visibility = ViewStates.Invisible;
            _editMestoStanovanja.Visibility = ViewStates.Invisible;

            _imageOk.Visibility = ViewStates.Invisible;
            _imageEdit.Visibility = ViewStates.Visible;
            _btnDodajFotografiju.Visibility = ViewStates.Visible;
        }
        public void PrikazKorisnikovogProfila()
        {
            if (SignLogInController.Instance.MojProfil.KorisnickoIme == DataController.Instance.OdabraniProfil.KorisnickoIme)
            {
                _imageEdit.Visibility = ViewStates.Visible;
                _btnDodajFotografiju.Visibility = ViewStates.Visible;
            }
            else
            {
                _imageEdit.Visibility = ViewStates.Invisible;
                _btnDodajFotografiju.Visibility = ViewStates.Invisible;
            }
        }        
        public void UcitajProfilnePodatke()
        {
            if (DataController.Instance.OdabraniProfil.Profilna != null && DataController.Instance.OdabraniProfil.Profilna.Sadrzaj!=null)
            {

                Bitmap bitmap = BitmapConverter.ConvertStringToBitmap(DataController.Instance.OdabraniProfil.Profilna.Sadrzaj);
                _profilnaSlika.SetImageBitmap(bitmap);
            }

            string imePrezime = DataController.Instance.OdabraniProfil.Ime + " " + DataController.Instance.OdabraniProfil.Prezime;
            _textImePrezime.Text = imePrezime;
            _textDatumRodjenja.Text = DataController.Instance.OdabraniProfil.DatumRodjenja.ToShortDateString();
            _textMestoStanovanja.Text = DataController.Instance.OdabraniProfil.MestoStanovanja;
            _textPol.Text = DataController.Instance.OdabraniProfil.Pol;
        }
        private void NapraviSliku()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, SingInActivity.PickImageFromCameraId);

        }
        public void PrikaziLokalneSlike()
        {
            Intent = new Intent();
            Intent.SetType("image/*");
            Intent.SetAction(Intent.ActionGetContent);

            StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), SingInActivity.PickImageId);
        }
        #endregion

        #region Event Handlers
        private void _profilnaSlika_Click(object sender, EventArgs e)
        {

        }
        private void _btnDodateFotografije_Click(object sender, EventArgs e)
        {
            OsposobiAdapter(DataController.Instance.OdabraniProfil.DodateSlike);
            DataController.Instance.TypeOfSelectedImages = TypeOfImages.AddedImages;
        }
        private void _btnOznaceneFotografije_Click(object sender, EventArgs e)
        {
            OsposobiAdapter(DataController.Instance.OdabraniProfil.TagovaneSlike);
            DataController.Instance.TypeOfSelectedImages = TypeOfImages.TagedImages;
        }
        private void _imageOk_Click(object sender, EventArgs e)
        {
            PrikaziPoljaZaPregled();
            // SignLogInController.Instance.MojProfil.DatumRodjenja = _textDatumRodjenja.Text;
            SignLogInController.Instance.MojProfil.MestoStanovanja = _textMestoStanovanja.Text;
            SignLogInController.Instance.MojProfil.Pol = _textPol.Text;

            SignLogInController.Instance.IzmeniProfil();
        }
        private void _imageEdit_Click(object sender, EventArgs e)
        {
            PrikaziPoljaZaEditovanje();
        }
        private void _btnDodajFotografiju_Click(object sender, EventArgs e)
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
        #endregion
    }
}