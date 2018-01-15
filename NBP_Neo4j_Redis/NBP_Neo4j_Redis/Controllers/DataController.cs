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

        TLProfil _odabraniProfil;
        List<string> _profili;

        public TLProfil OdabraniProfil { get => _odabraniProfil; set => _odabraniProfil = value; }

        public List<string> Profili
        {
            get
            {
                if (_profili.Count == 0)
                    _profili = PreuzmiAktivneProfile();
                return _profili;
            }

            set
            {
                _profili = value;
            }
        }
        int _indexOdabranogProfila;

        public int IndexOdabranogProfila
        {
            get
            {
                this.PronadjiProfilLokalno(OdabraniProfil.KorisnickoIme, out _indexOdabranogProfila);
                return _indexOdabranogProfila;
            }
            set
            {
                this._indexOdabranogProfila = value;
            }
        }

        public string KorisnickoOdabranogProfila
        {
            get; set;
        }

        #region Constructor
        private DataController()
        {
            OdabraniProfil = new TLProfil();
            _profili = new List<string>();
        }
        #endregion

        public List<string> PreuzmiAktivneProfile()
        {
            List<string> lista = DataAPI.Instance.GetAllActiveUsersUsernames();

            return lista;
        }

        public string PronadjiProfilLokalno(string korisicko, out int indexProfila)
        {
            indexProfila = 0;
            for (int i = 0; i < this.Profili.Count; i++)
            {
                string[] podaci = Profili[i].Split(' ');
                if (podaci[0] == korisicko)
                {
                    indexProfila = i;
                    return Profili[i];
                }
            }
            return null;
        }

        public TLProfil VratiOdabraniProfil()
        {
            Profil profil = new Profil();
            profil.IdentificatorName = "KorisnickoIme";
            profil.IdentificatorValue = KorisnickoOdabranogProfila;
            Profil temp = DataAPI.Instance.GetEntity<Profil>(profil)[0];
            TLProfil odabrani = new TLProfil(temp);
            List<Slika> slikeTemp = DataAPI.Instance.GetAllPicturesForProfile(temp);
            odabrani.DodateSlike = TLSlika.GetTLImages(slikeTemp);

            return odabrani;

        }
    }
}