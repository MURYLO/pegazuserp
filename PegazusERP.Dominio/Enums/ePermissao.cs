using System.ComponentModel;

namespace PegazusERP.Dominio.Enums
{
    public enum ePermissao
    {
        [Description("Administrador")]
        Administrador = 1,

        [Description("Atendente")]
        Atendente = 2,

        [Description("Vendedor")]
        Vendedor = 3,

        [Description("Caixa")]
        Caixa = 4,

        [Description("Gerente")]
        Gerente = 5,

        [Description("Estoque")]
        Estoque = 6,

        [Description("Técnico")]
        Tecnico = 7,
    }
}
