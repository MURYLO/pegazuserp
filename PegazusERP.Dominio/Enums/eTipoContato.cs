using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegazusERP.Dominio.Enums
{
    public enum eTipoContato
    {
        [Description("Fixo")]
        Fixo = 0,

        [Description("Residencial")]
        Residencial = 1,

        [Description("Comercial")]
        Comercial = 2,

        [Description("Celular")]
        Celular = 3,

    }
}
