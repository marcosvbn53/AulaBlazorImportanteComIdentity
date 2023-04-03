using System.Net;

namespace Catalogo_Balzor.Client.Utils
{
    public class CustomizacaoDeExcecoesDeRequest : Exception
    {
        public HttpStatusCode StatusCode;

        public CustomizacaoDeExcecoesDeRequest() { }

        public CustomizacaoDeExcecoesDeRequest(string message, Exception innerException) : base(message, innerException) { }

        public CustomizacaoDeExcecoesDeRequest(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
