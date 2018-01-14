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
using CombinedAPI.Entities;

namespace NBP_Neo4j_Redis.NecessaryClasses
{
    public class TwoImages
    {
        public Slika Slika1 { get; set; }
        public Slika Slika2 { get; set; }

        public TwoImages(Slika slika1, Slika slika2)
        {
            Slika1 = slika1;
            Slika2 = slika2;
        }
        public TwoImages()
        {
            Slika1 = new Slika();
            Slika2 = new Slika();
        }
    }
}