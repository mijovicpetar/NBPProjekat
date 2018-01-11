using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Entities
{
    public class Lokacija : Node
    {
        private string _identificatorName;
        private bool _useInWhereClause;
        private object _identificatorValue;
        
        #region Properties     

        public string Drzava { get; set; }
        public string Grad { get; set; }
        public double KoordinataX { get; set; }
        public double KoordinataY { get; set; }
        public string Naziv { get; set; }

        public override string IdentificatorName { get => _identificatorName; set => _identificatorName = value; }
        public override bool UseInWhereClause { get => _useInWhereClause; set => _useInWhereClause = value; }
        public override object IdentificatorValue { get => _identificatorValue; set => _identificatorValue = value; }

        #endregion

        #region Constructors

        public Lokacija()
        {
            _useInWhereClause = false;
        }

        #endregion
    }
}
