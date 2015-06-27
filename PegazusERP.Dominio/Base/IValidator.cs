using System.Collections.Generic;

namespace PegazusERP.Dominio.Base
{
    public interface IValidator
    {
        IEnumerable<string[]> Validate();
    }
}
