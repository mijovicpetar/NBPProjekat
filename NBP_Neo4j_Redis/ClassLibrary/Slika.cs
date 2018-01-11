using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Slika
    {
        #region Properties
        public List<Profil> Osobe { get; set; }
        public string Opis { get; set; }
        public string Kljuc { get; set; }
      //  public Bitmap Sadrzaj { get; set; }
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
        #endregion
    }
}
