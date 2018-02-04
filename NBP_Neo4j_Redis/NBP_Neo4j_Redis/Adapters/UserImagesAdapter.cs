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
using NBP_Neo4j_Redis.Controllers;

namespace NBP_Neo4j_Redis.Adapters
{
    public class UserImagesAdapter : BaseAdapter
    {
        #region Atributes
        private List<TwoImages> images;
        private ProfileActivity profileActivity;
#pragma warning disable CS0169 // The field 'UserImagesAdapter.inflater' is never used
        private LayoutInflater inflater;
#pragma warning restore CS0169 // The field 'UserImagesAdapter.inflater' is never used
        #endregion

        #region Constructors
        public UserImagesAdapter(ProfileActivity profileActivity, List<TwoImages> twoImages) :base()
        {
            
            this.profileActivity = profileActivity;
            images = twoImages;
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
            LayoutInflater inflater = (LayoutInflater)profileActivity.BaseContext.GetSystemService(Context.LayoutInflaterService);
            View view = inflater.Inflate(Resource.Layout.Image_item, null);

            TwoImages slike = images[position];
            ImageView image1, image2;
            image1 = view.FindViewById<ImageView>(Resource.Id.slikaKorisnika1);
            image2 = view.FindViewById<ImageView>(Resource.Id.slikaKorisnika2);

            TextView kljucSlike1, kljucSlike2;
            kljucSlike1 = view.FindViewById<TextView>(Resource.Id.KljucSlike1);
            kljucSlike2 = view.FindViewById<TextView>(Resource.Id.KljucSlike2);
            
            kljucSlike1.Text = slike.Slika1.Kljuc;
            if (slike.Slika2 != null)
                kljucSlike2.Text = slike.Slika2.Kljuc;
            else
                image2.Visibility = ViewStates.Invisible;

            kljucSlike1.Visibility = ViewStates.Invisible;
            kljucSlike2.Visibility = ViewStates.Invisible;

            PostaviSlike(slike, image1, image2);

            return view;
        }
        public override int Count
        {
            get
            {
                return images.Count;
            }
        }
        #endregion

        #region Event Handles
        private void Image2_Click(object sender, EventArgs e)
        {
            string kljuc2 = ((sender as ImageView).Parent as View).FindViewById<TextView>(Resource.Id.KljucSlike2).Text;
            if (kljuc2 != "")
            {
                DataController.Instance.NadjiOdabranuSliku(kljuc2);
                profileActivity.StartActivity(typeof(ImageActivity));
            }
        }
        private void Image1_Click(object sender, EventArgs e)
        {
            string kljuc1 = ((sender as ImageView).Parent as View).FindViewById<TextView>(Resource.Id.KljucSlike1).Text;
            if (kljuc1 != "")
            {
                DataController.Instance.NadjiOdabranuSliku(kljuc1);
                profileActivity.StartActivity(typeof(ImageActivity));
            }
        }
        #endregion

        #region Methodes
        private void PostaviSlike(TwoImages twoImages, ImageView image1, ImageView image2)
        {

            ViewGroup.LayoutParams param1 = (ViewGroup.LayoutParams)image1.LayoutParameters;
            param1.Height = 360;
            image1.LayoutParameters = param1;

            ViewGroup.LayoutParams param2 = (ViewGroup.LayoutParams)image2.LayoutParameters;
            param2.Height = 360;
            image2.LayoutParameters = param2;

            image1.Click += Image1_Click;
            image2.Click += Image2_Click;
            image1.SetImageBitmap(BitmapConverter.ConvertStringToBitmap(twoImages.Slika1.Sadrzaj));
            image2.SetImageBitmap(BitmapConverter.ConvertStringToBitmap(twoImages.Slika2.Sadrzaj));
        }
        #endregion
    }

    class UserImagesAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}