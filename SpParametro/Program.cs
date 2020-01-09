using SpParametro.Business;
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

            aplicacao.Iniciar();
        }
    }
}
