using PegazusERP.Dominio.Aggregates.ProdutoAgg;
using System.Data.Entity.ModelConfiguration;

namespace PegazusERP.Infraestrutura.UnitOfWork.Mapping
{
    public class MarcaProdutoEntityConfiguration:EntityTypeConfiguration<MarcaProduto>
    {
        public MarcaProdutoEntityConfiguration()
        {
            this.HasKey(c => c.Id);

            this.Property(c => c.Id)
                .HasColumnName("ID_MARC")
                .IsRequired();

            this.Property(c => c.Nome)
                .HasColumnName("NOME_MARC")
                .HasMaxLength(60);

            this.ToTable("MARCA");
        }
    }
}
