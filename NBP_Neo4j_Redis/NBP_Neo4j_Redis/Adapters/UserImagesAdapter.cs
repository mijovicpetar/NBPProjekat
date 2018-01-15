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
using NBP_Neo4j_Redis.NecessaryClasses;
using NBP_Neo4j_Redis.Activities;
using NBP_Neo4j_Redis.TLEntities;

namespace NBP_Neo4j_Redis.Adapters
{
    public class UserImagesAdapter : BaseAdapter
    {
        
        private List<TwoImages> images;
        private ProfileActivity profileActivity;
        private LayoutInflater inflater;


        public UserImagesAdapter(ProfileActivity profileActivity, List<TwoImages> twoImages) :base()
        {
            
            this.profileActivity = profileActivity;
            images = twoImages;
        }


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
            LayoutInflater inflater = (LayoutInflater)profileActivity.BaseContext.GetSystemService(Context.LayoutInflaterService);
            View view = inflater.Inflate(Resource.Layout.Image_item, null);

            TwoImages slike = images[position];

            ImageView image1, image2;
            image1 = view.FindViewById<ImageView>(Resource.Id.slikaKorisnika1);
            image2 = view.FindViewById<ImageView>(Resource.Id.slikaKorisnika2);
            
            
            ViewGroup.LayoutParams param1 = (ViewGroup.LayoutParams)image1.LayoutParameters;
            param1.Height = 360;
            image1.LayoutParameters = param1;

            ViewGroup.LayoutParams param2 = (ViewGroup.LayoutParams)image2.LayoutParameters;
            param2.Height = 360;
            image2.LayoutParameters = param2;

            image1.Click += Image1_Click;
            image2.Click += Image2_Click;
            //  image1.SetImageBitmap(BitmapConverter.ConvertByteArrayToBitmap(images[position].Slika1.Sadrzaj));
            // image2.SetImageBitmap(BitmapConverter.ConvertByteArrayToBitmap(images[position].Slika2.Sadrzaj));


            return view;
        }

        private void Image2_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Image1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return images.Count;
            }
        }

    }

    class UserImagesAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}