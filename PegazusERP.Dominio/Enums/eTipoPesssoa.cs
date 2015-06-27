using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegazusERP.Dominio.Enums
{
    public enum eTipoPesssoa
    {
        [Description("Cliente")]
        Cliente = 0,

        [Description("Agência")]
        Agencia = 1,

        [Description("Vendedor")]
        Vendedor = 2,

        [Description("Fornecedor")]
        Fornecedor = 3,

        [Description("Funcionário")]
        Funcionario = 4,

        [Description("Usuário")]
        Usuario = 5,

        [Description("Transportadora")]
        Transportadora = 6,

        [Description("Técnico")]
        Tecnico = 7,

        [Description("Entregador")]
        Entregador = 8,

        [Description("Motorista")]
        Motorista = 9,

    }
}
