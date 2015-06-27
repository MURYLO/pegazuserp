using PegazusERP.Dominio.Aggregates.ProdutoAgg;
using PegazusERP.Infraestrutura.Base;
using PegazusERP.Infraestrutura.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegazusERP.Infraestrutura.Repositories
{
    public class ProdutoRepository: Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(MainBCUnitOfWork unitOfWork)
            :base(unitOfWork)
        {

        }
    }
}
