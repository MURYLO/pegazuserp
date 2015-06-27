using Microsoft.Practices.Unity.InterceptionExtension;
using PegazusERP.Aplicacao.AppServices;
using PegazusERP.Aplicacao.Base;

namespace PegazusERP.Aplicacao.AOP
{
    internal class AutenticacaoCallHandler : ICallHandler
    {
        public int Order { get; set; }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            bool autenticado = true;
            autenticado = ControladorDeSessao.EstaAutenticado();

            if (autenticado)
            {
                IMethodReturn result = getNext()(input, getNext);
                return result;
            }
            else
            {
                // TODO: criar tratamento de erro para autenticacao
                return input.CreateExceptionMethodReturn(new ApplicationValidationErrorsException("Usuário não autenticado."));
            }
        }
    }
}
