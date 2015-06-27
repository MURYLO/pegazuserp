using PegazusERP.Aplicacao.AOP;
using PegazusERP.Dominio.Aggregates.ProdutoAgg;
using PegazusERP.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PegazusERP.Aplicacao.Services.Interface
{
    public interface IMarcaProdutoAppService : IDisposable
    {
       // [Autenticacao]
        MarcaProdutoDTO AddMarcaProduto(MarcaProdutoDTO MarcaProdutoDTO);

       // [Autenticacao]
        void UpdateMarcaProduto(MarcaProdutoDTO MarcaProdutoDTO);

       // [Autenticacao]
        void RemoveMarcaProduto(int MarcaProdutoId);

       // [Autenticacao]
        MarcaProdutoDTO FindMarcaProduto(int MarcaProdutoId);

      //  [Autenticacao]
        List<MarcaProdutoDTO> FindMarcaProdutos<KProperty>(string texto, Expression<Func<MarcaProduto, KProperty>> orderByExpression, bool ascending, int pageIndex, int pageCount);

      //  [Autenticacao]
        List<MarcaProdutoDTO> FindMarcaProdutos<KProperty>(string texto, Expression<Func<MarcaProduto, KProperty>> orderByExpression, bool ascending = true);

       // [Autenticacao]
        long CountMarcaProdutos(string texto);
    }
}
