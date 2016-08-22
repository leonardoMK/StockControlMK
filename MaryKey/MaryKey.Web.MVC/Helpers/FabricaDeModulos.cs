using MaryKey.Dominio.Repositorio;
using MaryKey.Dominio.Servicos;
using MaryKey.Repositorio.EF.Repositorio;
using MaryKey.Repositorio.EF.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaryKey.Web.MVC.Helpers
{
    public class FabricaDeModulos
    {
        public static IServicoCriptografia CriarServicoCriptografia()
        {
            return new ServicoCriptografia();
        }
        public static IUsuarioRepositorio CriarUsuarioRepositorio()
        {
            return new UsuarioRepositorio();
        }

        public static ServicoAutenticacao CriarServicoAutenticacao()
        {
            return new Dominio.Servicos.ServicoAutenticacao(CriarUsuarioRepositorio(), CriarServicoCriptografia());
        }
    }
}