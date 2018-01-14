using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinedAPI.Entities
{
    public class Profil : Node
    {
        private string _identificatorName;
        private bool _useInWhereClause;
        private string _identificatorValue;

        #region Properties
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Pol { get; set; }
        public string MestoStanovanja { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Lozinka { get; set; }
        public string KorisnickoIme { get; set; }

        public override string IdentificatorName { get => _identificatorName; set => _identificatorName = value; }
        public override bool UseInWhereClause { get => _useInWhereClause; set => _useInWhereClause = value; }
        public override string IdentificatorValue { get => _identificatorValue; set => _identificatorValue = value; }


        #endregion
        #region Constructors
        public Profil()
        {
            DatumRodjenja = new DateTime();

            _identificatorName = "KorisnickoIme";
            _useInWhereClause = false;
        }
        #endregion
    }
}
