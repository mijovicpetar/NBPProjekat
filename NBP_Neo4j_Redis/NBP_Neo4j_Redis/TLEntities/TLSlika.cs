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

namespace NBP_Neo4j_Redis.TLEntities
{
    public class TLSlika : Slika
    {
        #region Atributes
        Lokacija _lokacija;
        TLProfil _vlasnik;
        List<TLProfil> _tagovaneOsobe;
        List<TLProfil> _lajkovi;
        #endregion

        #region Properties
        public Lokacija LokacijaSlike { get => _lokacija; set => _lokacija = value; }
        public TLProfil Vlasnik { get => _vlasnik; set => _vlasnik = value; }
        public List<TLProfil> TagovaneOsobe { get => _tagovaneOsobe; set => _tagovaneOsobe = value; }
        public List<TLProfil> Lajkovi1 { get => _lajkovi; set => _lajkovi = value; }
        #endregion

        #region Constructors
        public TLSlika() : base()
        {
            _lokacija = new Lokacija();
            //   _vlasnik = new TLProfil();
            //   _tagovaneOsobe = new List<TLProfil>();
            //   _lajkovi = new List<TLProfil>();
        }
        public TLSlika(Slika slika)
        {
            this.Opis = slika.Opis;
            this.Kljuc = slika.Kljuc;
            this.Sadrzaj = slika.Sadrzaj;
            this.Username = slika.Username;
            this.IdentificatorName = slika.IdentificatorName;
            this.IdentificatorValue = slika.IdentificatorValue;
            this.UseInWhereClause = slika.UseInWhereClause;

        }
        #endregion
        #region Methodes
        public static List<TLSlika> GetTLImages(List<Slika> slike)
        {
            List<TLSlika> slikeTL = new List<TLSlika>();
            if (slike == null)
                return null;
            foreach (Slika s in slike)
                slikeTL.Add(new TLSlika(s));
            return slikeTL;
        }
        public Slika ReturnBaseImage()
        {
            Slika slika = new Slika();
            slika.IdentificatorName = this.IdentificatorName;
            slika.IdentificatorValue = this.IdentificatorValue;
            slika.Kljuc = this.Kljuc;
            slika.Opis = this.Opis;
            slika.Sadrzaj = this.Sadrzaj;
            slika.UseInWhereClause = this.UseInWhereClause;
            slika.Username = this.Username;
            return slika;
        }
        #endregion
    }
}