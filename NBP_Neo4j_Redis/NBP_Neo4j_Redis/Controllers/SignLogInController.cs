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
                stringResult = null;
            return stringResult;
        }
    }
}