using Catalogo_Balzor.Client.Services.Base;
using Catalogo_Balzor.Client.Services.Interfaces;
using Catalogo_Balzor.Shared.Models;
using Catalogo_Balzor.Shared.Models.Response;
using Microsoft.Win32;
using Polly;
using Polly.Extensions.Http;
using System.Security.Principal;

namespace Catalogo_Balzor.Client.Services
{
    public class AutenticacaoServices : BaseService, IAutenticacaoServices
    {
        private readonly HttpClient _httpClient;
        private IAsyncPolicy<HttpResponseMessage> _httpRequestPolicy;

        public AutenticacaoServices(HttpClient httpClient, IAsyncPolicy<HttpResponseMessage> httpRequestPolicy)
        {
            _httpRequestPolicy = httpRequestPolicy;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7118/");
        }

        public async Task<UsuarioRespostaLogin> Login(UserInfo UsuarioLogin)
        {

            var loginConteudo = ObterConteudo(UsuarioLogin);

            var response = await _httpRequestPolicy.ExecuteAsync(ctx =>
                _httpClient.PostAsync("/api/account/login", loginConteudo), context);

            var json = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin
                {
                    MensagensDeResposta = await DeserializarObjeto<RespostaDeErros>(response)
                };
            }

            return await DeserializarObjeto<UsuarioRespostaLogin>(response);
        }

        public async Task<UsuarioRespostaLogin> Registro(UserInfo UsuarioRegistro)
        {
            var registroConteudo = ObterConteudo(UsuarioRegistro);

            var response = await _httpRequestPolicy.ExecuteAsync(ctx =>
                _httpClient.PostAsync("/api/account/register", registroConteudo), context);

            var json = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin
                {
                    MensagensDeResposta = await DeserializarObjeto<RespostaDeErros>(response)
                };
            }

            return await DeserializarObjeto<UsuarioRespostaLogin>(response);
        }
    }
}
