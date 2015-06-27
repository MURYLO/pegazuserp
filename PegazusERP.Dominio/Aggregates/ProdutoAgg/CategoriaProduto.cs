using PegazusERP.Dominio.Base;
using System.Collections.Generic;

namespace PegazusERP.Dominio.Aggregates.ProdutoAgg
{
    public class CategoriaProduto:Entity, IValidator
    {
        public string Nome { get; set; }
        
        #region Validação

        public IEnumerable<string[]> Validate()
        {
            var validationResults = new List<string[]>();

            if (string.IsNullOrEmpty(Nome))
            {
                validationResults.Add(new string[] { "Nome é obrigatório.", "Nome" });
            }
            else if (Nome.Length > 60)
            {
                validationResults.Add(new string[] { "Deve conter no máximo 60 caracteres .", "Nome" });
            }

            return validationResults;
        }

        #endregion
    }
}
