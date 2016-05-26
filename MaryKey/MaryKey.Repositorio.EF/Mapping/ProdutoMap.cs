using MaryKey.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaryKey.Repositorio.EF.Mapping
{
    class ProdutoMap : EntityTypeConfiguration<Produto>
    {
        public ProdutoMap()
        {
            ToTable("produto");
            HasKey(c => c.Id);
            Property(c => c.Nome).IsRequired().HasColumnName("Nome").HasMaxLength(150);
            Property(c => c.PercentualDesconto).IsRequired().HasColumnName("PercentualDesconto");
            Property(c => c.Quantidade).IsOptional().HasColumnName("Quantidade");
            Property(c => c.ValorProduto).IsRequired().HasColumnName("ValorProduto");
        }
    }
}
