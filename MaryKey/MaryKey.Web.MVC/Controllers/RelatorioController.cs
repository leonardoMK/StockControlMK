
using MaryKey.Dominio;
using MaryKey.Repositorio.EF.Repositorio;
using MaryKey.Web.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaryKey.Web.MVC.Controllers
{
    public class RelatorioController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RelatorioProdutos(string nome)
        {
            var banco = new ProdutoRepositorio();
            var produtos = string.IsNullOrWhiteSpace(nome) ? banco.BuscarTodos() : banco.BuscarPorNome(nome);
            List<ProdutoModel> models = new List<ProdutoModel>();
            foreach (var p in produtos)
            {
                models.Add(new ProdutoModel()
                {
                    Id = p.Id,
                    Lucro = (p.ValorProduto * p.Quantidade) - p.GetValorCompra() * p.Quantidade,
                    Nome = p.Nome,
                    PercentualDesconto = p.PercentualDesconto,
                    Quantidade = p.Quantidade,
                    ValorProduto = p.ValorProduto,
                    ValorTotal = p.ValorProduto * p.Quantidade
                });
            }

            return View(models);
        }

        public JsonResult ProdutosAutoComplete(string term)
        {
            var banco = new ProdutoRepositorio();
            IList<Produto> produtosEncontrados = string.IsNullOrEmpty(term) ? banco.BuscarTodos() : banco.BuscarPorNome(term);

            var json = produtosEncontrados.Select(x => new { label = x.Nome });

            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}