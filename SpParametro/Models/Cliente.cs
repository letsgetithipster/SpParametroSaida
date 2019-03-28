using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpParametro
{
    public class Cliente
    {
        public Cliente(string guid, string clientName, DateTime dateCreated)
        {
            this.Guid = guid;
            this.ClientName = clientName;
            this.DateCreated = dateCreated;
        }

        public string Guid { get; private set; }
        public string ClientName { get; private set; }
        public DateTime DateCreated { get; private set; }

        public override string ToString()
        {
            return string.Format($"Cliente: {ClientName}; {Environment.NewLine}Data Criação: {DateCreated}; {Environment.NewLine}Guid: {Guid}");
        }
    }
}
