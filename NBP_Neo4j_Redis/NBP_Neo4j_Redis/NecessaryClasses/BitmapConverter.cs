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
using Android.Graphics;
using Java.Nio;
using Java.IO;
using System.IO;
using Android.Util;

namespace NBP_Neo4j_Redis
{
    public static class BitmapConverter
    {

        public static Bitmap ConvertStringToBitmap(string sBytes)
        {
            byte[] array = Base64.Decode(sBytes, Base64Flags.Default);
            var imageBitmap = BitmapFactory.DecodeByteArray(array, 0, array.Length);
            return imageBitmap;
        }
        public static string ConvertBitmapToString(Bitmap bitmap)
        {
            byte[] array;
            string sArray;
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                array = stream.ToArray();
                sArray = Convert.ToBase64String(array);
            }
            return sArray;
        }
        

    }
}