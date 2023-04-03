using Catalogo_Balzor.Client.Utils;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Catalogo_Balzor.Client.Auth
{
    public class TokenAuthenticationProvider : AuthenticationStateProvider, IAuthorizeService
    {

        private readonly IJSRuntime js;
        private readonly HttpClient http;
        public static readonly string tokenkey = "tokenkey";

        public TokenAuthenticationProvider(IJSRuntime js, HttpClient http)
        {
            this.js = js;
            this.http = http;
        }

        public AuthenticationState AuthStateAtual { get; private set; }

        private AuthenticationState notAuthenticate =>
            new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //Vamos veririfar se exise um token, lembrando que este método é executada quando a aplicação é iniciada

            var token = await js.GetFromLocalStorage(tokenkey);

            if (string.IsNullOrEmpty(token))
            {
                return notAuthenticate;
            }
            AuthStateAtual = CreateAuthenticationState(token);
            return AuthStateAtual;
        }

        public AuthenticationState CreateAuthenticationState(string token)
        {
            //Assim que recebermos o token, vamos grava-lo no header do request na sessão autorization dessa
            //forma poderemos autorizar cada requisição http enviada ao servidor por este cliente 
            http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", token);

            //Extrair as claims
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithiutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());
                    foreach (var parseRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parseRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }
                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp =>
            new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }

        private byte[] ParseBase64WithiutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public async Task Login(string token)
        {
            try
            {
                //Adiciona o token ao localstorage 
                await js.SetInLocalStorage(tokenkey, token);

                //Cria o estado de autenticação JWT
                var authState = CreateAuthenticationState(token);

                //Precisamos notificar o blazor que, houve mudanças no estado de atenticação, ou seja 
                //alguém fez login, então chamamaos o método NotifyAuthenticationStateChanged que se encarregará 
                //de notificar a todos os componentes em cascata permitindo ou bloqueando o
                //acesso a trechos do nosso sitema
                NotifyAuthenticationStateChanged(Task.FromResult(authState));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Logout()
        {
            try
            {
                //Aqui vamos remover o token do LocalStorage e depois temos que limpar o Authorization
                //do header do request de forma que o usuário não tenha mas acesso.
                await js.RemoveItem(tokenkey);
                http.DefaultRequestHeaders.Authorization = null;
                //Aqui iremos notificar ao blazor que não existe um usuário logado, setando para todoso os 
                //componentens que não devem permitir o acesso de um usuário não autenticado
                NotifyAuthenticationStateChanged(Task.FromResult(notAuthenticate));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
