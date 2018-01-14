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
    public class UsersActitity : Activity
    {

        private List<string> users = new List<string>();
        private ListView listUsers;
        private Button myProfile;

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Users);

            listUsers = FindViewById<ListView>(Resource.Id.list_of_profiles);
            myProfile = FindViewById<Button>(Resource.Id.btnMyProfile);
            
            OsposobiAdapter();
        }

        private void OsposobiAdapter()
        {
            for (int i = 0; i < 10; i++)
            {
                string s = "Melanija Krstojevic" + i;
                users.Add(s);
            }


            listUsers.Adapter = new UsersAdapter(this, users);

        }


    }
}