using PegazusERP.Dominio.Aggregates.PessoaAgg;
using PegazusERP.Dominio.Base;
using PegazusERP.Dominio.Enums;
using System.Collections.Generic;

namespace PegazusERP.Dominio.Aggregates.UsuarioAgg
{
    public class Usuario : Entity, IValidator
    {
        #region Propriedades

        public ePermissao? Permissao { get; set; }

        public string NomeUsuario { get; set; }

        public string Senha { get; set; }

       // public eAtivo? Ativo { get; set; }

        public bool Ativo { get; set; }

        public int PessoaId { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        #endregion

        #region Métodos públicos
        

        
        #endregion

        #region Métodos privados



        #endregion

        #region Validação

        public IEnumerable<string[]> Validate()
        {
            var validationResults = new List<string[]>();

            if (string.IsNullOrWhiteSpace(NomeUsuario))
            {
                validationResults.Add(new string[] { "Nome é obrigatório.", "NomeUsuario" });
            }
            else if (NomeUsuario.Length > 100)
            {
                validationResults.Add(new string[] { "Nome máximo 100.", "NomeUsuario" });
            }

            if (!Ativo)
            {
                validationResults.Add(new string[] { "Ativo é obrigatório", "Ativo" });
            }

            return validationResults;
        }

        #endregion
    }
}
