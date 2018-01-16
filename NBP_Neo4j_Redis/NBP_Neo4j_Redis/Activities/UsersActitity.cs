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

namespace NBP_Neo4j_Redis.Activities
{
    [Activity(Theme = "@android:style/Theme.NoTitleBar", Icon = "@drawable/user", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class UsersActitity : Activity
    {
        #region Atributes
        private List<string> users = new List<string>();
        #endregion

        #region Controls
        private ListView listUsers;
        private Button myProfile;
        #endregion

        #region Overrides
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Users);
            InitializeAndAssign();
        }
        #endregion

        #region  
        private void InitializeAndAssign()
        {
            listUsers = FindViewById<ListView>(Resource.Id.list_of_profiles);
            myProfile = FindViewById<Button>(Resource.Id.btnMyProfile);
            myProfile.Click += MyProfile_Click;

            ViewController.Instance.Context = this;
            ViewController.Instance.ListaProfila = listUsers;
            ViewController.Instance.RenderujProfile();
        }
        private void MyProfile_Click(object sender, EventArgs e)
        {
            DataController.Instance.OdabraniProfil = SignLogInController.Instance.MojProfil;
            StartActivity(typeof(ProfileActivity));
        }
        #endregion
        
    }
}