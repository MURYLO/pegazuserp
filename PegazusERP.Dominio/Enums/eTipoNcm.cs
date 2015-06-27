using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegazusERP.Dominio.Enums
{
    public enum eTipoNcm
    {
        [Description("Ativo Imobilizado")]
        AtivoImobilizado = 0,

        [Description("Embalagem")]
        Embalagem=1,

        [Description("Garrafa")]
        Garrafa=2,

        [Description("Matéria Prima")]
        MateriaPrima=3,

        [Description("Material de Uso e Consumo")]
        MaterialdeUsoConsumo=4,

        [Description("Mercadoria para Revenda")]
        Mercadoriapararevenda=5,

        [Description("Outras")]
        Outras=6,

        [Description("Outros Insumos")]
        Outrosinsumos=7,

        [Description("Produto Acabado")]
        ProdutoAcabado=8,

        [Description("Produto em Processo")]
        ProdutoemProcesso=9,

        [Description("Produto Intermediário")]
        ProdutoIntermediario=10,

        [Description("Serviços")]
        Servicos=11,

        [Description("SubProduto")]
        Subproduto=12
    }
}
