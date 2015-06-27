using Microsoft.Practices.Unity.InterceptionExtension;

namespace PegazusERP.Aplicacao.AOP
{
    internal class AutenticacaoAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(Microsoft.Practices.Unity.IUnityContainer container)
        {
            return new AutenticacaoCallHandler();
        }
    }
}
