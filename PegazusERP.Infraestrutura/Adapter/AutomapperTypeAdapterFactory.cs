using AutoMapper;
using PegazusERP.Dominio.Aggregates.PessoaAgg;
using PegazusERP.Dominio.Aggregates.ProdutoAgg;
using PegazusERP.Dominio.Aggregates.UsuarioAgg;
using PegazusERP.DTO;


namespace PegazusERP.Infraestrutura.Adapter
{
    public class AutomapperTypeAdapterFactory : ITypeAdapterFactory
    {
        #region Constructor

        /// <summary>
        /// Create a new Automapper type adapter factory
        /// </summary>
        public AutomapperTypeAdapterFactory()
        {
            // Mapeamentos
            Mapper.CreateMap<Pessoa, PessoaDTO>();
            Mapper.CreateMap<Pessoa, PessoaListDTO>();
            Mapper.CreateMap<Usuario, UsuarioDTO>();
            Mapper.CreateMap<MarcaProduto, MarcaProdutoDTO>();
            Mapper.CreateMap<CategoriaProduto, CategoriaProdutoDTO>();
            Mapper.CreateMap<Produto, ProdutoDTO>();
        }

        #endregion

        #region ITypeAdapterFactory Members

        public ITypeAdapter Create()
        {
            return new AutomapperTypeAdapter();
        }

        #endregion
    }
}
