
using PegazusERP.Dominio.Enums;
namespace PegazusERP.DTO
{
    public class ProdutoDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int? MarcaProdutoId { get; set; }

        public int? CategoriaProdutoId { get; set; }

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
    }
}
