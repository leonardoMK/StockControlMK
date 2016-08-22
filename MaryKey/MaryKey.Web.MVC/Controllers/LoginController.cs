using MaryKey.Dominio;
using MaryKey.Dominio.Servicos;
using MaryKey.Web.MVC.Helpers;
using MaryKey.Web.MVC.Models;
using MaryKey.Web.MVC.Seguranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaryKey.Web.MVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                ServicoAutenticacao servicoAutenticacao = FabricaDeModulos.CriarServicoAutenticacao();

                Usuario usuarioAutenticado = servicoAutenticacao.BuscarPorAutenticacao(loginModel.Login, loginModel.Senha);

                if (usuarioAutenticado != null)
                {
                    ControleDeSessao.CriarSessao(usuarioAutenticado);
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("INVALID_LOGIN", "Usuário ou senha inválidos.");
            return View("Index", loginModel);
        }

        public ActionResult Sair()
        {
            ControleDeSessao.Encerrar();
            return RedirectToAction("Index");
        }
    }
}