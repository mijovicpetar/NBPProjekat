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
using Android.Graphics;

namespace NBP_Neo4j_Redis.Controllers
{
    #region Enums
    public enum TypeOfImages
    {
        AddedImages,
        TagedImages
    }
    #endregion
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

        #region Atributes
        TLProfil _odabraniProfil;
        TLSlika _odabranaSlika;
        List<string> _profili;
        List<string> _profili_lajkovi;
        private TypeOfImages _typeOfSelectedImages;
        int _indexOdabranogProfila;
        #endregion

        #region Properties
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
        public TypeOfImages TypeOfSelectedImages { get => _typeOfSelectedImages; set => _typeOfSelectedImages = value; }
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

        int _indexOdabranogProfilaLajk;
        public int IndexOdabranogProfilaLajk
        {
            get
            {
                this.PronadjiProfilLokalno1(OdabraniProfil.KorisnickoIme, out _indexOdabranogProfila);
                return _indexOdabranogProfilaLajk;
            }
            set
            {
                this._indexOdabranogProfilaLajk = value;
            }
        }

        public string KorisnickoOdabranogProfila
        {
            get; set;
        }
        #endregion


        #region Constructor
        private DataController()
        {
            OdabraniProfil = new TLProfil();
            _profili = new List<string>();
            _profili_lajkovi = new List<string>();
        }
        #endregion

        #region Methodes
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
                    _profili_lajkovi.Clear();
                }
            }
        }
        public bool DodajNovuSliku(Bitmap sadrzajSlike)
        {
            string imageString = BitmapConverter.ConvertBitmapToString(sadrzajSlike);

            Slika image = new Slika();
            image.Sadrzaj = imageString;
            image.Kljuc = SignLogInController.Instance.MojProfil.KorisnickoIme + "_" + DateTime.Now.Ticks.ToString();
            image.UseInWhereClause = true;
            image.Username = SignLogInController.Instance.MojProfil.KorisnickoIme;
            image.Opis = "";
            image.IdentificatorName = "Username";
            image.IdentificatorValue = image.Username;

            bool uspesnoDodavanje= DataAPI.Instance.CreateEntity(image);
            if (uspesnoDodavanje)
            {
                TLSlika slika = new TLSlika(image);
                DataController.Instance.OdabranaSlika = slika;
                _profili_lajkovi.Clear();
            }

            SignLogInController.Instance.MojProfil.DodateSlike.Insert(0, new TLSlika(image));

            return uspesnoDodavanje;
        }
        

        public bool IzmeniOdabranuSliku()
        {
            OdabranaSlika.IdentificatorName = "Kljuc";
            OdabranaSlika.IdentificatorValue = OdabranaSlika.Kljuc;
            return DataAPI.Instance.EditEntity(OdabranaSlika.ReturnBaseImage());
        }

        public void ObrisiOdabranuSliku()
        {
            bool uspesno = DataAPI.Instance.DeleteEntity(OdabranaSlika);
            OdabraniProfil.DodateSlike.Remove(OdabranaSlika);
            OdabranaSlika = null;
            
        }

        public void IzmeniLokaciju()
        {
            Lokacija lokacija = OdabranaSlika.LokacijaSlike;
            lokacija.Drzava = "";
            lokacija.Grad = "";
            lokacija.IdentificatorName = "Naziv";
            lokacija.IdentificatorValue = lokacija.Naziv;
            lokacija.KoordinataX = 0;
            lokacija.KoordinataX = 0;
            lokacija.UseInWhereClause = true;


            DataAPI.Instance.CreateLocationIfNeeded(lokacija);
            LokacijaSlike lokacijaSlike = new LokacijaSlike(OdabranaSlika.ReturnBaseImage(), lokacija);
            DataAPI.Instance.CreateRelationship(lokacijaSlike);
        }

        public void NapraviLajk()
        {
            string lajks = SignLogInController.Instance.MojProfil.KorisnickoIme + " " + SignLogInController.Instance.MojProfil.Ime;
            bool postoji = false;
            foreach (var like in _profili_lajkovi)            
                if (like.Contains(lajks))                
                    postoji = true;
            
            if (!postoji)
            {
                OdabranaSlika.IdentificatorName = "Kljuc";
                OdabranaSlika.IdentificatorValue = OdabranaSlika.Kljuc;
                SignLogInController.Instance.MojProfil.IdentificatorName = "KorisnickoIme";
                SignLogInController.Instance.MojProfil.IdentificatorValue = SignLogInController.Instance.MojProfil.KorisnickoIme;
                Lajk lajk = new Lajk(SignLogInController.Instance.MojProfil.ReturnBaseProfile(), OdabranaSlika.ReturnBaseImage());
                bool uspesno = DataAPI.Instance.CreateRelationship(lajk);
            }
        }
      #endregion
    }
}