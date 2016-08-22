using MaryKey.Dominio;
using MaryKey.Repositorio.EF.Repositorio;
using MaryKey.Web.MVC.Models;
using MaryKey.Web.MVC.Seguranca;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaryKey.Web.MVC.Controllers
{
    [Autorizador]
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult ManterProduto(int? id)
        {
            var db = new ProdutoRepositorio();
            if (id.HasValue)
            {
                var produto = db.buscarPorId((int)id);
                var model = new ProdutoModel()
                {
                    Nome = produto.Nome,
                    ValorProduto = produto.ValorProduto,
                    PercentualDesconto = produto.PercentualDesconto,
                    Quantidade = produto.Quantidade,
                    Id = produto.Id
                };
                return View(model);
            }
            return View();
        }

        public ActionResult ExcluirProduto(int id)
        {
            var db = new ProdutoRepositorio();
            db.Excluir(id);
            TempData["Mensagem"] = "Produto excluido com sucesso!";
            return RedirectToAction("RelatorioProdutos", "Relatorio");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Salvar(ProdutoModel produto)
        {
            var model = new ProdutoModel()
            {
                Nome = produto.Nome,
                PercentualDesconto = produto.PercentualDesconto,
                Quantidade = produto.Quantidade,
                ValorProduto = produto.ValorProduto
            };
            var database = new ProdutoRepositorio();
            if (ModelState.IsValid)
            {
                var produtos = database.BuscarPorNome(produto.Nome);
                var produtoPorId = produto.Id!=null ? database.buscarPorId((int)produto.Id) : null;
                
                if (produtos.Count > 0 || produtoPorId!=null)
                {
                    model.Id = produtos.Count >0 ? produtos[0].Id : produtoPorId.Id;
                    try
                    {
                        database.Atualizar(new Produto()
                        {
                            Id = (int)model.Id,
                            Nome = model.Nome,
                            PercentualDesconto = model.PercentualDesconto,
                            Quantidade = model.Quantidade,
                            ValorProduto = model.ValorProduto
                        });
                    }
                    catch(SqlException e)
                    {
                        TempData["Mensagem"] = "Erro ao atualizar dados no banco de dados: '"+e.Message+"' Por favor, digite os dados corretamente";
                    }
                    TempData["Mensagem"] = "Produto atualizado com sucesso!";
                    return RedirectToAction("RelatorioProdutos","Relatorio");
                    //View("ManterProduto", model);
                }
                else
                {
                    try
                    {
                        database.AddProduct(new Produto()
                        {
                            Nome = model.Nome,
                            PercentualDesconto = model.PercentualDesconto,
                            Quantidade = model.Quantidade,
                            ValorProduto = model.ValorProduto
                        });
                        TempData["Mensagem"] = "Produto inserido com sucesso";
                        return RedirectToAction("RelatorioProdutos", "Relatorio");
                    }
                    catch(SqlException e)
                    {
                        TempData["Mensagem"] = "Erro ao inserir dados no banco de dados: '" + e.Message + "' Por favor, digite os dados corretamente";
                    }
                    return View("ManterProduto", model);
                }
            }
            else
            {
                return View("ManterProduto",model);
            }
        }
    }
}