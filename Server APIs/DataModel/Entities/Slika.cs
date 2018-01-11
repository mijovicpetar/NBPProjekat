using System.Collections.Generic;
namespace DataModel.Entities
{
    public class Slika : Node
    {
        private string _identificatorName;
        private bool _useInWhereClause;
        private object _identificatorValue;

        #region Properties

        public override string IdentificatorName { get => _identificatorName; set => _identificatorName = value; }
        public override bool UseInWhereClause { get => _useInWhereClause; set => _useInWhereClause = value; }
        public override object IdentificatorValue { get => _identificatorValue; set => _identificatorValue = value; }
       
        public string Opis { get; set; }
        public string Kljuc { get; set; }
        public string Sadrzaj { get; set; }

        #endregion

        #region Constructors

        public Slika()
        {
            _useInWhereClause = false;
        }

        #endregion
    }
}
