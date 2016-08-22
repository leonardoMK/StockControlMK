using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MaryKey.Web.MVC.Seguranca
{
    public class Autorizador : AuthorizeAttribute
    {

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (AuthorizeCore(filterContext.HttpContext))
            {
                UsuarioLogado usuarioLogado = ControleDeSessao.UsuarioLogado;

                if (usuarioLogado != null)
                {
                    var identidade = new GenericIdentity(usuarioLogado.Login);
                    string[] roles = usuarioLogado.Permissoes;

                    var principal = new GenericPrincipal(identidade, roles);

                    Thread.CurrentPrincipal = principal;
                    HttpContext.Current.User = principal;

                    base.OnAuthorization(filterContext);
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult
                                (new RouteValueDictionary
                                {
                                    { "action", "Index"},
                                    { "controller", "Login"}
                                });
                }
            }
            else
            {
                HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}