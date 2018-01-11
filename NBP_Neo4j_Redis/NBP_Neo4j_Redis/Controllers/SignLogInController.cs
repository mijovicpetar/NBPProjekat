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
using NBP_Neo4j_Redis.Entities;
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
        
        Profil _mojProfil;


        public Profil MojProfil { get => _mojProfil; set => _mojProfil = value; }

        private SignLogInController()
        {
            MojProfil = new Profil();
        }
    }
}