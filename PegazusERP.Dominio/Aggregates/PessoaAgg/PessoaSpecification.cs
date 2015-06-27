using PegazusERP.Domino.Base.Specification;

namespace PegazusERP.Dominio.Aggregates.PessoaAgg
{
    public static class PessoaSpecifications
    {
        public static Specification<Pessoa> Consulta()
        {
            Specification<Pessoa> spec = new DirectSpecification<Pessoa>(c => true);
            
            return spec;
        }
    }
}
