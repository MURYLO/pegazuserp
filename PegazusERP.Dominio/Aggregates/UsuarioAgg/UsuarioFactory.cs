

using PegazusERP.Dominio.Aggregates.Base;
using PegazusERP.Dominio.Enums;

namespace PegazusERP.Dominio.Aggregates.UsuarioAgg
{
    public static class UsuarioFactory
    {
        public static Usuario CreateUsuario(
            ePermissao? permissao,
            string nomeUsuario,
            string senha,
            bool ativo
            )
        {
            var usuario = new Usuario();

            usuario.Permissao = permissao;
            usuario.NomeUsuario = nomeUsuario;
            usuario.Senha = senha;
            usuario.Ativo = ativo;

            return usuario;
        }
    }
}
