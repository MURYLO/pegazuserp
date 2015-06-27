using System.Web;
using System;
using System.Collections.Generic;

namespace PegazusERP.Web.Helpers
{
    public class CookieHelper
    {
        #region Membros
        /// <summary>
        /// Nome do cookie
        /// </summary>
        private readonly string _cookieName;

        #endregion

        #region Construtor

        /// <summary>
        /// Define o nome do cookie a ser gerenciado
        /// </summary>
        /// <param name="cookieName">Nome do cookie</param>
        public CookieHelper(string cookieName)
        {
            _cookieName = cookieName;
        }

        #endregion

        #region Metodos Publicos

        /// <summary>
        /// Cria um novo cookie ou atualiza os parametros do existente.
        /// </summary>
        /// <param name="expiresDate">Data de expiração do cookie. Null para nunca expirar (100 anos).</param>
        /// <param name="httpOnly">Define o acesso ao cookie por apenas requisição http ou tambem por javascript no lado do cliente.</param>
        /// <param name="update">Define se será atualizado os valores dos parametros caso o cookie já exista.</param>
        /// <returns>True se criado ou atualizado com sucesso ou false se o cookie não puder ser acessado.</returns>
        public bool CreateCookie(DateTime? expiresDate, bool httpOnly = true, bool update = true)
        {
            try
            {
                HttpContext context = HttpContext.Current;
                HttpCookie cookie = context.Request.Cookies.Get(_cookieName);

                if (cookie == null)
                {
                    cookie = new HttpCookie(_cookieName);
                    cookie.Expires = expiresDate ?? DateTime.Now.AddYears(100);
                    cookie.HttpOnly = httpOnly;
                    context.Response.Cookies.Add(cookie);
                }
                else if (update)
                {
                    cookie.Expires = expiresDate ?? DateTime.Now.AddYears(100);
                    cookie.HttpOnly = httpOnly;
                    context.Response.Cookies.Set(cookie);
                }
                else
                {
                    return true;
                }

                HttpCookie readCookie = context.Request.Cookies.Get(_cookieName);
                return (readCookie != null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Adiciona um par key/value ao cookie.
        /// </summary>
        /// <param name="key">Chave do par.</param>
        /// <param name="value">Valor do par.</param>
        /// <returns>True se adicionado o valor com sucesso ou false se não for possível acessar o cookie.</returns>
        public bool SetCookieValue(string key, string value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(key))
                    return false;

                HttpContext context = HttpContext.Current;
                HttpCookie cookie = context.Request.Cookies.Get(_cookieName);

                if (cookie == null)
                {
                    CreateCookie(null);
                    cookie = context.Request.Cookies.Get(_cookieName);
                    if (cookie == null)
                        return false;
                }

                cookie.Values.Set(key, value ?? "");
                context.Response.Cookies.Set(cookie);

                HttpCookie readCookie = context.Request.Cookies.Get(_cookieName);
                return (readCookie != null && readCookie.Values.Get(readCookie.Values.Get(key)) != null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtém o valor de uma respectiva chave
        /// </summary>
        /// <param name="key">Chave referente ao valor a ser buscado</param>
        /// <returns>Valor referente à chave</returns>
        public string GetCookieValue(string key)
        {
            try
            {
                HttpContext context = HttpContext.Current;
                HttpCookie cookie = context.Request.Cookies.Get(_cookieName);

                if (cookie == null)
                    return null;

                return cookie.Values.Get(key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtém uma lista de nomes das chaves do cookie.
        /// </summary>
        /// <returns>Lista de chaves. Lista vazia caso não seja possível acessar o cookie.</returns>
        public List<string> GetAllCookieKeys()
        {
            List<string> keys = new List<string>();
            HttpContext context = HttpContext.Current;
            HttpCookie cookie = context.Request.Cookies.Get(_cookieName);

            if (cookie == null)
            {
                return keys;
            }

            foreach (var key in cookie.Values.AllKeys)
            {
                keys.Add(key);
            }

            return keys;
        }

        /// <summary>
        /// Remove uma chave da coleção do cookie.
        /// </summary>
        /// <param name="key">Chave referente ao par de valores key/value do cookie.</param>
        /// <returns>True se o cookie não existir, true se alterado com sucesso ou false se o cookie não puder ser acessado.</returns>
        public bool RemoveCookieKey(string key)
        {
            HttpContext context = HttpContext.Current;
            HttpCookie cookie = context.Request.Cookies.Get(_cookieName);

            if (cookie == null)
            {
                return true;
            }

            cookie.Values.Remove(key);
            context.Response.Cookies.Set(cookie);
            HttpCookie readCookie = context.Request.Cookies.Get(_cookieName);
            return (readCookie != null && readCookie.Values.Get(readCookie.Values.Get(key)) == null);
        }

        /// <summary>
        /// Expira o cookie.
        /// </summary>
        public void ExpireCookie()
        {
            try
            {
                HttpContext context = HttpContext.Current;
                context.Response.Cookies.Add(new HttpCookie(_cookieName) { Name = _cookieName, Expires = DateTime.Now.AddDays(-1d) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}