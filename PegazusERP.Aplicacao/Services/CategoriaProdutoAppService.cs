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
    public class CategoriaProdutoAppService : ICategoriaProdutoAppService
    {
        #region Membros

        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;

        #endregion

        #region Construtor

        public CategoriaProdutoAppService(ICategoriaProdutoRepository categoriaProdutoRepository)
        {
            if (categoriaProdutoRepository == null)
                throw new ArgumentNullException("categoriaProdutoRepository");

            _categoriaProdutoRepository = categoriaProdutoRepository;
        }

        #endregion

        #region Membros de ICategoriaProdutoAppService

        public CategoriaProdutoDTO AddCategoriaProduto(CategoriaProdutoDTO categoriaProdutoDTO)
        {
            try
            {
                if (categoriaProdutoDTO == null)
                    throw new Exception("Objeto não instânciado.");

                var categoriaProduto = ProdutoFactory.CreateCategoriaProduto(categoriaProdutoDTO.Nome
                                                        );

                SalvarCategoriaProduto(categoriaProduto);

                var adapter = TypeAdapterFactory.CreateAdapter();
                return adapter.Adapt<CategoriaProduto, CategoriaProdutoDTO>(categoriaProduto);
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

        public void UpdateCategoriaProduto(CategoriaProdutoDTO categoriaProdutoDTO)
        {
            try
            {
                if (categoriaProdutoDTO == null)
                    throw new Exception("Objeto não instânciado.");

                var persistido = _categoriaProdutoRepository.Get(categoriaProdutoDTO.Id);
                if (persistido == null)
                    throw new Exception("CategoriaProduto não encontrado.");

                var corrente = ProdutoFactory.CreateCategoriaProduto(categoriaProdutoDTO.Nome
                                                        );

                corrente.Id = persistido.Id;

                AlterarCategoriaProduto(persistido, corrente);
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

        public void RemoveCategoriaProduto(int CategoriaProdutoId)
        {
            try
            {
                if (CategoriaProdutoId <= 0)
                    throw new Exception("Id da CategoriaProduto inválido.");

                var CategoriaProduto = _categoriaProdutoRepository.Get(CategoriaProdutoId);
                if (CategoriaProduto == null)
                    throw new Exception("CategoriaProduto não encontrado.");

                _categoriaProdutoRepository.Remove(CategoriaProduto);
                _categoriaProdutoRepository.Commit();
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

        public CategoriaProdutoDTO FindCategoriaProduto(int CategoriaProdutoId)
        {
            try
            {
                if (CategoriaProdutoId <= 0)
                    throw new Exception("Id do usuário inválido.");

                var CategoriaProduto = _categoriaProdutoRepository.Get(CategoriaProdutoId);
                if (CategoriaProduto == null)
                    throw new Exception("Usuário não encontrado.");

                var adapter = TypeAdapterFactory.CreateAdapter();
                return adapter.Adapt<CategoriaProduto, CategoriaProdutoDTO>(CategoriaProduto);
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

        public List<CategoriaProdutoDTO> FindCategoriaProdutos<KProperty>(string texto, Expression<Func<CategoriaProduto, KProperty>> orderByExpression, bool ascending = true)
        {
            try
            {
                var spec = ProdutoSpecifications.ConsultaCategoriaProduto(texto);
                List<CategoriaProduto> CategoriaProdutos = _categoriaProdutoRepository.AllMatching<KProperty>(spec, orderByExpression, ascending).ToList();

                var adapter = TypeAdapterFactory.CreateAdapter();
                return adapter.Adapt<List<CategoriaProduto>, List<CategoriaProdutoDTO>>(CategoriaProdutos);
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

        public List<CategoriaProdutoDTO> FindCategoriaProdutos<KProperty>(string texto, Expression<Func<CategoriaProduto, KProperty>> orderByExpression, bool ascending, int pageIndex, int pageCount)
        {
            try
            {
                if (pageIndex <= 0 || pageCount <= 0)
                    throw new Exception("Argumentos da paginação inválidos.");

                var spec = ProdutoSpecifications.ConsultaCategoriaProduto(texto);
                List<CategoriaProduto> CategoriaProdutos = _categoriaProdutoRepository.GetPaged<KProperty>(pageIndex, pageCount, spec, orderByExpression, ascending).ToList();

                var adapter = TypeAdapterFactory.CreateAdapter();
                return adapter.Adapt<List<CategoriaProduto>, List<CategoriaProdutoDTO>>(CategoriaProdutos);
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

        public long CountCategoriaProdutos(string texto)
        {
            try
            {
                var spec = ProdutoSpecifications.ConsultaCategoriaProduto(texto);
                return _categoriaProdutoRepository.Count(spec);
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

        void SalvarCategoriaProduto(CategoriaProduto CategoriaProduto)
        {
            // Validando
            var validator = EntityValidatorFactory.CreateValidator();
            if (!validator.IsValid(CategoriaProduto))
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages<CategoriaProduto>(CategoriaProduto));

            _categoriaProdutoRepository.Add(CategoriaProduto);
            _categoriaProdutoRepository.Commit();
        }

        void AlterarCategoriaProduto(CategoriaProduto persistido, CategoriaProduto corrente)
        {
            // Recupera a validação
            var validator = EntityValidatorFactory.CreateValidator();
            if (!validator.IsValid(corrente))
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages<CategoriaProduto>(corrente));

            _categoriaProdutoRepository.Merge(persistido, corrente);
            _categoriaProdutoRepository.Commit();
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            _categoriaProdutoRepository.Dispose();
        }

        #endregion
    }
}
