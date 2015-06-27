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
    public class PessoaController : Controller
    {
        #region Membros

        private readonly IPessoaAppService _pessoaAppService;

        public PessoaController(IPessoaAppService pessoaAppService)
        {
            _pessoaAppService = pessoaAppService;
        }

        #endregion

        #region Actions

        public ActionResult Index(PagedList<PessoaDTO> pagedList)
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

        public ActionResult IndexGrid(PagedList<PessoaDTO> pagedList)
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
                PessoaDTO PessoaDTO;
                if (!id.HasValue || id == 0)
                {
                    PessoaDTO = new PessoaDTO { Ativo = true };
                }
                else
                {
                    PessoaDTO = _pessoaAppService.FindPessoa(id.Value);
                }

                return View(PessoaDTO);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpPost, ValidateAntiForgeryToken()]
        public ActionResult POSTEditar(PessoaDTO PessoaDTO)
        {
            try
            {
                if (PessoaDTO.Id == 0)
                {
                    PessoaDTO = _pessoaAppService.AddPessoa(PessoaDTO);
                }
                else
                {
                    _pessoaAppService.UpdatePessoa(PessoaDTO);
                }

               
                return JavaScript(
                    "MensagemSucesso(' + mensagemSucesso + ');" +
                    "CarregarPaginaAjax('" + Url.Action("Index", "Pessoa") + "');");
            }
            catch (Exception ex)
            {
                TratamentoErro.Tratamento(this, ex);
                return View("Editar", PessoaDTO);
            }
        }

        public ActionResult Excluir(int id)
        {
            try
            {
                var PessoaDTO = _pessoaAppService.FindPessoa(id);
                _pessoaAppService.RemovePessoa(id);

              
                return JavaScript(
                    "MensagemSucesso('');" +
                    "CarregarPaginaAjax('" + Url.Action("Index", "Pessoa") + "');");
            }
            catch (Exception ex)
            {
                return JavaScript("MensagemErro('" + ex.Message + "');");
            }
        }

        #endregion

        #region Métodos Privados

        private void ConfigureGrid(PagedList<PessoaDTO> pagedList)
        {

            // Definindo a action da GridPartial
            pagedList.Action = "IndexGrid";

            // Obtenha a quantidade total de registros
            var totalRecords = (int)_pessoaAppService.CountPessoas(pagedList.SearchTerm);

            // Obtenha os registros
            IList<PessoaDTO> entities;

            if (pagedList.Sort == "Nome")
                entities = _pessoaAppService.FindPessoas(pagedList.SearchTerm, c => c.Nome, pagedList.SortAsc, pagedList.Page, pagedList.PageSize);

            else if (pagedList.Sort == "Nome")
                entities = _pessoaAppService.FindPessoas(pagedList.SearchTerm, c => c.Nome, pagedList.SortAsc, pagedList.Page, pagedList.PageSize);

            else if (pagedList.Sort == "Nome")
                entities = _pessoaAppService.FindPessoas(pagedList.SearchTerm, c => c.Nome, pagedList.SortAsc, pagedList.Page, pagedList.PageSize);

            else if (pagedList.Sort == "Nome")
                entities = _pessoaAppService.FindPessoas(pagedList.SearchTerm, c => c.Nome, pagedList.SortAsc, pagedList.Page, pagedList.PageSize);

            else
                entities = _pessoaAppService.FindPessoas(pagedList.SearchTerm, c => c.Nome, pagedList.SortAsc, pagedList.Page, pagedList.PageSize);

            // Defina os valores
            pagedList.Parametros(this, entities, totalRecords);
        }

        #endregion
    
    }
}
