using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using PegazusERP.Aplicacao.Services;
using PegazusERP.Aplicacao.Services.Interface;
using PegazusERP.Dominio.Aggregates.PessoaAgg;
using PegazusERP.Dominio.Aggregates.ProdutoAgg;
using PegazusERP.Infraestrutura.Adapter;
using PegazusERP.Infraestrutura.Logging;
using PegazusERP.Infraestrutura.Repositories;
using PegazusERP.Infraestrutura.UnitOfWork;
using PegazusERP.Infraestrutura.Validator;
using PPegazusERP.Dominio.Aggregates.UsuarioAgg;
using System.Web.Mvc;

namespace PegazusERP.Web
{
    public class DependencyResolverConfig
    {
        private static UnityContainer _container;

        public static void RegisterDependency()
        {
            // criando nosso container de dependencias e registrando uma dependencia de exemplo.
            _container = new UnityContainer();
            _container.AddNewExtension<Interception>();

            //-> Unit of Work and repositories
            _container.RegisterType(typeof(MainBCUnitOfWork), new PerResolveLifetimeManager());

            _container.RegisterType(typeof(ITypeAdapterFactory), typeof(AutomapperTypeAdapterFactory));
            var typeAdapterFactory = _container.Resolve<ITypeAdapterFactory>();
            TypeAdapterFactory.SetCurrent(typeAdapterFactory);

            #region RegisterType

            // Repositórios
            _container.RegisterType(typeof(IUsuarioRepository), typeof(UsuarioRepository));
            _container.RegisterType(typeof(IPessoaRepository), typeof(PessoaRepository));
            _container.RegisterType(typeof(IMarcaProdutoRepository), typeof(MarcaProdutoRepository));
            _container.RegisterType(typeof(ICategoriaProdutoRepository), typeof(CategoriaProdutoRepository));
            _container.RegisterType(typeof(IProdutoRepository), typeof(ProdutoRepository));

            // Serviços
            _container.RegisterType(typeof(IUsuarioAppService), typeof(UsuarioAppService));//.Configure<Interception>().SetInterceptorFor<IUsuarioAppService>(new InterfaceInterceptor());
            _container.RegisterType(typeof(IPessoaAppService), typeof(PessoaAppService));//.Configure<Interception>().SetInterceptorFor<IPessoaAppService>(new InterfaceInterceptor());
            _container.RegisterType(typeof(IMarcaProdutoAppService), typeof(MarcaProdutoAppService));
            _container.RegisterType(typeof(ICategoriaProdutoAppService), typeof(CategoriaProdutoAppService));
            _container.RegisterType(typeof(IProdutoAppService), typeof(ProdutoAppService));

            #endregion

            //modificando o DependencyResolver para a nossa customização passando o container.
            DependencyResolver.SetResolver(new UnityDependencyResolver(_container));

            LoggerFactory.SetCurrent(new EmailTraceSourceLogFactory());
            EntityValidatorFactory.SetCurrent(new DataAnnotationsEntityValidatorFactory());
        }
    }
}