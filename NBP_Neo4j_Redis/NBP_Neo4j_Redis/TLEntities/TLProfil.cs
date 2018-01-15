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
        public TLProfil(Profil p)
        {
            this.DatumRodjenja = p.DatumRodjenja;
            this.IdentificatorName = p.IdentificatorName;
            this.IdentificatorValue = p.IdentificatorValue;
            this.Ime = p.Ime;
            this.KorisnickoIme = p.KorisnickoIme;
            this.Lozinka = p.Lozinka;
            this.MestoStanovanja = p.MestoStanovanja;
            this.Pol = p.Pol;
            this.Prezime = p.Prezime;
            this.UseInWhereClause = p.UseInWhereClause;

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