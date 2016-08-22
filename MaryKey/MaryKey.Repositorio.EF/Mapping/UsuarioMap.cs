using MaryKey.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaryKey.Repositorio.EF.Mapping
{
    class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            ToTable("usuario");
            HasKey(u => u.Id);
            Property(u => u.Login).IsRequired().HasColumnName("Login").HasMaxLength(200);
            Property(u => u.Senha).IsRequired().HasColumnName("Senha").HasMaxLength(200);

            HasMany(u => u.Permissoes).WithMany(p => p.Usuarios).Map(m =>
            {
                m.ToTable("Usuario_Permissao");
                m.MapLeftKey("IdUsuario");
                m.MapRightKey("IdPermissao");
            });
        }
    }
}
