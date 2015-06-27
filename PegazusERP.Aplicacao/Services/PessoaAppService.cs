using PegazusERP.Aplicacao.Base;
using PegazusERP.Aplicacao.Services.Interface;
using PegazusERP.Dominio.Aggregates.PessoaAgg;
using PegazusERP.DTO;
using PegazusERP.Infraestrutura.Adapter;
using PegazusERP.Infraestrutura.Logging;
using PegazusERP.Infraestrutura.Util;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using PegazusERP.Infraestrutura.Validator;

namespace PegazusERP.Aplicacao.Services
{
    public class PessoaAppService : IPessoaAppService
    {
        #region Membros

        private readonly IPessoaRepository _pessoaRepository;

        #endregion

        #region Construtor

        public PessoaAppService(IPessoaRepository pessoaRepository)
        {
            if (pessoaRepository == null)
                throw new ArgumentNullException("pessoaRepository");

            _pessoaRepository = pessoaRepository;
        }

        #endregion

        #region Membros de IPessoaAppService

        public PessoaDTO AddPessoa(PessoaDTO pessoaDTO)
        {
            try
            {
                if (pessoaDTO == null)
                    throw new Exception("Objeto não instânciado.");

                var pessoa = PessoaFactory.CreatePessoa(pessoaDTO.Nome, 
                                                        pessoaDTO.Cpf,
                                                        pessoaDTO.Cnpj,
                                                        pessoaDTO.RazaoSocial,
                                                        pessoaDTO.NomeFantasia,
                                                        pessoaDTO.LimiteCredito,
                                                        pessoaDTO.Email,
                                                        DateTime.Now,
                                                        DateTime.Now,
                                                        true,
                                                        pessoaDTO.VendedorId,
                                                        pessoaDTO.Ie,
                                                        pessoaDTO.Sexo,
                                                        pessoaDTO.EstadoCivil,
                                                        pessoaDTO.DataNascimento,
                                                        pessoaDTO.Escolaridade,
                                                        pessoaDTO.Profissao,
                                                        pessoaDTO.Nacionalidade,
                                                        pessoaDTO.TipoPessoa,
                                                        pessoaDTO.CompraPrazo,
                                                        pessoaDTO.SubISS,
                                                        pessoaDTO.ObjetivoComercial,
                                                        pessoaDTO.RetemImpostos,
                                                        pessoaDTO.FisicaJuridica,
                                                        pessoaDTO.PercentualVista,
                                                        pessoaDTO.PercentualPrazo);

                SalvarPessoa(pessoa);

                var adapter = TypeAdapterFactory.CreateAdapter();
                return adapter.Adapt<Pessoa, PessoaDTO>(pessoa);
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

        public void UpdatePessoa(PessoaDTO pessoaDTO)
        {
            try
            {
                if (pessoaDTO == null)
                    throw new Exception("Objeto não instânciado.");

                var spec = PessoaSpecifications.Consulta();
                var persistido = _pessoaRepository.Get(pessoaDTO.Id);
                if (persistido == null)
                    throw new Exception("Pessoa não encontrado.");

                var corrente = PessoaFactory.CreatePessoa(pessoaDTO.Nome,
                                                        persistido.Cpf,
                                                        persistido.Cnpj,
                                                        persistido.RazaoSocial,
                                                        persistido.NomeFantasia,
                                                        pessoaDTO.LimiteCredito,
                                                        pessoaDTO.Email,
                                                        persistido.DataCadastro,
                                                        DateTime.Now,
                                                        pessoaDTO.Ativo,
                                                        pessoaDTO.VendedorId,
                                                        persistido.Ie,
                                                        persistido.Sexo,
                                                        pessoaDTO.EstadoCivil,
                                                        persistido.DataNascimento,
                                                        pessoaDTO.Escolaridade,
                                                        pessoaDTO.Profissao,
                                                        pessoaDTO.Nacionalidade,
                                                        persistido.TipoPessoa,
                                                        pessoaDTO.CompraPrazo,
                                                        pessoaDTO.SubISS,
                                                        pessoaDTO.ObjetivoComercial,
                                                        pessoaDTO.RetemImpostos,
                                                        persistido.FisicaJuridica,
                                                        pessoaDTO.PercentualVista,
                                                        pessoaDTO.PercentualPrazo);

                corrente.Id = persistido.Id;

                AlterarPessoa(persistido, corrente);
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

        public void RemovePessoa(int pessoaId)
        {
            try
            {
                if (pessoaId <= 0)
                    throw new Exception("Id da pessoa inválido.");

                var pessoa = _pessoaRepository.Get(pessoaId);
                if (pessoa == null)
                    throw new Exception("Pessoa não encontrado.");

                var spec = PessoaSpecifications.Consulta();
                if (_pessoaRepository.Count(spec) <= 1)
                    throw new ApplicationValidationErrorsException("Não é possível excluir o único usuário do sistema.");

                _pessoaRepository.Remove(pessoa);
                _pessoaRepository.Commit();
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

        public PessoaDTO FindPessoa(int pessoaId)
        {
            try
            {
                if (pessoaId <= 0)
                    throw new Exception("Id do usuário inválido.");

                var spec = PessoaSpecifications.Consulta();
                var pessoa = _pessoaRepository.Get(pessoaId);
                if (pessoa == null)
                    throw new Exception("Usuário não encontrado.");

               // pessoa.Nome = Encryption.DecryptToString(pessoa.Nome);

                var adapter = TypeAdapterFactory.CreateAdapter();
                return adapter.Adapt<Pessoa, PessoaDTO>(pessoa);
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

        public List<PessoaDTO> FindPessoas<KProperty>(string texto, Expression<Func<Pessoa, KProperty>> orderByExpression, bool ascending = true)
        {
            try
            {
                var spec = PessoaSpecifications.Consulta();
                List<Pessoa> pessoas = _pessoaRepository.AllMatching<KProperty>(spec, orderByExpression, ascending).ToList();

                var adapter = TypeAdapterFactory.CreateAdapter();
                return adapter.Adapt<List<Pessoa>, List<PessoaDTO>>(pessoas);
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

        public List<PessoaDTO> FindPessoas<KProperty>(string texto, Expression<Func<Pessoa, KProperty>> orderByExpression, bool ascending, int pageIndex, int pageCount)
        {
            try
            {
                if (pageIndex <= 0 || pageCount <= 0)
                    throw new Exception("Argumentos da paginação inválidos.");

                var spec = PessoaSpecifications.Consulta();
                List<Pessoa> pessoas = _pessoaRepository.GetPaged<KProperty>(pageIndex, pageCount, spec, orderByExpression, ascending).ToList();

                var adapter = TypeAdapterFactory.CreateAdapter();
                return adapter.Adapt<List<Pessoa>, List<PessoaDTO>>(pessoas);
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

        public long CountPessoas(string texto)
        {
            try
            {
                var spec = PessoaSpecifications.Consulta();
                return _pessoaRepository.Count(spec);
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

        void SalvarPessoa(Pessoa pessoa)
        {
            // Validando
            var validator = EntityValidatorFactory.CreateValidator();
            if (!validator.IsValid(pessoa))
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages<Pessoa>(pessoa));
            
            //var specExistePessoa = PessoaSpecifications.Consulta();
            //if (_pessoaRepository.AllMatching(specExistePessoa).Any())
            //    throw new ApplicationValidationErrorsException("Já existe um usuário cadastrado com este e-mail.");

            _pessoaRepository.Add(pessoa);
            _pessoaRepository.Commit();
        }

        void AlterarPessoa(Pessoa persistido, Pessoa corrente)
        {
            // Recupera a validação
            var validator = EntityValidatorFactory.CreateValidator();
            if (!validator.IsValid(corrente))
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages<Pessoa>(corrente));

            //var specExistePessoa = PessoaSpecifications.Consulta();
            //if (_pessoaRepository.AllMatching(specExistePessoa).Where(c => c.Id != persistido.Id).Any())
            //    throw new ApplicationValidationErrorsException("Já existe um usuário cadastrado com este e-mail.");

            //var spec = PessoaSpecifications.Consulta();
            //if (_pessoaRepository.Count(spec) <= 1)
            //    throw new ApplicationValidationErrorsException("Não é possível inativar o único usuário ativo do sistema.");

            _pessoaRepository.Merge(persistido, corrente);
            _pessoaRepository.Commit();
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            _pessoaRepository.Dispose();
        }

        #endregion
    }
}
