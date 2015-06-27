using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegazusERP.Dominio.Enums
{
    public enum ePessoa
    {
        [Description("Fisica")]
        Fisica = 0,

        [Description("Juridica")]
        Juridica = 1
    }
}
