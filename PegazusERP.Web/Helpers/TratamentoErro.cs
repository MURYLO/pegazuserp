using System;
using System.Web.Mvc;
using System.Linq;
using PegazusERP.Infraestrutura.Base;

namespace PegazusERP.Web.Helpers
{
    public static class TratamentoErro
    {
        public static void Tratamento(this Controller controller, Exception ex, bool clearModelState = true)
        {
            if (clearModelState)
            {
                controller.ModelState.Clear();
            }

            var appException = ex as AppException;
            if (appException != null && appException.ValidationErrors != null)
            {
                foreach (var erro in appException.ValidationErrors)
                    controller.ModelState.AddModelError(erro.MemberNames, erro.ErrorMessage);
            }
            
            controller.ViewBag.AlertError = ex.Message;
        }

        public static string Tratamento(Exception ex)
        {
            var appException = ex as AppException;
            string mensagemErro = string.Empty;
            if (appException != null && appException.ValidationErrors != null)
            {
                mensagemErro = string.Join("\n", appException.ValidationErrors.Select(x=>x.ErrorMessage));
            }
            else
            {
                mensagemErro = ex.Message;
            }

            return mensagemErro;
        }
    }
}