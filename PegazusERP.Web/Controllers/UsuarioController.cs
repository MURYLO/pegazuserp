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
    [Authorize]
    public class UsuarioController : Controller
    {
        #region Membros

        readonly IUsuarioAppService _usuarioService;

        public UsuarioController(
            IUsuarioAppService usuarioService
            )
        {
            _usuarioService = usuarioService;
        }

        #endregion

        #region Actions

        public ActionResult Index(PagedList<UsuarioDTO> pagedList)
        {
            try
            {
                var entities = GetEntities(pagedList);
                var totalRecords = (int)_usuarioService.CountUsuarios(pagedList.SearchTerm);
                pagedList.Parametros(this, entities, totalRecords);
                if (Request.IsAjaxRequest())
                {
                    return View("IndexGridViewPartial", pagedList);
                }

                return View(pagedList);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
    
        public ActionResult Criar()
        {
            try
            {
                return View("Editar", new UsuarioDTO());
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        public ActionResult Editar(int id)
        {
            try
            {
                var usuarioDTO = _usuarioService.FindUsuario(id);

                return View(usuarioDTO);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpPost, ValidateAntiForgeryToken()]
        public ActionResult Editar(UsuarioDTO usuarioDTO)
        {
            try
            {
                if (usuarioDTO.Id == 0)
                {
                    usuarioDTO = _usuarioService.AddUsuario(usuarioDTO);
                    Helper.AdicionarMensagem("Usuário criado com sucesso!", eTipoMensagem.Success, this);
                }
                else
                {
                    usuarioDTO = _usuarioService.UpdateUsuario(usuarioDTO);
                    Helper.AdicionarMensagem("Usuário alterado com sucesso!", eTipoMensagem.Success, this);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Helper.TratarExcecao(ex, this);
                return View(usuarioDTO);
            }
        }

        public ActionResult Remove(int id)
        {
            try
            {
                _usuarioService.RemoveUsuario(id);
                Helper.AdicionarMensagem("Usuário removido com sucesso!", eTipoMensagem.Success, this);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        #endregion

        #region Métodos privados

        private IList<UsuarioDTO> GetEntities(PagedList<UsuarioDTO> pagedList)
        {
            IList<UsuarioDTO> entities;

            if (pagedList.Sort == "NomeUsuario")
                entities = _usuarioService.FindUsuarios(pagedList.SearchTerm, c => c.NomeUsuario, pagedList.SortAsc, pagedList.Page, pagedList.PageSize);
            else
                entities = _usuarioService.FindUsuarios(pagedList.SearchTerm, c => c.Id, pagedList.SortAsc, pagedList.Page, pagedList.PageSize);

            return entities;
        }

        #endregion
    }
}
