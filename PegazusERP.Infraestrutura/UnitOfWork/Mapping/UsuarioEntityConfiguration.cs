using PegazusERP.Dominio.Aggregates.UsuarioAgg;
using System.Data.Entity.ModelConfiguration;

namespace PegazusERP.Infraestrutura.UnitOfWork.Mapping
{
    class UsuarioEntityConfiguration : EntityTypeConfiguration<Usuario>
    {
        public UsuarioEntityConfiguration()
        {
            // Configurando propriedades e chaves
            this.HasKey(c => c.Id);

            this.Property(c => c.Id)
                .HasColumnName("ID_USUA")
                .IsRequired();

            this.Property(c => c.Permissao)
                .HasColumnName("PERMISSAO_USUA")
                .IsRequired();

            this.Property(c => c.NomeUsuario)
                .HasColumnName("NOME_USUA")
                .IsRequired();

            this.Property(c => c.Senha)
                .HasColumnName("SENHA_USUA")
                .IsRequired();

            this.Property(c => c.Ativo)
                .HasColumnName("ATIVO_USUA")
                .IsRequired();

            this.Property(c => c.PessoaId)
                .HasColumnName("ID_PESS")
                .IsRequired();

            this.HasRequired(c => c.Pessoa)
                .WithMany()
                .HasForeignKey(c => c.PessoaId)
                .WillCascadeOnDelete(false);

            //Configurando a Tabela
            this.ToTable("USUARIO");
        }
    }
}
