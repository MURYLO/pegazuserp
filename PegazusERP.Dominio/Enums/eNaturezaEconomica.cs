using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegazusERP.Dominio.Enums
{
    public enum eNaturezaEconomica
    {
        [Description("Animal")]
        Animal=0,

        [Description("Animal/Insumos")]
        AnimalInsumos=1,

        [Description("Ativo Imobilizado")]
        AtivoImobilizado=2,

        [Description("Créditos e Ressarcimentos")]
        CreditosRessarcimentos = 3,

        [Description("Energia Elétrica")]
        EnergiaEletrica = 4,

        [Description("Insumos")]
        Insumos = 5,

        [Description("Mercadorias")]
        Mercadorias = 6,

        [Description("Serviços")]
        Servicos = 7,

        [Description("Produção Própria")]
        ProducaoPropria = 8,

        [Description("Serviços de Comunicação")]
        ServicosComunicacao = 9,

        [Description("Serviços de Transporte")]
        ServicosTransporte = 10,

        [Description("Uso ou Consumo")]
        UsoConsumo = 11,

        [Description("Vasilhames ou Sacarias")]
        VasilhamesSacarias = 12,

        [Description("Águas Minerais")]
        AguasMinerais = 13,
    }
}
