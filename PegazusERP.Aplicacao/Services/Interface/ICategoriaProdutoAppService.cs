using PegazusERP.Aplicacao.AOP;
using PegazusERP.Dominio.Aggregates.ProdutoAgg;
using PegazusERP.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PegazusERP.Aplicacao.Services.Interface
{
    public interface ICategoriaProdutoAppService : IDisposable
    {
       // [Autenticacao]
        CategoriaProdutoDTO AddCategoriaProduto(CategoriaProdutoDTO CategoriaProdutoDTO);

       // [Autenticacao]
        void UpdateCategoriaProduto(CategoriaProdutoDTO CategoriaProdutoDTO);

       // [Autenticacao]
        void RemoveCategoriaProduto(int CategoriaProdutoId);

       // [Autenticacao]
        CategoriaProdutoDTO FindCategoriaProduto(int CategoriaProdutoId);

      //  [Autenticacao]
        List<CategoriaProdutoDTO> FindCategoriaProdutos<KProperty>(string texto, Expression<Func<CategoriaProduto, KProperty>> orderByExpression, bool ascending, int pageIndex, int pageCount);

      //  [Autenticacao]
        List<CategoriaProdutoDTO> FindCategoriaProdutos<KProperty>(string texto, Expression<Func<CategoriaProduto, KProperty>> orderByExpression, bool ascending = true);

       // [Autenticacao]
        long CountCategoriaProdutos(string texto);
    }
}
