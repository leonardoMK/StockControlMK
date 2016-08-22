using MaryKey.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaryKey.Web.MVC.Seguranca
{
    public class UsuarioLogado
    {
        public string Login { get; private set; }

        public string[] Permissoes { get; private set; }

        public UsuarioLogado(Usuario usuario)
        {
            this.Login = usuario.Login;
            this.Permissoes = usuario.Permissoes.Select(p => p.Nome).ToArray();
        }
    }
}