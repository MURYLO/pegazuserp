using PegazusERP.Dominio.Aggregates.ProdutoAgg;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegazusERP.Infraestrutura.UnitOfWork.Mapping
{
    public class CategoriaProdutoEntityConfiguration:EntityTypeConfiguration<CategoriaProduto>
    {
        public CategoriaProdutoEntityConfiguration()
        {
            this.HasKey(c=>c.Id);

            this.Property(c => c.Id)
                .HasColumnName("ID_CATE")
                .IsRequired();

            this.Property(c => c.Nome)
                .HasColumnName("NOME_CATE")
                .HasMaxLength(60);

            this.ToTable("CATEGORIA");
        }
    }
}
