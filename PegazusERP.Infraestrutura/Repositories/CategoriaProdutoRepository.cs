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
    public class CategoriaProdutoRepository: Repository<CategoriaProduto>, ICategoriaProdutoRepository
    {
        public CategoriaProdutoRepository(MainBCUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
                
        }
    }
}










