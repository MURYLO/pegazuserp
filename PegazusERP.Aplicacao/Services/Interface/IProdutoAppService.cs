using PegazusERP.Aplicacao.AOP;
using PegazusERP.Dominio.Aggregates.ProdutoAgg;
using PegazusERP.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PegazusERP.Aplicacao.Services.Interface
{
    public interface IProdutoAppService : IDisposable
    {
       // [Autenticacao]
        ProdutoDTO AddProduto(ProdutoDTO produtoDTO);

       // [Autenticacao]
        void UpdateProduto(ProdutoDTO produtoDTO);

       // [Autenticacao]
        void RemoveProduto(int produtoId);

       // [Autenticacao]
        ProdutoDTO FindProduto(int produtoId);

      //  [Autenticacao]
        List<ProdutoDTO> FindProdutos<KProperty>(string texto, Expression<Func<Produto, KProperty>> orderByExpression, bool ascending, int pageIndex, int pageCount);

      //  [Autenticacao]
        List<ProdutoDTO> FindProdutos<KProperty>(string texto, Expression<Func<Produto, KProperty>> orderByExpression, bool ascending = true);

       // [Autenticacao]
        long CountProdutos(string texto);
    }
}
