using PegazusERP.Dominio.Aggregates.PessoaAgg;
using PegazusERP.Infraestrutura.Base;
using PegazusERP.Infraestrutura.UnitOfWork;

namespace PegazusERP.Infraestrutura.Repositories
{
    public class PessoaRepository : Repository<Pessoa>, IPessoaRepository
    {
        #region Construtor

        public PessoaRepository(MainBCUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion
    }
}
