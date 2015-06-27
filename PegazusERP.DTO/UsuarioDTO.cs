using PegazusERP.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegazusERP.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        public ePermissao? Permissao { get; set; }

        public string NomeUsuario { get; set; }

        public string Senha { get; set; }

       // public eAtivo? Ativo { get; set; }
        public bool Ativo { get; set; }
    }
}
