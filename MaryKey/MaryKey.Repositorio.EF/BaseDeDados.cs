﻿using MaryKey.Dominio;
using MaryKey.Repositorio.EF.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace MaryKey.Repositorio.EF
{
    public class BaseDeDados : DbContext
    {
        public BaseDeDados() : base("MARYKEY")
        {
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Permissao> Permissao { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<BaseDeDados>(null);
            modelBuilder.Configurations.Add(new ProdutoMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new PermissaoMap());
            base.OnModelCreating(modelBuilder);
        }

    }
}
