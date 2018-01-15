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
            string stringResult="";
            if (result == RegistrationOutcome.FALIURE)
                stringResult = "Neuspesna registracija.";
            else if (result == RegistrationOutcome.USERNAME_IN_USE)
                stringResult = "Korisnicko ime vec postoji.";
            else
            {
                SignLogInController.Instance.MojProfil.Profilna.Opis = "Ovo je moja profilna slika.";
                SignLogInController.Instance.MojProfil.Profilna.Username = SignLogInController.Instance.MojProfil.KorisnickoIme;
                Slika profilnaSlika = MojProfil.Profilna.ReturnBaseImage();
                


                bool uspesnoDodavanjeSlike=DataAPI.Instance.CreateEntity(profilnaSlika);
                Profilna relationshipProfilna = new Profilna(profil, profilnaSlika);
                bool uspesnoDodavanjePotega=DataAPI.Instance.CreateRelationship(relationshipProfilna);

                if (!uspesnoDodavanjeSlike)
                    stringResult = "Neuspesno dodavanje slike.";
            }
            return stringResult;
        }
        public bool PrijaviSe(string korisnicko, string lozinka)
        {
            bool uspesnaPrijava = false;
            Profil mojProfil = DataAPI.Instance.Login(korisnicko, lozinka);
            if (mojProfil != null)
            {
                _mojProfil = new TLProfil(mojProfil);

                _mojProfil.DodateSlike =TLSlika.GetTLImages(DataAPI.Instance.GetAllPicturesForProfile(mojProfil));

                if (_mojProfil.DodateSlike.Count > 0)
                    _mojProfil.Profilna = _mojProfil.DodateSlike[0];


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
    }
}