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
using NBP_Neo4j_Redis.Activities;
using NBP_Neo4j_Redis.Controllers;

namespace NBP_Neo4j_Redis.Adapters
{
    class ImageAdapter : BaseAdapter
    {
        #region Atributes
        private List<string> users;
        private Activity usersActivity;
        private LayoutInflater inflater;
        #endregion

        #region Constructors
        public ImageAdapter(Activity Uactivity, List<string> users) : base()
        {
            this.users = users;
            this.usersActivity = Uactivity;
            inflater = (LayoutInflater)usersActivity.BaseContext.GetSystemService(Context.LayoutInflaterService);
        }
        #endregion

        #region Overrides
        public override Java.Lang.Object GetItem(int position)
        {
            return users[position];
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            View itemView = convertView;

            if (convertView == null)
            {
                itemView = inflater.Inflate(Resource.Layout.User_item, null);
            }

            TextView user = itemView.FindViewById<TextView>(Resource.Id.user);
            ImageView imageChat = itemView.FindViewById<ImageView>(Resource.Id.slikaChat);
            ImageView imageUser = itemView.FindViewById<ImageView>(Resource.Id.slikaUser);
            try { imageUser.Click -= ImageUser_Click; }
            finally { imageUser.Click += ImageUser_Click; }

            user.Text = users[position];
            
            return itemView;
        }
        private void ImageUser_Click(object sender, EventArgs e)
        {

            string[] podaci = ((sender as ImageView).Parent as View).FindViewById<TextView>(Resource.Id.user).Text.Split(' ');
            DataController.Instance.KorisnickoOdabranogProfila = podaci[0];
            int index_odabranog_profila;

            DataController.Instance.OdabraniProfil = DataController.Instance.VratiOdabraniProfil();
            string profil = DataController.Instance.PronadjiProfilLokalno1(DataController.Instance.OdabraniProfil.KorisnickoIme, out index_odabranog_profila);
            DataController.Instance.IndexOdabranogProfilaLajk = index_odabranog_profila;

            usersActivity.StartActivity(typeof(ProfileActivity));
        }
        public override int Count
        {
            get
            {
                return users.Count;
            }
        }
        #endregion
    }

    class ImageAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}