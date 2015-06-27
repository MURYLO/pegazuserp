using PegazusERP.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegazusERP.Dominio.Aggregates.ProdutoAgg
{
    public static class ProdutoFactory
    {
        public static MarcaProduto CreateMarcaProduto(
                string nome
            )
        {
            var marca = new MarcaProduto();

            marca.Nome = nome;

            return marca;
        }

        public static CategoriaProduto CreateCategoriaProduto(
                string nome 
            )
        {
            var categoria = new CategoriaProduto();

            categoria.Nome = nome;

            return categoria;
        }

        public static Produto CreateProduto(
            string nome,
            int? marcaProdutoId,
            int? categoriaProdutoId,
            string codigoBarras,
            bool usaBalanca,
            bool ativo,
            int estoqueAtual,
            string modelo,
            decimal custo,
            decimal venda,
            eUnidade? unidade,
            bool movimentaEstoque,
            eTipoNcm? tipoNcm,
            string ncm,
            eNaturezaEconomica? naturezaEconomica,
            eTipoProduto? tipoProduto,
            eObjetivoComercial? objetivoComercial,
            string referencia,
            string referenciaAux,
            string localEstoque,
            bool aceitaSaldoNegativo,
            int quantidadeMinimaEstoque

        )
        {
            var produto = new Produto();

            produto.Nome = nome;
            produto.MarcaProdutoId = marcaProdutoId;
            produto.CategoriaProdutoId = categoriaProdutoId;
            produto.CodigoBarras = codigoBarras;
            produto.UsaBalanca = usaBalanca;
            produto.Ativo = ativo;
            produto.EstoqueAtual = estoqueAtual;
            produto.Modelo = modelo;
            produto.Custo = custo;
            produto.Venda = venda;
            produto.Unidade = unidade;
            produto.MovimentaEstoque = movimentaEstoque;
            produto.TipoNcm = tipoNcm;
            produto.Ncm = ncm;
            produto.NaturezaEconomica = naturezaEconomica;
            produto.TipoProduto = tipoProduto;
            produto.ObjetivoComercial = objetivoComercial;
            produto.Referencia = referencia;
            produto.ReferenciaAux = referenciaAux;
            produto.LocalEstoque = localEstoque;
            produto.AceitaSaldoNegativo = aceitaSaldoNegativo;
            produto.QuantidadeMinimaEstoque = quantidadeMinimaEstoque;

            return produto;
        }
    }
}
