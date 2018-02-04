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
using NBP_Neo4j_Redis.TLEntities;
namespace NBP_Neo4j_Redis.NecessaryClasses
{
    public class TwoImages
    {
        #region Properties
        public TLSlika Slika1 { get; set; }
        public TLSlika Slika2 { get; set; }
        #endregion

        #region Constructors
        public TwoImages(TLSlika slika1, TLSlika slika2)
        {
            Slika1 = slika1;
            Slika2 = slika2;
        }
        public TwoImages()
        {
            Slika1 = new TLSlika();
            Slika2 = new TLSlika();
        }
        #endregion
    }
}