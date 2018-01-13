using System;
using CombinedAPI;
using CombinedAPI.Entities;

namespace TestingCombinedCommunicator
{
    class MainClass
    {
        public static Profil CreateProfil()
        {
            Profil profil = new Profil();
            profil.DatumRodjenja = DateTime.Now;
            profil.Ime = "Petar";
            profil.KorisnickoIme = "mijovicpetar";
            profil.Lozinka = "123";
            profil.MestoStanovanja = "Nis";
            profil.Pol = "M";
            profil.Prezime = "Mijovic";

            profil.IdentificatorName = "KorisnickoIme";
            profil.IdentificatorValue = "mijovicpetar";

            return profil;
        }

        public static Slika CreateSlika()
        {
            Slika slika = new Slika();
            slika.Kljuc = "1";
            slika.Opis = "slicka";
            slika.Sadrzaj = "12312312";
            slika.Username = "mijovicpetar";

            slika.IdentificatorName = "Kljuc";
            slika.IdentificatorValue = slika.Kljuc;

            return slika;
        }

        public static void Main(string[] args)
        {
            CombinedAPI.DataAPI.Instance.DeleteEvrything();

            Profil profil = CreateProfil();

            DataAPI.Instance.Register(profil);
            profil = DataAPI.Instance.Login("mijovicpetar", "123");

            profil.IdentificatorName = "KorisnickoIme";
            profil.IdentificatorValue = "mijovicpetar";

            var res1 = DataAPI.Instance.GetAllUsersUsernames();
            var res2 = DataAPI.Instance.GetAllActiveUsersUsernames();
            DataAPI.Instance.Logout(profil);
            var res3 = DataAPI.Instance.GetAllActiveUsersUsernames();

            Slika slika = CreateSlika();
            DataAPI.Instance.CreateEntity(slika);

            Lajk lajk = new Lajk(profil, slika);
            DataAPI.Instance.CreateRelationship(lajk);

            var res4 = DataAPI.Instance.GetAllLikesForPhoto(slika);
            var res5 = DataAPI.Instance.GetAllPicturesForProfile(profil);
        }
    }
}
