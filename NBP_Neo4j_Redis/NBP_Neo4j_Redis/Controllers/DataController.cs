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
    public class DataController
    {

        #region Singleton
        private static DataController _instance;
        public static DataController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DataController();
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        
        #endregion

        Profil _odabraniProfil;

        public Profil OdabraniProfil { get => _odabraniProfil; set => _odabraniProfil = value; }
        #region Constructor
        private DataController()
        {
            OdabraniProfil = new Profil();
        }
        #endregion

    }
}