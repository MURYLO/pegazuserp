
using PegazusERP.DTO;
using System;
using System.Web;
using System.Web.Security;

namespace PegazusERP.Aplicacao.AppServices
{
    public class AppManagerSession
    {
        public UsuarioDTO UsuarioDTO { get; private set; }

        public DateTime? DataHoraLogin { get; private set; }

        public void SetUsuario(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
            {
                UsuarioDTO = null;
                DataHoraLogin = null;
            }
            else
            {
                UsuarioDTO = usuarioDTO;
                DataHoraLogin = DateTime.Now;
            }
        }

    }

    public static class ControladorDeSessao
    {
        #region Membros privados

        private static AppManagerSession _appSession { get; set; }

        private static AppManagerSession GetSession()
        {
            if (HttpContext.Current == null)
            {
                if (_appSession == null)
                {
                    _appSession = new AppManagerSession();
                }
                return _appSession;
            }
            else
            {
                var appManagerSession = (AppManagerSession)HttpContext.Current.Session["arquitetura_session"];
                if (appManagerSession == null)
                {
                    appManagerSession = new AppManagerSession();
                    FormsAuthentication.SignOut();
                }
                return appManagerSession;
            }
        }

        private static void SetSession(AppManagerSession appManagerSession)
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Session["arquitetura_session"] = appManagerSession;
        }


        #endregion

        #region Membros públicos

        public static void Autenticar(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
            {
                throw new ArgumentNullException("usuarioDTO");
            }

            var appManagerSession = GetSession();
            appManagerSession.SetUsuario(usuarioDTO);
            FormsAuthentication.SetAuthCookie(usuarioDTO.NomeUsuario, false);

            SetSession(appManagerSession);
        }

        public static void Desautenticar()
        {
            SetSession(null);
            FormsAuthentication.SignOut();
        }

        public static UsuarioDTO GetUsuario()
        {
            var appManager = GetSession();
            return appManager.UsuarioDTO;
        }

        public static bool EstaAutenticado()
        {
            var appManager = GetSession();
            if (appManager.UsuarioDTO == null || appManager.UsuarioDTO.Id == 0)
                return false;

            return true;
        }

        #endregion
    }
}
