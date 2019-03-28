using System.Collections.Generic;
using SpParametro.DataAccess;
using SpParametro.DataAccess.Util;

namespace SpParametro
{
    public interface IConexaoSql
    {
        RetornoDb ExecutaProcedure(SortedList<int, ParametrosProcedure> parametrosProcedure, string procedure, bool retornaTabela);
        Cliente StoredProcOutParameter(int clientId);
    }
}