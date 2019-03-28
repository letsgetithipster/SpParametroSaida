using SpParametro.DataAccess;
using System;

namespace SpParametro.Business
{
    public class Aplicacao
    {
        private readonly IClienteDAO clienteDAO;

        #region Contrutores
        
        public Aplicacao(string connectionString)
        {
            this.clienteDAO = new ClienteDAO(connectionString);
        }

        public Aplicacao(IClienteDAO clienteDAO)
        {
            this.clienteDAO = clienteDAO;
        }

        #endregion

        public void Iniciar()
        {            
            Cliente cliente = clienteDAO.GetClientDetails(1);

            Console.WriteLine(cliente.ToString());
            Console.ReadKey();
        }
    }
}
