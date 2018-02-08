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
using CombinedAPI;
using NBP_Neo4j_Redis.Controllers;
namespace NBP_Neo4j_Redis.Activities
{
    [Activity(MainLauncher = true, Theme = "@android:style/Theme.NoTitleBar", Icon = "@drawable/user", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class LogInActivity : Activity
    {
        #region Controls
        EditText _korisnickoIme;
        EditText _lozinka;
        Button _logIn;
        Button _signIn;
        #endregion

        #region Overrides
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LogIn);

            PoveziKomponente();
        }
        #endregion
   
        #region Methodes
        private void PoveziKomponente()
        {
            _korisnickoIme = FindViewById<EditText>(Resource.Id.editTextKorisnicko);
            _lozinka = FindViewById<EditText>(Resource.Id.editTextSifra);
            _logIn = FindViewById<Button>(Resource.Id.btnLogIn);
            _signIn = FindViewById<Button>(Resource.Id.btnSignIn);
            _logIn.Click += _logIn_Click;
            _signIn.Click += _signIn_Click;

        }
        #endregion

        #region Event Handles
        private void _signIn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SingInActivity));
        }
        private void _logIn_Click(object sender, EventArgs e)
        {
            string korisnickoIme = _korisnickoIme.Text;
            string lozinka = _lozinka.Text;
            bool uspesnoPrijavljivanje = SignLogInController.Instance.PrijaviSe(korisnickoIme, lozinka);

            if (uspesnoPrijavljivanje)
                StartActivity(typeof(UsersActitity));
            else
            {
                var upozorenje = new AlertDialog.Builder(this);
                upozorenje.SetTitle("Upozorenje!");
                upozorenje.SetNeutralButton("Ok", Ok_Click);
                upozorenje.SetMessage("Korisnicko ime i lozinka se ne poklapaju.");
                upozorenje.Show();
            }
        }
        private void Ok_Click(object sender, DialogClickEventArgs e)
        {
        }
        #endregion
    }
}