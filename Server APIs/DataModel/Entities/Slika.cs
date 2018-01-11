using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace DataModel.Entities
{
    public class Slika : Node
    {
        private string _identificatorName;
        private bool _useInWhereClause;
        private object _identificatorValue;

        #region Properties
        public override string IdentificatorName { get => _identificatorName; set => _identificatorName = value; }
        public override bool UseInWhereClause { get => _useInWhereClause; set => _useInWhereClause = value; }
        public override object IdentificatorValue { get => _identificatorValue; set => _identificatorValue = value; }
       
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
