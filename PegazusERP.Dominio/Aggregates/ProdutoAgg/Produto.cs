using PegazusERP.Dominio.Base;
using PegazusERP.Dominio.Enums;
using System.Collections.Generic;

namespace PegazusERP.Dominio.Aggregates.ProdutoAgg
{
    public class Produto:Entity, IValidator
    {
        public string Nome { get; set; }

        public int? MarcaProdutoId { get; set; }

        public virtual MarcaProduto MarcaProduto { get; set; }

        public int? CategoriaProdutoId { get; set; }

        public virtual CategoriaProduto CategoriaProduto { get; set; }

        public string CodigoBarras { get; set; }

        public bool UsaBalanca { get; set; }

        public bool Ativo { get; set; }

        public int EstoqueAtual { get; set; }

        public string Modelo { get; set; }

        public decimal Custo { get; set; }

        public decimal Venda { get; set; }

        public eUnidade? Unidade { get; set; }

        public bool MovimentaEstoque { get; set; }

        public eTipoNcm? TipoNcm { get; set; }

        public string Ncm { get; set; }

        public eTipoProduto? TipoProduto { get; set; }
        
        public eNaturezaEconomica? NaturezaEconomica { get; set; }

        public eObjetivoComercial? ObjetivoComercial { get; set; }

        public string Referencia { get; set; }

        public string ReferenciaAux { get; set; }

        public string LocalEstoque { get; set; }

        public bool AceitaSaldoNegativo { get; set; }

        public int QuantidadeMinimaEstoque { get; set; }

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
                validationResults.Add(new string[] { "Deve conter no máximo 100 caracteres .", "Nome" });
            }

            if (!CategoriaProdutoId.HasValue)
            {
                 validationResults.Add(new string[] { "Informe a categoria do produto.", "CategoriaProdutoId" });
            }

            if (!Unidade.HasValue)
            {
                validationResults.Add(new string[] { "Informe a unidade do produto.", "Unidade" });
            }

            return validationResults;
        }

        #endregion
    }
}
