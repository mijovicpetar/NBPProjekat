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
namespace NBP_Neo4j_Redis.Adapters
{
    class ImageAdapter : BaseAdapter
    {
        #region Atributes
        private List<string> users;
        private ImageActivity usersActivity;
        private LayoutInflater inflater;
        #endregion

        #region Constructors
        public ImageAdapter(ImageActivity Uactivity, List<string> users) : base()
        {
            this.users = users;
            this.usersActivity = Uactivity;
            inflater = (LayoutInflater)usersActivity.BaseContext.GetSystemService(Context.LayoutInflaterService);
        }
        #endregion

        #region Overrides
        public override Java.Lang.Object GetItem(int position)
        {
            return position;
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

            user.Text = users[position];
            
            return itemView;
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