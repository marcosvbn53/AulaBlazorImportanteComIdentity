using System.Text.Json;
using System.Text;
using Catalogo_Balzor.Client.Utils;
using Polly;

namespace Catalogo_Balzor.Client.Services.Base
{
    public abstract class BaseService
    {
        protected Context context = new Polly.Context("", new Dictionary<string, object>() { { "message", "" } });
                                  
        protected bool TratarErrosResponse(HttpResponseMessage response)
        {
            //Tratamentos com base no StatusCode
            switch ((int)response.StatusCode)
            {
                case 401:  //Não autorizado, não conhece o usuário
                case 403:  //Acesso negado 
                case 404:  //Recurso não encontrado
                case 500:  //Erro de servidor
                    throw new CustomizacaoDeExcecoesDeRequest(response.StatusCode);

                case 400: return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }

        protected StringContent ObterConteudo(object dados)
        {
            return new StringContent(JsonSerializer.Serialize(dados), Encoding.UTF8, "application/json");
        }

        protected async Task<T> DeserializarObjeto<T>(HttpResponseMessage httpResponseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await httpResponseMessage.Content.ReadAsStringAsync(), options);
        }
    }
}
