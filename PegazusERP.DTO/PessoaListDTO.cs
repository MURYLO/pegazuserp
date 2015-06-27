using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegazusERP.DTO
{
    public class PessoaListDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public DateTime? DataCadastro { get; set; }

        public bool Ativo { get; set; }

        public string NomeFantasia { get; set; }

        public string Cpf { get; set; }

        public string Cnpj { get; set; }
    }
}
