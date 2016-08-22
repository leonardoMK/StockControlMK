using MaryKey.Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaryKey.Dominio;

namespace MaryKey.Repositorio.EF.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        public Usuario BuscarPorEmail(string identificacao)
        {
            using (var db = new BaseDeDados())
            {
                return db.Usuario.Include("Permissoes").FirstOrDefault(u => u.Login == identificacao);
            }

        }
    }
}
