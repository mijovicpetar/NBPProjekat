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
        TLSlika _profilna;
        List<TLSlika> _tagovaneSlike;
        List<TLSlika> _dodateSlike;
        
        public TLSlika Profilna { get => _profilna; set => _profilna = value; }
        public List<TLSlika> TagovaneSlike { get => _tagovaneSlike; set => _tagovaneSlike = value; }
        public List<TLSlika> DodateSlike { get => _dodateSlike; set => _dodateSlike = value; }

        public TLProfil() :base()
        {
            _profilna = new TLSlika();
            _tagovaneSlike = new List<TLSlika>();
            _dodateSlike = new List<TLSlika>();
        }
       
        public TLProfil(Profil profil) :base()
        {
            this.DatumRodjenja = profil.DatumRodjenja;
            this.Ime = profil.Ime;
            this.Prezime = profil.Prezime;
            this.Pol = profil.Pol;
            this.MestoStanovanja = profil.MestoStanovanja;
            this.KorisnickoIme = profil.KorisnickoIme;
            this.Lozinka = profil.Lozinka;

            this.IdentificatorName = profil.IdentificatorName;
            this.IdentificatorValue = profil.IdentificatorValue;
            this.UseInWhereClause = profil.UseInWhereClause;
            _profilna = new TLSlika();
            _tagovaneSlike = new List<TLSlika>();
            _dodateSlike = new List<TLSlika>();
            
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