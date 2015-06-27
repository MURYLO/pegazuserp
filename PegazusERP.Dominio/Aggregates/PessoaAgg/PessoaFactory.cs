
using PegazusERP.Dominio.Enums;
using System;
namespace PegazusERP.Dominio.Aggregates.PessoaAgg
{
    public static class PessoaFactory
    {
        public static Pessoa CreatePessoa(
             string nome,
             string cpf,
             string cnpj,
             string razaoSocial,
             string nomeFantasia,
             decimal? limiteCredito,
             string email,
             DateTime? dataCadastro,
             DateTime? ultimaAtualizacaoCadastro,
             bool ativo,
             int? vendedorId,
             string ie,
             eSexo? sexo,
             string estadoCivil,
             DateTime? dataNascimento,
             string escolaridade,
             string profissao,
             string nacionalidade,
             eTipoPesssoa? tipoPessoa,
             bool compraPrazo,
             bool subISS,
             eObjetivoComercial? objetivoComercial,
             bool retemImpostos,
             ePessoa? fisicaJuridica,        
             decimal? percentualVista,
             decimal? percentualPrazo
            
            )
        {
            var pessoa = new Pessoa();

            pessoa.Nome = nome;
            pessoa.Cpf = cpf;
            pessoa.Cnpj = cnpj;
            pessoa.RazaoSocial = razaoSocial;
            pessoa.NomeFantasia = nomeFantasia;
            pessoa.LimiteCredito = limiteCredito;
            pessoa.Email = email;
            pessoa.DataCadastro = dataCadastro;
            pessoa.UltimaAtualizacaoCadastro = ultimaAtualizacaoCadastro;
            pessoa.Ativo = ativo;
            pessoa.VendedorId = vendedorId;
            pessoa.Ie = ie;
            pessoa.Sexo = sexo;
            pessoa.EstadoCivil = estadoCivil;
            pessoa.DataNascimento = dataNascimento;
            pessoa.Escolaridade = escolaridade;
            pessoa.Profissao = profissao;
            pessoa.Nacionalidade = nacionalidade;
            pessoa.TipoPessoa = tipoPessoa;
            pessoa.CompraPrazo = compraPrazo;
            pessoa.SubISS = subISS;
            pessoa.ObjetivoComercial = objetivoComercial;
            pessoa.RetemImpostos = retemImpostos;
            pessoa.FisicaJuridica = fisicaJuridica;
            pessoa.PercentualVista = percentualVista;
            pessoa.PercentualPrazo = percentualPrazo;

            return pessoa;
        }
    }
}
