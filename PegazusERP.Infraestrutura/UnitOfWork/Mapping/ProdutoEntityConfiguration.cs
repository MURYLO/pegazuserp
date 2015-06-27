using PegazusERP.Dominio.Aggregates.ProdutoAgg;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegazusERP.Infraestrutura.UnitOfWork.Mapping
{
    public class ProdutoEntityConfiguration:EntityTypeConfiguration<Produto>
    {
        public ProdutoEntityConfiguration()
        {
            this.HasKey(c => c.Id);

            this.Property(c => c.Id)
                .HasColumnName("ID_PROD")
                .IsRequired();

            this.Property(c => c.CategoriaProdutoId)
                .HasColumnName("ID_CATE");

            this.Property(c => c.MarcaProdutoId)
                .HasColumnName("ID_MARC");

            this.Property(c => c.Nome)
                .HasColumnName("NOME_PROD")
                .HasMaxLength(60)
                .IsRequired();

            this.Property(c => c.CodigoBarras)
                .HasColumnName("CODBARRAS_PROD");

            this.Property(c => c.UsaBalanca)
                .HasColumnName("USABALANCA_PROD");

            this.Property(c => c.Ativo)
                .HasColumnName("ATIVO_PROD");

            this.Property(c => c.EstoqueAtual)
                .HasColumnName("ESTOQUEATUAL_PROD");

            this.Property(c => c.Modelo)
                .HasMaxLength(100)
                .HasColumnName("MODELO_PROD");

            this.Property(c => c.Custo)
                .HasColumnName("CUSTO_PROD");

            this.Property(c => c.Venda)
                .HasColumnName("VENDA_PROD");

            this.Property(c => c.Unidade)
                .HasColumnName("UNIDADE_PROD")
                .IsRequired();

            this.Property(c => c.MovimentaEstoque)
                .HasColumnName("MOVESTOQUE_PROD");

            this.Property(c => c.TipoNcm)
                .HasColumnName("TIPONCM_PROD");

            this.Property(c => c.Ncm)
                .HasColumnName("NCM_PROD");

            this.Property(c => c.NaturezaEconomica)
                .HasColumnName("NATUREZAECON_PROD");

            this.Property(c => c.TipoProduto)
                .HasColumnName("TIPO_PROD");

            this.Property(c => c.Referencia)
                .HasColumnName("REFERENCIA_PROD");

            this.Property(c => c.ReferenciaAux)
                .HasColumnName("REFAUXLIAR_PROD");

            this.Property(c => c.LocalEstoque)
                .HasColumnName("LOCALESTOQUE_PROD");

            this.Property(c => c.AceitaSaldoNegativo)
                .HasColumnName("ACCSALDONEGATIVO_PROD");

            this.Property(c => c.QuantidadeMinimaEstoque)
                .HasColumnName("QTDEMINESTOQUE_PROD");

            this.Property(c => c.ObjetivoComercial)
                .HasColumnName("OBJETIVOCOMERCIAL_PROD");

           
            this.HasOptional(c => c.CategoriaProduto)
                .WithMany()
                .HasForeignKey(c => c.CategoriaProdutoId)
                .WillCascadeOnDelete(false);

            this.HasOptional(c => c.MarcaProduto)
                .WithMany()
                .HasForeignKey(c => c.MarcaProdutoId)
                .WillCascadeOnDelete(false);

            this.ToTable("PRODUTO");
        }
    }
}
