using SpParametro.DataAccess.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace SpParametro.DataAccess
{
    public class ClienteDAO : IClienteDAO
    {
        private readonly IConexaoSql conexaoSql;

        public ClienteDAO(string connectionString)
        {
            this.conexaoSql = new ConexaoSql(connectionString);
        }

        public Cliente GetClientDetails(int id)
        {
            SortedList<int, ParametrosProcedure> parametros = new SortedList<int, ParametrosProcedure>
            {
                //entrada
                {0, new ParametrosProcedure("@ClientID", id) },

                //saida
                {1, new ParametrosProcedure("@Guid", SqlDbType.VarChar,  100) },
                {2, new ParametrosProcedure("@ClientName", SqlDbType.VarChar, 100) },
                {3, new ParametrosProcedure("@DateCreated", SqlDbType.DateTime, 20) }

            };

            RetornoDb retornoDb = conexaoSql.ExecutaProcedure(parametros, Procedure.GetClientDetails, false);

            return new Cliente(
                retornoDb.ParametroSaida["@Guid"],
                retornoDb.ParametroSaida["@ClientName"],
                Convert.ToDateTime(retornoDb.ParametroSaida["@DateCreated"])
                );
        }
    }
}
