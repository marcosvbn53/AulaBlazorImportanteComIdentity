using Catalogo_Balzor.Client.BaseComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Catalogo_Balzor.Client.Pages
{
    public class indexBase : BaseScreenComponent
    {
        public string Perfis = "Admin, Registrador, Escrivão";

        [CascadingParameter]
        private Task<AuthenticationState> authenticationState { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationState;
            if (authState.User.Identity.IsAuthenticated)
            {
                Console.WriteLine($"Bem-vindo {authState.User.Identity.Name}");
                if(authState.User.IsInRole("Admin"))
                {
                    Console.WriteLine("Você faz parte do perfil Admin");
                }
            }
            else
            {
                Console.WriteLine("Usuário não autenticado");
            }
        }
    }
}
