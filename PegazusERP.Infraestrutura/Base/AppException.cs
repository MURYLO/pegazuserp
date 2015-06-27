using PegazusERP.Infraestrutura.Validator;
using System;
using System.Collections.Generic;

namespace PegazusERP.Infraestrutura.Base
{
    public class AppException : Exception
    {
        #region Propriedades

        IEnumerable<ValidationResult> _validationErrors;

        public IEnumerable<ValidationResult> ValidationErrors
        {
            get
            {
                return _validationErrors;
            }
        }

        #endregion

        #region Construtor

        public AppException(string mensagem) : base(mensagem) { }

        public AppException(IEnumerable<ValidationResult> validationErrors) : base("Verifique os dados informados.")
        {
            _validationErrors = validationErrors;
        }

        #endregion
    }
}
