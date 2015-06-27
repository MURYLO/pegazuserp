using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegazusERP.Dominio.Enums
{
    public enum eTipoEndereco
    {
        [Description("Faturamento")]
        Faturamento = 0,

        [Description("Comercial")]
        Comercial = 1,

        [Description("Entrega")]
        Entrega = 2
    }
}
