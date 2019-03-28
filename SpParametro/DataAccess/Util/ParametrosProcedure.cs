using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpParametro.DataAccess
{
    public class ParametrosProcedure
    {
        public ParametrosProcedure(string nome, SqlDbType tipo, int tamanho)
        {
            this.Nome = nome;
            this.TipoDado = tipo;
            this.Direcao = ParameterDirection.Output;      
            this.Tamanho = tamanho;
        }

        public ParametrosProcedure(string nome, object valor)
        {
            this.Nome = nome;           
            this.Direcao = ParameterDirection.Input;
            this.Valor = valor;
        }

        public string Nome { get; set; }
        public SqlDbType TipoDado { get; set; }
        public ParameterDirection Direcao { get; set; }
        public object Valor { get; set; }
        public int Tamanho { get; set; }
    }
}
