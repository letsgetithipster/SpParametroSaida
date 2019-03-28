using SpParametro.DataAccess;
using SpParametro.DataAccess.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SpParametro
{
    public class ConexaoSql : IConexaoSql
    {
        private readonly string connectionString;

        public ConexaoSql(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private SqlConnection ObterConexao()
        {
            return new SqlConnection(connectionString);
        }

        public RetornoDb ExecutaProcedure(SortedList<int, ParametrosProcedure> parametrosProcedure, string procedure, bool retornaTabela)
        {
            using (SqlConnection conexao = ObterConexao())
            {
                var comando = conexao.CreateCommand();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = procedure;

                foreach (var parametro in parametrosProcedure)
                {
                    if (parametro.Value.Direcao.Equals(ParameterDirection.Output))
                    {
                        SqlParameter sqlParameter = comando.CreateParameter();
                        sqlParameter.ParameterName = parametro.Value.Nome;
                        sqlParameter.Direction = parametro.Value.Direcao;
                        sqlParameter.SqlDbType = parametro.Value.TipoDado;
                        sqlParameter.Size = parametro.Value.Tamanho;

                        comando.Parameters.Add(sqlParameter);
                    }
                    else if (parametro.Value.Direcao.Equals(ParameterDirection.Input))
                    {
                        comando.Parameters.AddWithValue(parametro.Value.Nome, parametro.Value.Valor);
                    }
                }

                conexao.Open();

                var tabelaSaida = new DataTable();

                if (retornaTabela)
                {
                    var reader = comando.ExecuteReader();
                    tabelaSaida.Load(reader);
                }
                else
                {
                    comando.ExecuteNonQuery();
                }

                var parametrosSaida = new Dictionary<string, string>();
                foreach (var parametro in parametrosProcedure)
                {
                    if (parametro.Value.Direcao.Equals(ParameterDirection.Output))
                    {
                        parametrosSaida.Add(parametro.Value.Nome, comando.Parameters[parametro.Value.Nome].Value.ToString());
                    }
                }

                return new RetornoDb(parametrosSaida, tabelaSaida);
            }
        }

        public Cliente StoredProcOutParameter(int clientId)
        {
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetClientDetails";

            cmd.Parameters.AddWithValue("@ClientID", clientId);

            cmd.Parameters.Add("@Guid", SqlDbType.VarChar, 100);
            cmd.Parameters["@Guid"].Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ClientName", SqlDbType.VarChar, 100);
            cmd.Parameters["@ClientName"].Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@DateCreated", SqlDbType.DateTime, 20);
            cmd.Parameters["@DateCreated"].Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                //Storing the output parameters value in 3 different variables.  
                string clientGuid = Convert.ToString(cmd.Parameters["@Guid"].Value);
                string clientName = Convert.ToString(cmd.Parameters["@ClientName"].Value);
                DateTime dateCreated = Convert.ToDateTime(cmd.Parameters["@DateCreated"].Value);
                // Here we get all three values from database in above three variables.  

                return new Cliente(clientGuid, clientName, dateCreated);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
