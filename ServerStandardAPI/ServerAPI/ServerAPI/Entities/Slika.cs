using System.Collections.Generic;

namespace CombinedAPI.Entities
{
    public class Slika : Node
    {
        private string _identificatorName;
        private bool _useInWhereClause;
        private string _identificatorValue;

        #region Properties

        public override string IdentificatorName { get => _identificatorName; set => _identificatorName = value; }
        public override bool UseInWhereClause { get => _useInWhereClause; set => _useInWhereClause = value; }
        public override string IdentificatorValue { get => _identificatorValue; set => _identificatorValue = value; }
       
        public string Opis { get; set; }
        public string Kljuc { get; set; }
        public string Sadrzaj { get; set; }
        public string Username { get; set; }

        #endregion

        #region Constructors

        public Slika()
        {
            _useInWhereClause = false;
            _identificatorName = "Kljuc";
            _identificatorValue = "";
            Opis = "";
            Username = "";
            Sadrzaj = "";

        }

        #endregion
    }
}
