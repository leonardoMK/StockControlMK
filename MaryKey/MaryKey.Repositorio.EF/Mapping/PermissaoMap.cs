using MaryKey.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaryKey.Repositorio.EF.Mapping
{
    class PermissaoMap : EntityTypeConfiguration<Permissao>
    {
        public PermissaoMap()
        {
            ToTable("Permissao");
            HasKey(p => p.Id);
            Property(p => p.Nome).IsRequired().HasColumnName("Nome").HasMaxLength(200);
        }
    }
}
