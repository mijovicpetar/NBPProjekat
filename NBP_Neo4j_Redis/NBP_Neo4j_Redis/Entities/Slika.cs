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
namespace NBP_Neo4j_Redis.Entities
{
    public class Slika
    {
        #region Properties
        public List<Profil> Osobe { get; set; }
        public string Opis { get; set; }
        public string Kljuc { get; set; }
        public Bitmap Sadrzaj { get; set; }
        public Lokacija Lokacija { get; set; }
        public List<Profil> Lajkovi { get; set; }
        #endregion
        #region Constructors
        public Slika()
        {
            Osobe = new List<Profil>();
            Lajkovi = new List<Profil>();
            Lokacija = new Lokacija();
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="bitmap"></param>
        public Slika(Bitmap bitmap)
        {
            Sadrzaj = bitmap;
            Osobe = new List<Profil>();
            Lajkovi = new List<Profil>();
            Lokacija = new Lokacija();
        }
        #endregion
    }
}