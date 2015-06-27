
using PegazusERP.Aplicacao.Base;
using PegazusERP.Aplicacao.Services.Interface;
using PegazusERP.DTO;
using PegazusERP.Infraestrutura.Adapter;
using PegazusERP.Infraestrutura.Logging;
using PegazusERP.Infraestrutura.Util;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using PegazusERP.Infraestrutura.Validator;
using PegazusERP.Dominio.Aggregates.ProdutoAgg;

namespace PegazusERP.Aplicacao.Services
{
    public class MarcaProdutoAppService : IMarcaProdutoAppService
    {
        #region Membros

        private readonly IMarcaProdutoRepository _marcaProdutoRepository;

        #endregion

        #region Construtor

        public MarcaProdutoAppService(IMarcaProdutoRepository marcaProdutoRepository)
        {
            if (marcaProdutoRepository == null)
                throw new ArgumentNullException("marcaProdutoRepository");

            _marcaProdutoRepository = marcaProdutoRepository;
        }

        #endregion

        #region Membros de IMarcaProdutoAppService

        public MarcaProdutoDTO AddMarcaProduto(MarcaProdutoDTO MarcaProdutoDTO)
        {
            try
            {
                if (MarcaProdutoDTO == null)
                    throw new Exception("Objeto não instânciado.");

                var marcaProduto = ProdutoFactory.CreateMarcaProduto(MarcaProdutoDTO.Nome
                                                        );

                SalvarMarcaProduto(marcaProduto);

                var adapter = TypeAdapterFactory.CreateAdapter();
                return adapter.Adapt<MarcaProduto, MarcaProdutoDTO>(marcaProduto);
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

        public void UpdateMarcaProduto(MarcaProdutoDTO marcaProdutoDTO)
        {
            try
            {
                if (marcaProdutoDTO == null)
                    throw new Exception("Objeto não instânciado.");

                var persistido = _marcaProdutoRepository.Get(marcaProdutoDTO.Id);
                if (persistido == null)
                    throw new Exception("MarcaProduto não encontrado.");

                var corrente = ProdutoFactory.CreateMarcaProduto(marcaProdutoDTO.Nome
                                                        );

                corrente.Id = persistido.Id;

                AlterarMarcaProduto(persistido, corrente);
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

        public void RemoveMarcaProduto(int MarcaProdutoId)
        {
            try
            {
                if (MarcaProdutoId <= 0)
                    throw new Exception("Id da MarcaProduto inválido.");

                var MarcaProduto = _marcaProdutoRepository.Get(MarcaProdutoId);
                if (MarcaProduto == null)
                    throw new Exception("MarcaProduto não encontrado.");

                _marcaProdutoRepository.Remove(MarcaProduto);
                _marcaProdutoRepository.Commit();
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

        public MarcaProdutoDTO FindMarcaProduto(int MarcaProdutoId)
        {
            try
            {
                if (MarcaProdutoId <= 0)
                    throw new Exception("Id do usuário inválido.");

                var MarcaProduto = _marcaProdutoRepository.Get(MarcaProdutoId);
                if (MarcaProduto == null)
                    throw new Exception("Usuário não encontrado.");

                var adapter = TypeAdapterFactory.CreateAdapter();
                return adapter.Adapt<MarcaProduto, MarcaProdutoDTO>(MarcaProduto);
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

        public List<MarcaProdutoDTO> FindMarcaProdutos<KProperty>(string texto, Expression<Func<MarcaProduto, KProperty>> orderByExpression, bool ascending = true)
        {
            try
            {
                var spec = ProdutoSpecifications.ConsultaMarca(texto);
                List<MarcaProduto> MarcaProdutos = _marcaProdutoRepository.AllMatching<KProperty>(spec, orderByExpression, ascending).ToList();

                var adapter = TypeAdapterFactory.CreateAdapter();
                return adapter.Adapt<List<MarcaProduto>, List<MarcaProdutoDTO>>(MarcaProdutos);
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

        public List<MarcaProdutoDTO> FindMarcaProdutos<KProperty>(string texto, Expression<Func<MarcaProduto, KProperty>> orderByExpression, bool ascending, int pageIndex, int pageCount)
        {
            try
            {
                if (pageIndex <= 0 || pageCount <= 0)
                    throw new Exception("Argumentos da paginação inválidos.");

                var spec = ProdutoSpecifications.ConsultaMarca(texto);
                List<MarcaProduto> MarcaProdutos = _marcaProdutoRepository.GetPaged<KProperty>(pageIndex, pageCount, spec, orderByExpression, ascending).ToList();

                var adapter = TypeAdapterFactory.CreateAdapter();
                return adapter.Adapt<List<MarcaProduto>, List<MarcaProdutoDTO>>(MarcaProdutos);
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

        public long CountMarcaProdutos(string texto)
        {
            try
            {
                var spec = ProdutoSpecifications.ConsultaMarca(texto);
                return _marcaProdutoRepository.Count(spec);
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

        void SalvarMarcaProduto(MarcaProduto MarcaProduto)
        {
            // Validando
            var validator = EntityValidatorFactory.CreateValidator();
            if (!validator.IsValid(MarcaProduto))
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages<MarcaProduto>(MarcaProduto));
            

            _marcaProdutoRepository.Add(MarcaProduto);
            _marcaProdutoRepository.Commit();
        }

        void AlterarMarcaProduto(MarcaProduto persistido, MarcaProduto corrente)
        {
            // Recupera a validação
            var validator = EntityValidatorFactory.CreateValidator();
            if (!validator.IsValid(corrente))
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages<MarcaProduto>(corrente));

            _marcaProdutoRepository.Merge(persistido, corrente);
            _marcaProdutoRepository.Commit();
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            _marcaProdutoRepository.Dispose();
        }

        #endregion
    }
}
