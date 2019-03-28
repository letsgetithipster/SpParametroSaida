using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpParametro.DataAccess.Util
{
    public class RetornoDb
    {
        public RetornoDb(Dictionary<string, string> parametroSaida, DataTable tabelaSaida)
        {
            this.ParametroSaida = parametroSaida;
            this.TabelaSaida = tabelaSaida;
        }

        public Dictionary<string, string> ParametroSaida { get; private set; }

        public DataTable TabelaSaida { get; private set; }
    }
}
