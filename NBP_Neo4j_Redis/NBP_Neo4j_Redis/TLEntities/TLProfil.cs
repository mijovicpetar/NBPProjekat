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
    public class TLProfil : Profil
    {
        Slika _profilna;
        List<Slika> _tagovaneSlike;
        List<Slika> _dodateSlike;
        
        public Slika Profilna { get => _profilna; set => _profilna = value; }
        public List<Slika> TagovaneSlike { get => _tagovaneSlike; set => _tagovaneSlike = value; }
        public List<Slika> DodateSlike { get => _dodateSlike; set => _dodateSlike = value; }

        public TLProfil() :base()
        {
            _profilna = new Slika();
            _tagovaneSlike = new List<Slika>();
            _dodateSlike = new List<Slika>();
        }
        public Profil ReturnBaseProfile()
        {
            Profil profil = new Profil();
            profil.DatumRodjenja = this.DatumRodjenja;
            profil.Ime = this.Ime;
            profil.Prezime = this.Prezime;
            profil.KorisnickoIme = this.KorisnickoIme;
            profil.Lozinka = this.Lozinka;
            profil.Pol = this.Pol;
            profil.MestoStanovanja = this.MestoStanovanja;
            profil.IdentificatorName = this.IdentificatorName;
            profil.IdentificatorValue = this.IdentificatorValue;
            profil.UseInWhereClause = this.UseInWhereClause;
            
            return profil;
        }


    }
}