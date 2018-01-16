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
    public enum TypeOfImages
    {
        AddedImages,
        TagedImages
    }
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
        TLSlika _odabranaSlika;
        List<string> _profili;
        private TypeOfImages _typeOfSelectedImages;

        public TLProfil OdabraniProfil { get => _odabraniProfil; set => _odabraniProfil = value; }
        public TLSlika OdabranaSlika { get => _odabranaSlika; set => _odabranaSlika = value; }

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

        List<string> _profili_lajkovi;
        public List<string> ProfiliLajkovi
        {
            get
            {
                if (_profili_lajkovi.Count == 0)
                    _profili_lajkovi = PreuzmiProfileKojiSuLajkovaliSliku();
                return _profili_lajkovi;
            }
            set
            {
                _profili_lajkovi = value;
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
        public TypeOfImages TypeOfSelectedImages { get => _typeOfSelectedImages; set => _typeOfSelectedImages = value; }



        #region Constructor
        private DataController()
        {
            OdabraniProfil = new TLProfil();
            _profili = new List<string>();
        }
        #endregion

        public List<string> PreuzmiAktivneProfile()
        {
            List<string> lista = DataAPI.Instance.GetAllActiveUserDetails();
          
            for (int i = 0; i < lista.Count; i++)
            {
                string[] ss = lista[i].Split(' ');
                if (ss[0] == OdabraniProfil.KorisnickoIme)
                {
                    lista.RemoveAt(i);
                    break;
                }
            }
            return lista;
        }

        public List<string> PreuzmiProfileKojiSuLajkovaliSliku()
        {
            List<Profil> temp = DataAPI.Instance.GetAllLikesForPhoto(OdabranaSlika.ReturnBaseImage());
            List<string> profili = new List<string>();
            foreach (Profil p in temp)
            {
                string s = p.KorisnickoIme + " " + p.Ime + " " + p.Prezime;
                profili.Add(s);
            }
            return profili;
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

        public string PronadjiProfilLokalno1(string korisnicko, out int indexProfila)
        {
            indexProfila = 0;
            for (int i = 0; i < this.ProfiliLajkovi.Count; i++)
            {
                string[] podaci = ProfiliLajkovi[i].Split(' ');
                if (podaci[0] == korisnicko)
                {
                    indexProfila = i;
                    return ProfiliLajkovi[i];
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
            odabrani.Profilna = TLSlika.ReturnProfileImage(odabrani);
            odabrani.TagovaneSlike = TLSlika.ReturnTagedImages(odabrani);
            odabrani.DodateSlike = TLSlika.ReturnAddedImages(odabrani);

            return odabrani;

        }

        public void NadjiOdabranuSliku(string kljuc)
        {
            List<TLSlika> odabraneSlike;
            if (_typeOfSelectedImages == TypeOfImages.AddedImages)
                odabraneSlike = _odabraniProfil.DodateSlike;
            else
                odabraneSlike = _odabraniProfil.TagovaneSlike;
            foreach (TLSlika odabranaSlika in odabraneSlike)
            {
                if (odabranaSlika.Kljuc == kljuc)
                {
                    _odabranaSlika = odabranaSlika;
                }
            }
        }
    }
}