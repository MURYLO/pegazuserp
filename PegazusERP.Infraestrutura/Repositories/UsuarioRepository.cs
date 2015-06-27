using PegazusERP.Infraestrutura.Base;
using PegazusERP.Dominio.Aggregates.UsuarioAgg;
using PPegazusERP.Dominio.Aggregates.UsuarioAgg;
using PegazusERP.Infraestrutura.UnitOfWork;

namespace PegazusERP.Infraestrutura.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        #region Construtor

        public UsuarioRepository(MainBCUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion
    }
}
