using PegazusERP.Aplicacao.Base;
using PegazusERP.Aplicacao.Services.Interface;
using PegazusERP.Dominio.Aggregates.ProdutoAgg;
using PegazusERP.DTO;
using PegazusERP.Infraestrutura.Adapter;
using PegazusERP.Infraestrutura.Logging;
using PegazusERP.Infraestrutura.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PegazusERP.Aplicacao.Services
{
    public class ProdutoAppService : IProdutoAppService
    {
        #region Membros

        private readonly IProdutoRepository _produtoRepository;

        #endregion

        #region Construtor

        public ProdutoAppService(IProdutoRepository produtoRepository)
        {
            if (produtoRepository == null)
                throw new ArgumentNullException("produtoRepository");

            _produtoRepository = produtoRepository;
        }

        #endregion

        #region Membros de IProdutoAppService

        public ProdutoDTO AddProduto(ProdutoDTO produtoDTO)
        {
            try
            {
                if (produtoDTO == null)
                    throw new Exception("Objeto não instânciado.");

                var Produto = ProdutoFactory.CreateProduto(  produtoDTO.Nome,
                                                             produtoDTO.MarcaProdutoId,
                                                             produtoDTO.CategoriaProdutoId,
                                                             produtoDTO.CodigoBarras,
                                                             produtoDTO.UsaBalanca,
                                                             produtoDTO.Ativo,
                                                             produtoDTO.EstoqueAtual,
                                                             produtoDTO.Modelo,
                                                             produtoDTO.Custo,
                                                             produtoDTO.Venda,
                                                             produtoDTO.Unidade,
                                                             produtoDTO.MovimentaEstoque,
                                                             produtoDTO.TipoNcm,
                                                             produtoDTO.Ncm,
                                                             produtoDTO.NaturezaEconomica,
                                                             produtoDTO.TipoProduto,
                                                             produtoDTO.ObjetivoComercial,
                                                             produtoDTO.Referencia,
                                                             produtoDTO.ReferenciaAux,
                                                             produtoDTO.LocalEstoque,
                                                             produtoDTO.AceitaSaldoNegativo,
                                                             produtoDTO.QuantidadeMinimaEstoque
                                                             );

                SalvarProduto(Produto);

                var adapter = TypeAdapterFactory.CreateAdapter();
                return adapter.Adapt<Produto, ProdutoDTO>(Produto);
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

        public void UpdateProduto(ProdutoDTO produtoDTO)
        {
            try
            {
                if (produtoDTO == null)
                    throw new Exception("Objeto não instânciado.");

                var persistido = _produtoRepository.Get(produtoDTO.Id);
                if (persistido == null)
                    throw new Exception("Produto não encontrado.");

                var corrente = ProdutoFactory.CreateProduto( produtoDTO.Nome,
                                                             produtoDTO.MarcaProdutoId,
                                                             produtoDTO.CategoriaProdutoId,
                                                             persistido.CodigoBarras,
                                                             produtoDTO.UsaBalanca,
                                                             produtoDTO.Ativo,
                                                             persistido.EstoqueAtual,
                                                             produtoDTO.Modelo,
                                                             produtoDTO.Custo,
                                                             produtoDTO.Venda,
                                                             persistido.Unidade,
                                                             produtoDTO.MovimentaEstoque,
                                                             produtoDTO.TipoNcm,
                                                             produtoDTO.Ncm,
                                                             produtoDTO.NaturezaEconomica,
                                                             produtoDTO.TipoProduto,
                                                             produtoDTO.ObjetivoComercial,
                                                             produtoDTO.Referencia,
                                                             produtoDTO.ReferenciaAux,
                                                             produtoDTO.LocalEstoque,
                                                             produtoDTO.AceitaSaldoNegativo,
                                                             produtoDTO.QuantidadeMinimaEstoque);

                corrente.Id = persistido.Id;

                AlterarProduto(persistido, corrente);
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

        public void RemoveProduto(int ProdutoId)
        {
            try
            {
                if (ProdutoId <= 0)
                    throw new Exception("Id da Produto inválido.");

                var Produto = _produtoRepository.Get(ProdutoId);
                if (Produto == null)
                    throw new Exception("Produto não encontrado.");

                _produtoRepository.Remove(Produto);
                _produtoRepository.Commit();
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

        public ProdutoDTO FindProduto(int ProdutoId)
        {
            try
            {
                if (ProdutoId <= 0)
                    throw new Exception("Id do usuário inválido.");

                var Produto = _produtoRepository.Get(ProdutoId);
                if (Produto == null)
                    throw new Exception("Usuário não encontrado.");

                var adapter = TypeAdapterFactory.CreateAdapter();
                return adapter.Adapt<Produto, ProdutoDTO>(Produto);
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

        public List<ProdutoDTO> FindProdutos<KProperty>(string texto, Expression<Func<Produto, KProperty>> orderByExpression, bool ascending = true)
        {
            try
            {
                var spec = ProdutoSpecifications.ConsultaProduto(texto);
                List<Produto> Produtos = _produtoRepository.AllMatching<KProperty>(spec, orderByExpression, ascending).ToList();

                var adapter = TypeAdapterFactory.CreateAdapter();
                return adapter.Adapt<List<Produto>, List<ProdutoDTO>>(Produtos);
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

        public List<ProdutoDTO> FindProdutos<KProperty>(string texto, Expression<Func<Produto, KProperty>> orderByExpression, bool ascending, int pageIndex, int pageCount)
        {
            try
            {
                if (pageIndex <= 0 || pageCount <= 0)
                    throw new Exception("Argumentos da paginação inválidos.");

                var spec = ProdutoSpecifications.ConsultaProduto(texto);
                List<Produto> Produtos = _produtoRepository.GetPaged<KProperty>(pageIndex, pageCount, spec, orderByExpression, ascending).ToList();

                var adapter = TypeAdapterFactory.CreateAdapter();
                return adapter.Adapt<List<Produto>, List<ProdutoDTO>>(Produtos);
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

        public long CountProdutos(string texto)
        {
            try
            {
                var spec = ProdutoSpecifications.ConsultaProduto(texto);
                return _produtoRepository.Count(spec);
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

        void SalvarProduto(Produto Produto)
        {
            // Validando
            var validator = EntityValidatorFactory.CreateValidator();
            if (!validator.IsValid(Produto))
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages<Produto>(Produto));
    
            _produtoRepository.Add(Produto);
            _produtoRepository.Commit();
        }

        void AlterarProduto(Produto persistido, Produto corrente)
        {
            // Recupera a validação
            var validator = EntityValidatorFactory.CreateValidator();
            if (!validator.IsValid(corrente))
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages<Produto>(corrente));
                    
            _produtoRepository.Merge(persistido, corrente);
            _produtoRepository.Commit();
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            _produtoRepository.Dispose();
        }

        #endregion
    }
}
