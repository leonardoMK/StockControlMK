using MaryKey.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaryKey.Repositorio.EF.Repositorio
{
    public class ProdutoRepositorio
    {
        BaseDeDados bd;
        private BaseDeDados CriarBaseDeDados()
        {
            return bd = new BaseDeDados();
        }

        public int AddProduct(Produto p)
        {
            using(CriarBaseDeDados())
            {
                bd.Entry(p).State = EntityState.Added;
                return bd.SaveChanges();
            }
        }

        public int Atualizar(Produto p)
        {
            using(CriarBaseDeDados())
            {
                bd.Entry(p).State = EntityState.Modified;
                return bd.SaveChanges();
            }
        }

        public int RemoverUmItem(Produto p)
        {
           
            using (CriarBaseDeDados())
            {
                bd.Entry(p).State = EntityState.Modified;
                return bd.SaveChanges();
            }
        }

        public Produto buscarPorId(int Id)
        {
            using(CriarBaseDeDados())
            {
                return bd.Produto.Find(Id);
            }
        }
        public IList<Produto> BuscarTodos()
        {
            using (BaseDeDados bd = new BaseDeDados())
            {
                var produtos = bd.Produto.Where(p => p.Nome!= null).ToList();
                return produtos;
            }
        }
        public IList<Produto> BuscarPorNome(String nome)
        {
            Type _Hack = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
            using (this.CriarBaseDeDados())
            {
                return bd.Produto.Where(p => p.Nome.Contains(nome)).ToList();
            }
        }

        public int Excluir(int id)
        {
            using (this.CriarBaseDeDados())
            {
                var produto = bd.Produto.Where(c => c.Id == id).First();
                bd.Entry(produto).State = EntityState.Deleted;
                return bd.SaveChanges();
            }
        }

    }
}
