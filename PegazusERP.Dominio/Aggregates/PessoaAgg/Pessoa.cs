using PegazusERP.Dominio.Aggregates.EnderecoAgg;
using PegazusERP.Dominio.Base;
using System.Collections.Generic;
using System;
using PegazusERP.Dominio.Enums;

namespace PegazusERP.Dominio.Aggregates.PessoaAgg
{
    public class Pessoa : Entity, IValidator
    {
        #region Propriedades

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string Cnpj { get; set; }

        public string RazaoSocial { get; set; }

        public string NomeFantasia { get; set; }

        public decimal? LimiteCredito { get; set; }

        public string Email { get; set; }

        //INICIALIZAR
        //public virtual ICollection<Endereco> ListaDeEnderecos { get; set; }

        //public virtual ICollection<Contato> ListaDeContato { get; set; }

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
                

        #endregion

        #region Validação

        public IEnumerable<string[]> Validate()
        {
            var validationResults = new List<string[]>();

            if (FisicaJuridica == ePessoa.Fisica && string.IsNullOrEmpty(Nome))
            {
                validationResults.Add(new string[] { "Informe o Nome." ,"Nome"});
            }
            if (Nome.Length > 100)
            {
                validationResults.Add(new string[] { "Nome deve conter no máximo 100 caracteres.", "Nome" });
            }

            if (FisicaJuridica == ePessoa.Juridica && string.IsNullOrEmpty(RazaoSocial))
            {
                validationResults.Add(new string[] { "Informe a Razão Social.", "RazaoSocial" });
            }
            if (Nome.Length > 100)
            {
                validationResults.Add(new string[] { "Nome deve conter no máximo 100 caracteres.", "Nome" });
            }

            if (FisicaJuridica == ePessoa.Fisica && string.IsNullOrEmpty(Cpf))
            {
                validationResults.Add(new string[] { "Informe o CPF", "Cpf" });
            }
            else if (FisicaJuridica == ePessoa.Juridica && string.IsNullOrEmpty(Cnpj))
            {
                validationResults.Add(new string[] { "Informe o CNPJ", "Cnpj" });
            }


            return validationResults;
        }

        #endregion
    }
}
