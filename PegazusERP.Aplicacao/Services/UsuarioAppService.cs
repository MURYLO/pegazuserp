using PegazusERP.Aplicacao.Base;
using PegazusERP.Aplicacao.Services.Interface;
using PegazusERP.Dominio.Aggregates.UsuarioAgg;
using PegazusERP.DTO;
using PegazusERP.Infraestrutura.Adapter;
using PegazusERP.Infraestrutura.Logging;
using PPegazusERP.Dominio.Aggregates.UsuarioAgg;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using PegazusERP.Aplicacao.AppServices;
using PegazusERP.Infraestrutura.Validator;


namespace PegazusERP.Aplicacao.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        #region Membros

        readonly IUsuarioRepository _usuarioRepository;

        #endregion

        #region Construtor

        public UsuarioAppService(IUsuarioRepository usuarioRepository)
        {
            if (usuarioRepository == null)
                throw new ArgumentNullException("usuarioRepository");

            _usuarioRepository = usuarioRepository;
        }

        #endregion

        #region Membros de IUsuarioAppService

        public UsuarioDTO AddUsuario(UsuarioDTO usuarioDTO)
        {
            try
            {
                if (usuarioDTO == null)
                    throw new Exception("Objeto não instânciado.");

                var usuario = UsuarioFactory.CreateUsuario(
                    usuarioDTO.Permissao,
                    usuarioDTO.NomeUsuario,
                    usuarioDTO.Senha,
                    usuarioDTO.Ativo
                    );

                SalvarUsuario(usuario);

                var adapter = TypeAdapterFactory.CreateAdapter();
                return adapter.Adapt<Usuario, UsuarioDTO>(usuario);
            }
            catch (ApplicationValidationErrorsException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().LogError(ex);
                throw new Exception("O servidor não respondeu.");
            }
        }

        public UsuarioDTO UpdateUsuario(UsuarioDTO usuarioDTO)
        {
            try
            {
                if (usuarioDTO == null)
                    throw new Exception("Objeto não instânciado.");

                var persistido = _usuarioRepository.Get(usuarioDTO.Id);
                if (persistido == null)
                    throw new Exception("Usuario não encontrado.");

                var corrente = UsuarioFactory.CreateUsuario(
                    usuarioDTO.Permissao,
                    usuarioDTO.NomeUsuario,
                    persistido.Senha,
                    usuarioDTO.Ativo
                    );

                corrente.Id = persistido.Id;

                AlterarUsuario(persistido, corrente);

                var adapter = TypeAdapterFactory.CreateAdapter();
                return adapter.Adapt<Usuario, UsuarioDTO>(corrente);
            }
            catch (ApplicationValidationErrorsException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().LogError(ex);
                throw new Exception("O servidor não respondeu.");
            }
        }

        public void RemoveUsuario(int usuarioId)
        {
            try
            {
                if (usuarioId <= 0)
                    throw new Exception("Id do usuário inválido.");

                var usuario = _usuarioRepository.Get(usuarioId);
                if (usuario == null)
                    throw new Exception("Usuario não encontrado.");

                _usuarioRepository.Remove(usuario);
                _usuarioRepository.Commit();
            }
            catch (ApplicationValidationErrorsException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().LogError(ex);
                throw new Exception("O servidor não respondeu.");
            }
        }

        public UsuarioDTO FindUsuario(int usuarioId)
        {
            try
            {
                if (usuarioId <= 0)
                    throw new Exception("Id do usuário inválido.");

                var usuario = _usuarioRepository.Get(usuarioId);
                if (usuario == null)
                    throw new Exception("Usuário não encontrado.");

                //usuario.Nome = Encryption.DecryptToString(usuario.Nome);

                var adapter = TypeAdapterFactory.CreateAdapter();
                return adapter.Adapt<Usuario, UsuarioDTO>(usuario);
            }
            catch (ApplicationValidationErrorsException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().LogError(ex);
                throw new Exception("O servidor não respondeu.");
            }
        }

        public UsuarioDTO FindUsuario(string nomeUsuario)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nomeUsuario))
                    throw new ApplicationValidationErrorsException("Informe o email.");

                var usuario = _usuarioRepository.GetFiltered(c=>c.NomeUsuario == nomeUsuario).SingleOrDefault();
                if (usuario == null)
                    throw new ApplicationValidationErrorsException("Usuário não encontrado.");

                //usuario.Senha = Encryption.DecryptToString(usuario.Senha);

                var adapter = TypeAdapterFactory.CreateAdapter();
                return adapter.Adapt<Usuario, UsuarioDTO>(usuario);
            }
            catch (ApplicationValidationErrorsException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().LogError(ex);
                throw new Exception("O servidor não respondeu.");
            }
        }

        public List<UsuarioDTO> FindUsuarios<KProperty>(string texto, Expression<Func<Usuario, KProperty>> orderByExpression, bool ascending = true)
        {
            try
            {
                var spec = UsuarioSpecifications.ConsultaTexto(texto);
                List<Usuario> usuarios = _usuarioRepository.AllMatching<KProperty>(spec, orderByExpression, ascending).ToList();

                var adapter = TypeAdapterFactory.CreateAdapter();
                return adapter.Adapt<List<Usuario>, List<UsuarioDTO>>(usuarios);
            }
            catch (ApplicationValidationErrorsException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().LogError(ex);
                throw new Exception("O servidor não respondeu.");
            }
        }

        public List<UsuarioDTO> FindUsuarios<KProperty>(string texto, Expression<Func<Usuario, KProperty>> orderByExpression, bool ascending, int pageIndex, int pageCount)
        {
            try
            {
                if (pageIndex <= 0 || pageCount <= 0)
                    throw new Exception("Argumentos da paginação inválidos.");

                var spec = UsuarioSpecifications.ConsultaTexto(texto);
                List<Usuario> usuarios = _usuarioRepository.GetPaged<KProperty>(pageIndex, pageCount, spec, orderByExpression, ascending).ToList();

                var adapter = TypeAdapterFactory.CreateAdapter();
                return adapter.Adapt<List<Usuario>, List<UsuarioDTO>>(usuarios);
            }
            catch (ApplicationValidationErrorsException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().LogError(ex);
                throw new Exception("O servidor não respondeu.");
            }
        }

        public long CountUsuarios(string texto)
        {
            try
            {
                var spec = UsuarioSpecifications.ConsultaTexto(texto);
                return _usuarioRepository.Count(spec);
            }
            catch (ApplicationValidationErrorsException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().LogError(ex);
                throw new Exception("O servidor não respondeu.");
            }
        }

        public void AutenticarUsuario(string email, string senha)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ApplicationValidationErrorsException("Digite o email.");
                }

                if (string.IsNullOrWhiteSpace(senha))
                {
                    throw new ApplicationValidationErrorsException("Digite a senha.");
                }

                var spec = UsuarioSpecifications.ConsultaEmail(email);
                var usuario = _usuarioRepository.AllMatching(spec).SingleOrDefault();
                if (usuario == null)
                {
                    throw new ApplicationValidationErrorsException("Usuário não encontrado.");
                }

                if (usuario.Senha != senha)
                {
                    throw new ApplicationValidationErrorsException("Senha inválida.");
                }

                var adapter = TypeAdapterFactory.CreateAdapter();
                var usuarioDTO = adapter.Adapt<Usuario, UsuarioDTO>(usuario);

                ControladorDeSessao.Autenticar(usuarioDTO);
            }
            catch (ApplicationValidationErrorsException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().LogError(ex);
                throw new Exception("O servidor não respondeu.");
            }
        }

        public void AlterarSenha(string senhaAtual, string novaSenha, string confirmaNovaSenha)
        {
            try
            {
                var usuario = _usuarioRepository.Get(ControladorDeSessao.GetUsuario().Id);
                if (usuario == null)
                {
                    throw new Exception("Usuario da sessao nao encontrado.");
                }

                if (string.IsNullOrWhiteSpace(senhaAtual))
                {
                    throw new ApplicationValidationErrorsException("Informe a senha atual.");
                }

                if (usuario.Senha != senhaAtual)
                {
                    throw new ApplicationValidationErrorsException("A senha atual está incorreta.");
                }

                if (string.IsNullOrWhiteSpace(novaSenha))
                {
                    throw new ApplicationValidationErrorsException("Informe a nova senha.");
                }

                if (novaSenha != confirmaNovaSenha)
                {
                    throw new ApplicationValidationErrorsException("A nova senha e a confirmação da nova senha não conferem.");
                }

                if (usuario.Senha == novaSenha)
                {
                    throw new ApplicationValidationErrorsException("A nova senha não pode ser igual a senha atual.");
                }

                usuario.Senha = senhaAtual;
                _usuarioRepository.Commit();
            }
            catch (ApplicationValidationErrorsException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().LogError(ex);
                throw new Exception("O servidor não respondeu.");
            }
        }

        #endregion

        #region Métodos Privados

        void SalvarUsuario(Usuario usuario)
        {
            // Validando
            var validator = EntityValidatorFactory.CreateValidator();
            if (!validator.IsValid(usuario))
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages<Usuario>(usuario));
            
            var specExisteUsuario = UsuarioSpecifications.ConsultaEmail(usuario.NomeUsuario);
            if (_usuarioRepository.AllMatching(specExisteUsuario).Any())
                throw new ApplicationValidationErrorsException("Já existe um usuário cadastrado com este e-mail.");

            _usuarioRepository.Add(usuario);
            _usuarioRepository.Commit();
        }

        void AlterarUsuario(Usuario persistido, Usuario corrente)
        {
            // Recupera a validação
            var validator = EntityValidatorFactory.CreateValidator();
            if (!validator.IsValid(corrente))
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages<Usuario>(corrente));

            var specExisteUsuario = UsuarioSpecifications.ConsultaEmail(corrente.NomeUsuario);
            if (_usuarioRepository.AllMatching(specExisteUsuario).Where(c => c.Id != persistido.Id).Any())
                throw new ApplicationValidationErrorsException("Já existe um usuário cadastrado com este e-mail.");

            _usuarioRepository.Merge(persistido, corrente);
            _usuarioRepository.Commit();
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            _usuarioRepository.Dispose();
        }

        #endregion
    }
}
