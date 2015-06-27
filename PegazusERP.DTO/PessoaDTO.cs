using PegazusERP.Dominio.Aggregates.EnderecoAgg;
using PegazusERP.Dominio.Aggregates.PessoaAgg;
using PegazusERP.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegazusERP.DTO
{
    public class PessoaDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string Cnpj { get; set; }

        public string RazaoSocial { get; set; }

        public string NomeFantasia { get; set; }

        public decimal? LimiteCredito { get; set; }

        public string Email { get; set; }

        //public List<Endereco> ListaDeEnderecos { get;private set; }

        //public List<Contato> ListaDeContato { get; set; }

        public DateTime? DataCadastro { get; set; }

        public DateTime? UltimaAtualizacaoCadastro { get; set; }

        public bool Ativo { get; set; }

        public int? VendedorId { get; set; }

        public string Ie { get; set; }

        public eSexo? Sexo { get; set; }

        public string EstadoCivil { get; set; }

        public DateTime? DataNascimento { get; set; }

        public string Escolaridade { get; set; }

        public string Profissao { get; set; }

        public string Nacionalidade { get; set; }

        public eTipoPesssoa? TipoPessoa { get; set; }

        public bool CompraPrazo { get; set; }

        public bool SubISS { get; set; }

        public eObjetivoComercial? ObjetivoComercial { get; set; }

        public bool RetemImpostos { get; set; }

        public ePessoa? FisicaJuridica { get; set; }

        //Tipo Técnico/vendedor
        public decimal? PercentualVista { get; set; }

        public decimal? PercentualPrazo { get; set; }
                
    }
}
