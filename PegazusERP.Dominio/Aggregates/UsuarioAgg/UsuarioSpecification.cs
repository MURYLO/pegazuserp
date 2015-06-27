using PegazusERP.Domino.Base.Specification;
namespace PegazusERP.Dominio.Aggregates.UsuarioAgg
{
    public static class UsuarioSpecifications
    {
        public static Specification<Usuario> ConsultaTexto(string texto)
        {
            Specification<Usuario> spec = new DirectSpecification<Usuario>(c => true);
            
            if (!string.IsNullOrWhiteSpace(texto))
            {
                spec &= new DirectSpecification<Usuario>(c => c.NomeUsuario.ToUpper().Contains(texto.ToUpper()));
            }

            return spec;
        }

        public static Specification<Usuario> ConsultaEmail(string email)
        {
            if (email != null)
            {
                email = email.Trim();
            }

            Specification<Usuario> spec = new DirectSpecification<Usuario>(c => true);

            return spec;
        }
    }
}
