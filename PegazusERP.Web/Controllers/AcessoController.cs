using PegazusERP.Aplicacao.AppServices;
using PegazusERP.Aplicacao.Services.Interface;
using PegazusERP.Infraestrutura.Util;
using PegazusERP.Web.Helpers;
using PegazusERP.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace PegazusERP.Web.Controllers
{
    public class AcessoController : Controller
    {
        #region Membros

        readonly IUsuarioAppService _usuarioService;


        public AcessoController(IUsuarioAppService usuarioService  )
        {
            _usuarioService = usuarioService;
        }

        #endregion

        #region Actions

        public ActionResult Index()
        {
            return RedirectToAction("Entrar");
        }

        public ActionResult Entrar(string returnUrl, string alertSuccess)
        {
            if (Request.IsAjaxRequest())
            {
                ViewBag.ReturnUrl = returnUrl;
                return View("_Relogar");
            }

            if (ControladorDeSessao.EstaAutenticado())
            {
                return RedirectToAction("Index", "Home");
            }

            var cookieValue = CookieManager.GetCookieValue("lembrarme");
            if (!string.IsNullOrWhiteSpace(cookieValue))
            {
                ViewBag.Usuario = cookieValue;
                ViewBag.Checked = "checked=checked";
            }

            ViewBag.AlertSuccess = alertSuccess;

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken()]
        public ActionResult Entrar(string usuario, string senha, string lembrarme, string ReturnUrl)
        {
            try
            {
                _usuarioService.AutenticarUsuario(usuario, senha);

                if (!string.IsNullOrWhiteSpace(lembrarme) && lembrarme == "on")
                {
                    CookieManager.SetCookieValue("lembrarme", usuario);
                }
                else
                {
                    CookieManager.SetCookieValue("lembrarme", null);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TratamentoErro.Tratamento(this, ex);
                ViewBag.Usuario = usuario;

                if (!string.IsNullOrWhiteSpace(lembrarme) && lembrarme == "on")
                {
                    ViewBag.Checked = "checked=checked";
                }

                return View("Entrar");
            }
        }

        public ActionResult Sair()
        {
            try
            {
                ControladorDeSessao.Desautenticar();
            }
            catch(Exception ex)
            {
                TratamentoErro.Tratamento(this, ex);
            }

            return RedirectToAction("Entrar");
        }

        [HttpPost]
        public ActionResult POSTEntrarAjax(string usuario, string senha, string returnUrl)
        {
            try
            {
                if (!Request.IsAjaxRequest())
                {
                    return Content("Não autorizado.");
                }

                _usuarioService.AutenticarUsuario(usuario, senha);

                return Json(new { sucesso = true, returnUrl = returnUrl });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, erro = ex.Message });
            }
        }

        //public ActionResult CriarPrimeiroUsuarioAdministrador()
        //{
        //    try
        //    {
        //        UsuarioDTO usuarioDTO = new UsuarioDTO
        //        {
        //            Ativo = true,
        //            Login = "admin",
        //            PessoaBairro = "Pq. das Larajeiras",
        //            PessoaCelular = "6299999999",
        //            PessoaCep = "74000000",
        //            PessoaCidade = "Goiânia",
        //            PessoaComplemento = "Complemento",
        //            PessoaDataNascimento = new DateTime(1984, 8, 31, 0, 0, 0),
        //            PessoaEmail = "desenvolvimento@fingerid.com.br",
        //            PessoaEstado = eEstado.GO,
        //            PessoaLogradouro = "Logradouro",
        //            PessoaNome = "Administrador Master",
        //            PessoaNumero = "S/N",
        //            PessoaSexo = Sexo.Masculino,
        //            PessoaTelefone = "623333333",
        //            Senha = "admin"
        //        };


        //        _usuarioService.AddAdministradorSistema(usuarioDTO);


        //        return Content("Usuário administrador criado com sucesso.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content(ex.Message);
        //    }
        //}

        public ActionResult RecuperarSenha(string usuario)
        {
            ViewBag.Usuario = usuario;
            return View();
        }

        //[HttpPost, ValidateAntiForgeryToken()]
        //public ActionResult RecuperarSenha(string usuario, FormCollection form)
        //{
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(usuario))
        //        {
        //            throw new Exception("Informe o nome de usuário.");
        //        }

        //        var usuarioDTO = _usuarioService.FindUsuarioNull(usuario);
        //        if (usuarioDTO == null)
        //        {
        //            throw new Exception("Usuário não encontrado.");
        //        }

        //        var token = _usuarioService.GerarTokenSenha(usuarioDTO.Id);

        //        var url = string.Format("http://{0}{1}", Request.Url.Authority, Url.Action("RedefinirSenha", "Acesso", new { id = token }));

        //        var sbMensagem = new StringBuilder();
        //        sbMensagem.AppendLine("<p>Uma solicitação de recuperação de senha foi enviada a partir do sistema Gestão Escolar.</p>");
        //        sbMensagem.AppendLine("<p>Caso não tenha solicitado nenhuma recuperação de senha favor desconsiderar esta mensagem.</p>");
        //        sbMensagem.AppendLine("<br />");
        //        sbMensagem.AppendLine("<p>Para redefinir sua senha clique no link abaixo. Caso o link não funcione copie e cole o link em seu navegador. O link tem validade de 48 horas.</p>");
        //        sbMensagem.AppendLine("<br />");
        //        sbMensagem.AppendLine(string.Format("<p><a href=\"{0}\">{0}</a></p>", url));
        //        sbMensagem.AppendLine("<br /><hr />");
        //        sbMensagem.AppendLine("<p>Gestão Escolar - Ponto iD Tecnologia</p>");
        //        sbMensagem.AppendLine("<p>Esta é uma mensagem automática. Não responda a esta mensagem.</p>");
                
        //        var emailDTO = new EmailDTO();

        //        emailDTO.Assunto = "Recuperar Senha - Gestão Escolar";
        //        emailDTO.Para = usuarioDTO.PessoaEmail;
        //        emailDTO.Mensagem = sbMensagem.ToString();

        //        var listaDeEmailDTO = new List<EmailDTO>();
        //        listaDeEmailDTO.Add(emailDTO);

        //        _emailAppService.EnviarEmailAssync(listaDeEmailDTO);

        //        ViewBag.AlertSuccess = "E-mail enviado com sucesso! Acesse seu e-mail e siga as instruções de recuperação de senha.";

        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        TratamentoErro.Tratamento(this, ex);
        //        ViewBag.Usuario = usuario;

        //        return View();
        //    }
        //}

        //public ActionResult Redefinirsenha(string id)
        //{
        //    try
        //    {
        //        _usuarioService.ValidarTokenSenha(id);
        //        ViewBag.Token = id;
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content(ex.Message);
        //    }
        //}

        //[HttpPost, ValidateAntiForgeryToken()]
        //public ActionResult Redefinirsenha(string senha, string confirma, string token)
        //{
        //    try
        //    {
        //        _usuarioService.RedefinirSenhaComToken(token, senha, confirma);

        //        return RedirectToAction("Entrar", new { alertSuccess = "Senha redefinida com sucesso! Entre com seu usuário e nova senha para continuar." });
        //    }
        //    catch (Exception ex)
        //    {
        //        TratamentoErro.Tratamento(this, ex);
        //        ViewBag.Token = token;

        //        return View();
        //    }
        //}

        public ActionResult Contato()
        {
            return View(new ContatoModel());
        }

        [HttpPost, ValidateAntiForgeryToken()]
        public ActionResult Contato(ContatoModel contatoModel)
        {
            try
            {
                if (!Function.ValidaEmail(contatoModel.Email))
                {
                    throw new Exception("O e-mail informado é inválido.");
                }

                var sbMensagem = new StringBuilder();
                sbMensagem.AppendLine("<p>" + contatoModel.Nome + " visitou o site Gestão Escolar e deixou uma mensagem.</p>");
                sbMensagem.AppendLine("<br />");
                sbMensagem.AppendLine("<p>Email: " + contatoModel.Email + "</p>");
                sbMensagem.AppendLine("<p>Telefone: " + contatoModel.Telefone + "</p>");
                sbMensagem.AppendLine("<p>Assunto: " + contatoModel.Assunto + "</p>");
                sbMensagem.AppendLine("<p>Mensagem:</p>");
                sbMensagem.AppendLine("<p>" + contatoModel.Mensagem + "</p>");
                sbMensagem.AppendLine("<br/><hr />");
                sbMensagem.AppendLine("<p>Gestão Escolar - Ponto iD Tecnologia</p>");
                sbMensagem.AppendLine("<p>Esta é uma mensagem automática. Não responda a esta mensagem.</p>");

                //var sistema = _sistemaAppService.GetSistema();

                //if (!sistema.ServidorEmailAtivo)
                //{
                //    throw new Exception("Não foi possível completar a operação.");
                //}

                //Function.SendEmail(
                //    "Contato realizado pelo site Gestão Escolar",
                //    sbMensagem.ToString(),
                //    "Gestão Escolcar",
                //    sistema.ServidorEmailConta,
                //    sistema.ServidorEmailSenha,
                //    sistema.ServidorEmailSmtp,
                //    sistema.ServidorEmailPorta.Value,
                //    sistema.ServidorEmailSsl,
                //    new List<string> { "contato@pontoid.com.br", "suporte@pontoid.com.br" },
                //    null);

                ViewBag.AlertSuccess = "E-mail enviado com sucesso!";
                return View("Contato", new ContatoModel());
            }
            catch (Exception ex)
            {
                TratamentoErro.Tratamento(this, ex);
                return View("Contato", contatoModel);
            }
        }

        public ActionResult SaibaMais()
        {
            return View();
        }

        #endregion
    }
}