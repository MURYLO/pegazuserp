using PegazusERP.Aplicacao.Services.Interface;
using PegazusERP.DTO;
using PegazusERP.Web.Helpers;
using PegazusERP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PegazusERP.Web.Controllers
{
    //[Authorize]
    public class ProdutoController : Controller
    {
        #region Membros

        private readonly IProdutoAppService _produtoAppService;
        private readonly ICategoriaProdutoAppService _categoriaProdutoAppService;
        private readonly IMarcaProdutoAppService _marcaProdutoAppService;

        public ProdutoController(IProdutoAppService produtoAppService,
                                ICategoriaProdutoAppService categoriaProdutoAppService,
                                IMarcaProdutoAppService marcaProdutoAppService)
        {
            _produtoAppService = produtoAppService;
            _categoriaProdutoAppService = categoriaProdutoAppService;
            _marcaProdutoAppService = marcaProdutoAppService;
        }

        #endregion

        #region Actions

        public ActionResult Index(PagedList<ProdutoDTO> pagedList)
        {
            try
            {
                ConfigureGrid(pagedList);
                return View(pagedList);
            }
            catch(Exception ex)
            {
                return View("Error", ex);
            }
        }

        public ActionResult IndexGrid(PagedList<ProdutoDTO> pagedList)
        {
            try
            {
                ConfigureGrid(pagedList);
                return View("IndexGridViewPartial", pagedList );
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult Editar(int? id, string error)
        {
            try
            {
                ViewBag.Marca = GetMarca();
                ViewBag.Categoria = GetCategoria();
                ProdutoDTO produtoDTO ;
                if (!id.HasValue || id == 0)
                {
                    produtoDTO = new ProdutoDTO { Ativo = true };
                }
                else
                {
                    produtoDTO = _produtoAppService.FindProduto(id.Value);
                }

                return View(produtoDTO);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpPost, ValidateAntiForgeryToken()]
        public ActionResult POSTEditar(ProdutoDTO produtoDTO)
        {
            try
            {
                if (produtoDTO.Id == 0)
                {
                    produtoDTO = _produtoAppService.AddProduto(produtoDTO);
                }
                else
                {
                    _produtoAppService.UpdateProduto(produtoDTO);
                }
               
                return JavaScript(
                    "MensagemSucesso('Produto gravado com sucesso.');" +
                    "CarregarPaginaAjax('" + Url.Action("Index", "Produto") + "');");
            }
            catch (Exception ex)
            {
                ViewBag.Marca = GetMarca();
                ViewBag.Categoria = GetCategoria();
                TratamentoErro.Tratamento(this, ex, false);
                return View("Editar", produtoDTO);
            }
        }

        public ActionResult Excluir(int id)
        {
            try
            {
                var ProdutoDTO = _produtoAppService.FindProduto(id);
                _produtoAppService.RemoveProduto(id);

              
                return JavaScript(
                    "MensagemSucesso('Produto excluído com sucesso.');" +
                    "CarregarPaginaAjax('" + Url.Action("Index", "Produto") + "');");
            }
            catch (Exception ex)
            {
                return JavaScript("MensagemErro('" + ex.Message + "');");
            }
        }

        #endregion

        #region Métodos Privados

        private void ConfigureGrid(PagedList<ProdutoDTO> pagedList)
        {

            // Definindo a action da GridPartial
            pagedList.Action = "IndexGrid";

            // Obtenha a quantidade total de registros
            var totalRecords = (int)_produtoAppService.CountProdutos(pagedList.SearchTerm);

            // Obtenha os registros
            IList<ProdutoDTO> entities;

            if (pagedList.Sort == "Nome")
                entities = _produtoAppService.FindProdutos(pagedList.SearchTerm, c => c.Nome, pagedList.SortAsc, pagedList.Page, pagedList.PageSize);

            else if (pagedList.Sort == "Nome")
                entities = _produtoAppService.FindProdutos(pagedList.SearchTerm, c => c.Nome, pagedList.SortAsc, pagedList.Page, pagedList.PageSize);

            else if (pagedList.Sort == "Nome")
                entities = _produtoAppService.FindProdutos(pagedList.SearchTerm, c => c.Nome, pagedList.SortAsc, pagedList.Page, pagedList.PageSize);

            else if (pagedList.Sort == "Nome")
                entities = _produtoAppService.FindProdutos(pagedList.SearchTerm, c => c.Nome, pagedList.SortAsc, pagedList.Page, pagedList.PageSize);

            else
                entities = _produtoAppService.FindProdutos(pagedList.SearchTerm, c => c.Nome, pagedList.SortAsc, pagedList.Page, pagedList.PageSize);

            // Defina os valores
            pagedList.Parametros(this, entities, totalRecords);
        }

        private SelectList GetCategoria()
        {
            var categoria = _categoriaProdutoAppService.FindCategoriaProdutos(null, c => c.Nome).ToList();
            return new SelectList(categoria, "Id", "Nome");
        }

        private SelectList GetMarca()
        {
            var marca = _marcaProdutoAppService.FindMarcaProdutos(null, c => c.Nome).ToList();
            return new SelectList(marca, "Id", "Nome");
        }


        #endregion
    
    }
}
