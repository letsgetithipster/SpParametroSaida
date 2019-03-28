using SpParametro.Business;
using SpParametro.DataAccess;
using System;
using System.Configuration;


namespace SpParametro
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            Iniciar(connectionString);
        }

        private static void Iniciar(string connectionString)
        {
            Aplicacao aplicacao = new Aplicacao(connectionString);

            try
            {
                aplicacao.Iniciar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
