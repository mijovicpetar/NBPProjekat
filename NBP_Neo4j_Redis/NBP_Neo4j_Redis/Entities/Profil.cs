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

namespace NBP_Neo4j_Redis.Entities
{
    public class Profil
    {
        #region Properties
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Pol { get; set; }
        public string MestoStanovanja { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Lozinka { get; set; }
        public string KorisnickoIme { get; set; }
        public List<Slika> ObjavljeneSlike { get; set; }
        public List<Slika> TagovaneSlike { get; set; }
        public Slika Profilna { get; set; }

        #endregion
        #region Constructors
        public Profil()
        {
            ObjavljeneSlike = new List<Slika>();
            TagovaneSlike = new List<Slika>();
            Profilna = new Slika();
            DatumRodjenja = new DateTime();
        }
        #endregion
    }
}