﻿using MaryKey.Dominio;
using MaryKey.Repositorio.EF.Repositorio;
using MaryKey.Web.MVC.Models;
using MaryKey.Web.MVC.Seguranca;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mime;
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

        //public ActionResult RemoverUmItem(int id)
        //{
        //    var db = new ProdutoRepositorio();
        //    var produto = db.buscarPorId(id);
        //    if(produto.Quantidade <= 1)
        //    {
        //        TempData["Mensagem"] = "Produto excluido com sucesso!";
        //        db.Excluir(id);
        //    }
        //    else
        //    {
        //        produto.Quantidade--;
        //        db.RemoverUmItem(produto);
        //        TempData["Mensagem"] = "Removido um item do produto";
        //    }
            
            
        //    return RedirectToAction("RelatorioProdutos", "Relatorio");
        //}

        public ActionResult RemoveOne(int id)
        {
            var db = new ProdutoRepositorio();
            var item = db.buscarPorId(id);
            if (item.Quantidade < 1)
            {
                TempData["Mensagem"] = "Operação não aceita, produto possui menos que 1 item no estoque";
                Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                return Content(Convert.ToString(TempData["Mensagem"]));
            }
            else
            {
                item.Quantidade--;
                db.RemoverUmItem(item);
                Response.StatusCode = (int)HttpStatusCode.OK;
                TempData["Mensagem"] = "Removido um item do produto";
            }
            return Content(Convert.ToString(TempData["Mensagem"]));
        }

        public ActionResult AddOne(int id)
        {
            var db = new ProdutoRepositorio();
            var produto = db.buscarPorId(id);
            produto.Quantidade++;
            db.Atualizar(produto);

            TempData["Mensagem"] = "Adicionado um item ao produto";
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Content(Convert.ToString(TempData["Mensagem"]));
        }

        //public ActionResult AdicionarUmItem(int id)
        //{
        //    var db = new ProdutoRepositorio();
        //    var produto = db.buscarPorId(id);
        //    produto.Quantidade++;
        //    db.Atualizar(produto);

        //    TempData["Mensagem"] = "Adicionado um item ao produto";
        //    return RedirectToAction("RelatorioProdutos", "Relatorio");
        //}

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