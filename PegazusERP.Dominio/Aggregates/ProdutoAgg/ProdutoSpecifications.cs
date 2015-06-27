using PegazusERP.Domino.Base.Specification;

namespace PegazusERP.Dominio.Aggregates.ProdutoAgg
{
    public class ProdutoSpecifications
    {
        public static Specification<MarcaProduto> ConsultaMarca(string texto)
        {
            Specification<MarcaProduto> spec = new DirectSpecification<MarcaProduto>(c => true);

            if (!string.IsNullOrEmpty(texto))
            {
                spec = new DirectSpecification<MarcaProduto>(c => c.Nome.Contains(texto));
            }

            return spec;
        }

        public static Specification<CategoriaProduto> ConsultaCategoriaProduto(string texto)
        {
            Specification<CategoriaProduto> spec = new DirectSpecification<CategoriaProduto>(c => true);

            if (!string.IsNullOrEmpty(texto))
            {
                spec = new DirectSpecification<CategoriaProduto>(c => c.Nome.Contains(texto));
            }

            return spec;
        }

        public static Specification<Produto> ConsultaProduto(string texto)
        {
            Specification<Produto> spec = new DirectSpecification<Produto>(c => true);

            if (!string.IsNullOrEmpty(texto))
            {
                spec = new DirectSpecification<Produto>(c => c.Nome.Contains(texto));
            }

            return spec;
        }
    }
}
