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
    public class CategoriaProdutoController : Controller
    {
        #region Membros

        private readonly ICategoriaProdutoAppService _categoriaProdutoAppService;

        public CategoriaProdutoController(ICategoriaProdutoAppService categoriaProdutoAppService)
        {
            _categoriaProdutoAppService = categoriaProdutoAppService;
        }

        #endregion

        #region Actions

        public ActionResult Index(PagedList<CategoriaProdutoDTO> pagedList)
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

        public ActionResult IndexGrid(PagedList<CategoriaProdutoDTO> pagedList)
        {
            try
            {
                ConfigureGrid(pagedList);
                return View("IndexGridViewPartial", pagedList);
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
                CategoriaProdutoDTO CategoriaProdutoDTO;
                if (!id.HasValue || id == 0)
                {
                    CategoriaProdutoDTO = new CategoriaProdutoDTO();
                }
                else
                {
                    CategoriaProdutoDTO = _categoriaProdutoAppService.FindCategoriaProduto(id.Value);
                }

                return View(CategoriaProdutoDTO);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpPost, ValidateAntiForgeryToken()]
        public ActionResult POSTEditar(CategoriaProdutoDTO categoriaProdutoDTO)
        {
            try
            {
                if (categoriaProdutoDTO.Id == 0)
                {
                    categoriaProdutoDTO = _categoriaProdutoAppService.AddCategoriaProduto(categoriaProdutoDTO);
                }
                else
                {
                    _categoriaProdutoAppService.UpdateCategoriaProduto(categoriaProdutoDTO);
                }

               
                return JavaScript(
                    "MensagemSucesso(' + mensagemSucesso + ');" +
                    "CarregarPaginaAjax('" + Url.Action("Index", "CategoriaProduto") + "');");
            }
            catch (Exception ex)
            {
                TratamentoErro.Tratamento(this, ex);
                return View("Editar", categoriaProdutoDTO);
            }
        }

        public ActionResult Excluir(int id)
        {
            try
            {
                var CategoriaProdutoDTO = _categoriaProdutoAppService.FindCategoriaProduto(id);
                _categoriaProdutoAppService.RemoveCategoriaProduto(id);

              
                return JavaScript(
                    "MensagemSucesso('');" +
                    "CarregarPaginaAjax('" + Url.Action("Index", "CategoriaProduto") + "');");
            }
            catch (Exception ex)
            {
                return JavaScript("MensagemErro('" + ex.Message + "');");
            }
        }

        #endregion

        #region Métodos Privados

        private void ConfigureGrid(PagedList<CategoriaProdutoDTO> pagedList)
        {

            // Definindo a action da GridPartial
            pagedList.Action = "IndexGrid";

            // Obtenha a quantidade total de registros
            var totalRecords = (int)_categoriaProdutoAppService.CountCategoriaProdutos(pagedList.SearchTerm);

            // Obtenha os registros
            IList<CategoriaProdutoDTO> entities;

            if (pagedList.Sort == "Nome")
                entities = _categoriaProdutoAppService.FindCategoriaProdutos(pagedList.SearchTerm, c => c.Nome, pagedList.SortAsc, pagedList.Page, pagedList.PageSize);

            else if (pagedList.Sort == "Nome")
                entities = _categoriaProdutoAppService.FindCategoriaProdutos(pagedList.SearchTerm, c => c.Nome, pagedList.SortAsc, pagedList.Page, pagedList.PageSize);

            else if (pagedList.Sort == "Nome")
                entities = _categoriaProdutoAppService.FindCategoriaProdutos(pagedList.SearchTerm, c => c.Nome, pagedList.SortAsc, pagedList.Page, pagedList.PageSize);

            else if (pagedList.Sort == "Nome")
                entities = _categoriaProdutoAppService.FindCategoriaProdutos(pagedList.SearchTerm, c => c.Nome, pagedList.SortAsc, pagedList.Page, pagedList.PageSize);

            else
                entities = _categoriaProdutoAppService.FindCategoriaProdutos(pagedList.SearchTerm, c => c.Nome, pagedList.SortAsc, pagedList.Page, pagedList.PageSize);

            // Defina os valores
            pagedList.Parametros(this, entities, totalRecords);
        }

        #endregion
    
    }
}
