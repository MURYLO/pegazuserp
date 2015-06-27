using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegazusERP.Dominio.Enums
{
    public enum eUnidade
    {
        [Description("Caixa")]
        caixa = 0,

        [Description("Unidade")]
        unidade = 1,

        [Description("kilograma")]
        kilograma = 2,

        [Description("Fardo")]
        fardo = 3,

        [Description("Litro")]
        litro = 4 
    }
}
