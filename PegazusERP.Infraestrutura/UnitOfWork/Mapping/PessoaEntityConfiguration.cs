using PegazusERP.Dominio.Aggregates.PessoaAgg;
using System.Data.Entity.ModelConfiguration;

namespace PegazusERP.Infraestrutura.UnitOfWork.Mapping
{
    class PessoaEntityConfiguration : EntityTypeConfiguration<Pessoa>
    {
        public PessoaEntityConfiguration()
        {
            // Configurando propriedades e chaves
            this.HasKey(c => c.Id);

            this.Property(c => c.Id)
                .HasColumnName("ID_PESS")
                .IsRequired();

            this.Property(c => c.Nome)
                .HasColumnName("NOME_PESS")
                .IsRequired();

            this.Property(c => c.Cpf)
                .HasColumnName("CPF_PESS");

            this.Property(c => c.Cnpj)
                .HasColumnName("CNPJ_PESS");

            this.Property(c => c.RazaoSocial)
                .HasColumnName("RAZAOSOCIAL_PESS");

            this.Property(c => c.NomeFantasia)
                .HasColumnName("NOMEFANTASIA_PESS");

            this.Property(c => c.LimiteCredito)
                .HasColumnName("LIMITECREDITO_PESS");

            this.Property(c => c.Email)
                .HasColumnName("EMAIL_PESS");

            this.Property(c => c.DataCadastro)
                .HasColumnName("DATACADASTRO_PESS");

            this.Property(c => c.UltimaAtualizacaoCadastro)
                .HasColumnName("ULTIMAATUALICADASTRO_PESS");

            this.Property(c => c.Ativo)
                .HasColumnName("ATIVO_PESS");

            this.Property(c => c.VendedorId)
                .HasColumnName("VENDEDORID_PESS");

            this.Property(c => c.Ie)
                .HasColumnName("IE_PESS");

            this.Property(c => c.Sexo)
                .HasColumnName("SEXO_PESS");

            this.Property(c => c.EstadoCivil)
                .HasColumnName("ESTADOCIVIL_PESS");

            this.Property(c => c.DataNascimento)
                .HasColumnName("DATANASC_PESS");

            this.Property(c => c.Escolaridade)
                .HasColumnName("ESCOLARIDADE_PESS");

            this.Property(c => c.Profissao)
                .HasColumnName("PROFISSAO_PESS");

            this.Property(c => c.Nacionalidade)
                .HasColumnName("NACIONALIDADE_PESS");

            this.Property(c => c.TipoPessoa)
                .HasColumnName("TIPOPESSOA_PESS");

            this.Property(c => c.CompraPrazo)
                .HasColumnName("COMPRAPRAZO_PESS");

            this.Property(c => c.SubISS)
                .HasColumnName("SUBISS_PESS");

            this.Property(c => c.ObjetivoComercial)
                .HasColumnName("OBJCOMERCIAL_PESS");

            this.Property(c => c.RetemImpostos)
                .HasColumnName("RETEMIMPOSTOS_PESS");

            this.Property(c => c.FisicaJuridica)
                .HasColumnName("FISICAJURIDICA_PESS");

            this.Property(c => c.PercentualVista)
                .HasColumnName("PERCENTUALVISTA_PESS");

            this.Property(c => c.PercentualPrazo)
                .HasColumnName("PERCENTUALPRAZO_PESS");

            //Configurando a Tabela
            this.ToTable("PESSOA");
        }
    }
}
