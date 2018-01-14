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
        Profil _vlasnik;
        List<Profil> _tagovaneOsobe;
        List<Profil> _lajkovi;

        public Lokacija LokacijaSlike { get => _lokacija; set => _lokacija = value; }
        public Profil Vlasnik { get => _vlasnik; set => _vlasnik = value; }
        public List<Profil> TagovaneOsobe { get => _tagovaneOsobe; set => _tagovaneOsobe = value; }
        public List<Profil> Lajkovi1 { get => _lajkovi; set => _lajkovi = value; }

        public TLSlika() : base()
        {
            _lokacija = new Lokacija();
            _vlasnik = new Profil();
            _tagovaneOsobe = new List<Profil>();
            _lajkovi = new List<Profil>();
        }
    }
}