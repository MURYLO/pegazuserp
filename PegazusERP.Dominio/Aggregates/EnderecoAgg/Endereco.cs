using PegazusERP.Dominio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PegazusERP.Dominio.Enums;

namespace PegazusERP.Dominio.Aggregates.EnderecoAgg
{
    public class Endereco:Entity, IValidator
    {
        public eTipoEndereco TipoEndereco { get; set; }

        public string Logradouro { get; set; }

        public int Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public eEstado Uf { get; set; }

        public string Cep { get; set; }

        public string Referencia { get; set; }

        #region Validação

        public IEnumerable<string[]> Validate()
        {
            var validationResults = new List<string[]>();

            if (false)
            {
                validationResults.Add(new string[] { "Valor inválido." });
            }

            return validationResults;
        }

        #endregion

    }
}
