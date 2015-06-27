using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegazusERP.Dominio.Enums
{
    public enum eTipoProduto
    {
        [Description("Produto")]
        Produto = 0,

        [Description("Serviço")]
        Servico = 1
       
    }
}
