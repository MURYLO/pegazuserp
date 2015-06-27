using PegazusERP.Aplicacao.AOP;
using PegazusERP.Dominio.Aggregates.UsuarioAgg;
using PegazusERP.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PegazusERP.Aplicacao.Services.Interface
{
    public interface IUsuarioAppService : IDisposable
    {
       // [Autenticacao]
        UsuarioDTO AddUsuario(UsuarioDTO usuarioDTO);

     //   [Autenticacao]
        UsuarioDTO UpdateUsuario(UsuarioDTO usuarioDTO);

      //  [Autenticacao]
        void RemoveUsuario(int usuarioId);

      //  [Autenticacao]
        UsuarioDTO FindUsuario(int usuarioId);

      //  [Autenticacao]
        UsuarioDTO FindUsuario(string email);

      //  [Autenticacao]
        List<UsuarioDTO> FindUsuarios<KProperty>(string texto, Expression<Func<Usuario, KProperty>> orderByExpression, bool ascending, int pageIndex, int pageCount);

      //  [Autenticacao]
        List<UsuarioDTO> FindUsuarios<KProperty>(string texto, Expression<Func<Usuario, KProperty>> orderByExpression, bool ascending = true);

      //  [Autenticacao]
        long CountUsuarios(string texto);

        void AutenticarUsuario(string usuario, string senha);

      //  [Autenticacao]
        void AlterarSenha(string senhaAtual, string novaSenha, string confirmaNovaSenha);
    }
}
