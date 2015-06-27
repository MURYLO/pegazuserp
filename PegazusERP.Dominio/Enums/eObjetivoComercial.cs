using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegazusERP.Dominio.Enums
{
    public enum eObjetivoComercial
    {
        [Description("Compra")]
        Compra = 0,

        [Description("Revenda")]
        Revenda = 1,

        [Description("Ativo Imobilizado")]
        AtivoImobilizado = 2,

        [Description("Consumo")]
        Consumo =3,

        [Description("UsoEmServiço")]
        UsoEmServico=4,

        [Description("Comercialização")]
        Comercializacao = 5
    }
}
