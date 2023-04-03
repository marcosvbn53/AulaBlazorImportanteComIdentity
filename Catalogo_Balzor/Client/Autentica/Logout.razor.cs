using Catalogo_Balzor.Client.BaseComponent;
using Microsoft.AspNetCore.Components;

namespace Catalogo_Balzor.Client.Autentica
{
    public class LogoutBase : BaseScreenComponent
    { 
        [Parameter]
        public string Mensagem { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                LimparMensagens();
                await authStateProvider.Logout();
                Navigation.NavigateTo("/");
            }
            catch
            {
                AdicionarMensagensDeErros("LogOut","Não foi possível realizar o Logout...");             
            }
        }
    }
}
