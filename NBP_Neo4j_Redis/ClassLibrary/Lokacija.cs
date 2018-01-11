using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Lokacija
    {
        #region Properties

        public string Drzava { get; set; }
        public string Grad { get; set; }
        public double KoordinataX { get; set; }
        public double KoordinataY { get; set; }
        public string Naziv { get; set; }
        public List<Slika> Slike { get; set; }
        public List<Profil> OznaceneOsobe { get; set; }
        #endregion
        #region Constructors
        public Lokacija()
        {
            Slike = new List<Slika>();
            OznaceneOsobe = new List<Profil>();
        }
        #endregion
    }
}
