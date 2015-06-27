using PegazusERP.Dominio.Base;
using PegazusERP.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegazusERP.Dominio.Aggregates.PessoaAgg
{
    public class Contato : Entity, IValidator
    {
        public virtual Pessoa Pessoa { get; set; }

        public int PessoaId { get; set; }

        public string numero { get; set; }

        public eTipoContato TipoContato { get; set; }

        #region Validação

        public IEnumerable<string[]> Validate()
        {
            var validationResults = new List<string[]>();

            if (PessoaId < 0)
            {
                validationResults.Add(new string[] { "Valor inválido.", "PessoaId" });
            }

            return validationResults;
        }

        #endregion
    }
}
