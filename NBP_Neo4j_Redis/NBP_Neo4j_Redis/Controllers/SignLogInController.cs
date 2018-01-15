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
using NBP_Neo4j_Redis.TLEntities;
using CombinedAPI;
using CombinedAPI.Entities;
namespace NBP_Neo4j_Redis.Controllers
{
    public class SignLogInController
    {
        #region Singleton
        private static SignLogInController _instance;
        public static SignLogInController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SignLogInController();
                return _instance;
            }
            set => _instance = value;
        }

       
        #endregion
        
        TLProfil _mojProfil;


        public TLProfil MojProfil { get => _mojProfil; set => _mojProfil = value; }

        private SignLogInController()
        {
            MojProfil = new TLProfil();
        }
        public string RegistrujSe()
        {
            Profil profil = MojProfil.ReturnBaseProfile();
            profil.IdentificatorValue = profil.KorisnickoIme;
            RegistrationOutcome result = DataAPI.Instance.Register(profil);
            string stringResult;
            if (result == RegistrationOutcome.FALIURE)
                stringResult = "Neuspesna registracija.";
            else if (result == RegistrationOutcome.USERNAME_IN_USE)
                stringResult = "Korisnicko ime vec postoji.";
            else
            {
                _mojProfil.IdentificatorName = "KorisnickoIme";
                _mojProfil.IdentificatorValue = _mojProfil.KorisnickoIme;
                _mojProfil.Profilna.Opis = "Ovo je moja profilna slika.";
                _mojProfil.Profilna.Username = MojProfil.KorisnickoIme;


                string datum = DateTime.Now.Ticks.ToString();
                _mojProfil.Profilna.Kljuc = MojProfil.Profilna.Username + "_" + datum;                
                _mojProfil.Profilna.UseInWhereClause = true;
                _mojProfil.Profilna.IdentificatorName = "Kljuc";
                _mojProfil.Profilna.IdentificatorValue = MojProfil.Profilna.Kljuc;

                Slika profilnaSlika = MojProfil.Profilna.ReturnBaseImage();
                bool uspesnoDodavanjeSlike=DataAPI.Instance.CreateEntity(profilnaSlika);
                Profilna relationshipProfilna = new Profilna(profilnaSlika, profil);
                bool uspesnoDodavanjePotega=DataAPI.Instance.CreateRelationship(relationshipProfilna);

                if (!uspesnoDodavanjeSlike)
                    stringResult = "Neuspesno dodavanje slike.";
                else
                    return null;
            }
            return stringResult;
        }
        public bool PrijaviSe(string korisnicko, string lozinka)
        {
            bool uspesnaPrijava = false;
            Profil mojProfil = DataAPI.Instance.Login(korisnicko, lozinka);
            if (mojProfil != null)
            {
                mojProfil.IdentificatorName = "KorisnickoIme";
                mojProfil.IdentificatorValue = mojProfil.KorisnickoIme;

                _mojProfil = new TLProfil(mojProfil);
                _mojProfil.Profilna = TLSlika.ReturnProfileImage(_mojProfil);
                _mojProfil.TagovaneSlike = TLSlika.ReturnTagedImages(_mojProfil);
                _mojProfil.DodateSlike = TLSlika.ReturnAddedImages(_mojProfil);
                
                DataController.Instance.OdabraniProfil = _mojProfil;
                uspesnaPrijava = true;
            }
            return uspesnaPrijava;
        }
        public bool UcitajProfilnuSliku(string username)
        {
            Slika profilnaSlika = new Slika();
            profilnaSlika.IdentificatorName = "Username";
            profilnaSlika.IdentificatorValue = username;
            return false;



        }
        public bool PrijaviSe()
        {
            Profil profil =DataAPI.Instance.Login(_mojProfil.KorisnickoIme, _mojProfil.Lozinka);
            if (profil != null)
                return true;
            return false;
        }
    }
}