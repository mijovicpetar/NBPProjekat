using System;
using System.Collections.Generic;
using DataModel;
using DataModel.Entities;
using Neo4jCommunicator;

namespace TestingNeo4JClient
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
            profil.IdentificatorValue = profil.KorisnickoIme;

            return profil;
        }

        public static Slika CreateSlika()
        {
            Slika slika = new Slika();

            slika.Kljuc = "1";
            slika.Opis = "opis";
            slika.Sadrzaj = "nekistringbytovi";
            slika.UseInWhereClause = false;
            slika.IdentificatorName = "Kljuc";
            slika.IdentificatorValue = slika.Kljuc;

            return slika;
        }

        public static void Main(string[] args)
        {
            Profil profil = CreateProfil();
            Neo4jManager.Instance.GenerateNewNode(profil);

            Slika slika = CreateSlika();
            Neo4jManager.Instance.GenerateNewNode(slika);

            Lajk lajk = new Lajk(profil, slika);
            Neo4jManager.Instance.GenerateNewRelation(lajk);

            profil.UseInWhereClause = true;
            List<Node> paramss = new List<Node>();
            paramss.Add(profil);
            List<Profil> profili = Neo4jManager.Instance.ExecuteMatchQuery<Profil>(paramss);
        }
    }
}
