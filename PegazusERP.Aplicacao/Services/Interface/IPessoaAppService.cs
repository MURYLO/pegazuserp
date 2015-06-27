using PegazusERP.Aplicacao.AOP;
using PegazusERP.Dominio.Aggregates.PessoaAgg;
using PegazusERP.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PegazusERP.Aplicacao.Services.Interface
{
    public interface IPessoaAppService : IDisposable
    {
       // [Autenticacao]
        PessoaDTO AddPessoa(PessoaDTO pessoaDTO);

       // [Autenticacao]
        void UpdatePessoa(PessoaDTO pessoaDTO);

       // [Autenticacao]
        void RemovePessoa(int pessoaId);

       // [Autenticacao]
        PessoaDTO FindPessoa(int pessoaId);

      //  [Autenticacao]
        List<PessoaDTO> FindPessoas<KProperty>(string texto, Expression<Func<Pessoa, KProperty>> orderByExpression, bool ascending, int pageIndex, int pageCount);

      //  [Autenticacao]
        List<PessoaDTO> FindPessoas<KProperty>(string texto, Expression<Func<Pessoa, KProperty>> orderByExpression, bool ascending = true);

       // [Autenticacao]
        long CountPessoas(string texto);
    }
}
