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
        Lokacija _lokacija;
        TLProfil _vlasnik;
        List<TLProfil> _tagovaneOsobe;
        List<TLProfil> _lajkovi;

        public Lokacija LokacijaSlike { get => _lokacija; set => _lokacija = value; }
        public TLProfil Vlasnik { get => _vlasnik; set => _vlasnik = value; }
        public List<TLProfil> TagovaneOsobe { get => _tagovaneOsobe; set => _tagovaneOsobe = value; }
        public List<TLProfil> Lajkovi1 { get => _lajkovi; set => _lajkovi = value; }

        public TLSlika() : base()
        {
            _lokacija = new Lokacija();
         //   _vlasnik = new TLProfil();
         //   _tagovaneOsobe = new List<TLProfil>();
         //   _lajkovi = new List<TLProfil>();
        }
    }
}