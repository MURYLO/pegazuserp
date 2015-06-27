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
    public class MarcaProdutoController : Controller
    {
        #region Membros

        private readonly IMarcaProdutoAppService _marcaProdutoAppService;

        public MarcaProdutoController(IMarcaProdutoAppService marcaProdutoAppService)
        {
            _marcaProdutoAppService = marcaProdutoAppService;
        }

        #endregion

        #region Actions

        public ActionResult Index(PagedList<MarcaProdutoDTO> pagedList)
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

        public ActionResult IndexGrid(PagedList<MarcaProdutoDTO> pagedList)
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
                MarcaProdutoDTO MarcaProdutoDTO;
                if (!id.HasValue || id == 0)
                {
                    MarcaProdutoDTO = new MarcaProdutoDTO();
                }
                else
                {
                    MarcaProdutoDTO = _marcaProdutoAppService.FindMarcaProduto(id.Value);
                }

                return View(MarcaProdutoDTO);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpPost, ValidateAntiForgeryToken()]
        public ActionResult POSTEditar(MarcaProdutoDTO marcaProdutoDTO)
        {
            try
            {
                if (marcaProdutoDTO.Id == 0)
                {
                    marcaProdutoDTO = _marcaProdutoAppService.AddMarcaProduto(marcaProdutoDTO);
                }
                else
                {
                    _marcaProdutoAppService.UpdateMarcaProduto(marcaProdutoDTO);
                }

               
                return JavaScript(
                    "MensagemSucesso(' + mensagemSucesso + ');" +
                    "CarregarPaginaAjax('" + Url.Action("Index", "MarcaProduto") + "');");
            }
            catch (Exception ex)
            {
                TratamentoErro.Tratamento(this, ex);
                return View("Editar", marcaProdutoDTO);
            }
        }

        public ActionResult Excluir(int id)
        {
            try
            {
                var MarcaProdutoDTO = _marcaProdutoAppService.FindMarcaProduto(id);
                _marcaProdutoAppService.RemoveMarcaProduto(id);

              
                return JavaScript(
                    "MensagemSucesso('');" +
                    "CarregarPaginaAjax('" + Url.Action("Index", "MarcaProduto") + "');");
            }
            catch (Exception ex)
            {
                return JavaScript("MensagemErro('" + ex.Message + "');");
            }
        }

        #endregion

        #region Métodos Privados

        private void ConfigureGrid(PagedList<MarcaProdutoDTO> pagedList)
        {

            // Definindo a action da GridPartial
            pagedList.Action = "IndexGrid";

            // Obtenha a quantidade total de registros
            var totalRecords = (int)_marcaProdutoAppService.CountMarcaProdutos(pagedList.SearchTerm);

            // Obtenha os registros
            IList<MarcaProdutoDTO> entities;

            if (pagedList.Sort == "Nome")
                entities = _marcaProdutoAppService.FindMarcaProdutos(pagedList.SearchTerm, c => c.Nome, pagedList.SortAsc, pagedList.Page, pagedList.PageSize);

            else if (pagedList.Sort == "Nome")
                entities = _marcaProdutoAppService.FindMarcaProdutos(pagedList.SearchTerm, c => c.Nome, pagedList.SortAsc, pagedList.Page, pagedList.PageSize);

            else if (pagedList.Sort == "Nome")
                entities = _marcaProdutoAppService.FindMarcaProdutos(pagedList.SearchTerm, c => c.Nome, pagedList.SortAsc, pagedList.Page, pagedList.PageSize);

            else if (pagedList.Sort == "Nome")
                entities = _marcaProdutoAppService.FindMarcaProdutos(pagedList.SearchTerm, c => c.Nome, pagedList.SortAsc, pagedList.Page, pagedList.PageSize);

            else
                entities = _marcaProdutoAppService.FindMarcaProdutos(pagedList.SearchTerm, c => c.Nome, pagedList.SortAsc, pagedList.Page, pagedList.PageSize);

            // Defina os valores
            pagedList.Parametros(this, entities, totalRecords);
        }

        #endregion
    
    }
}
