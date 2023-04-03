using Catalogo_Balzor.Client.BaseComponent;
using Catalogo_Balzor.Shared.Models;

namespace Catalogo_Balzor.Client.Autentica
{
    public class LoginBase : BaseScreenComponent
    {
        public UserInfo UserLogin { get; set; }   

        public LoginBase()
        {
            UserLogin = new UserInfo();
        }

        public async Task FazerLogin()
        {
            UsuarioRespostaLogin resposta = null;
            try
            {
                LimparMensagens();
                resposta = await authServico.Login(UserLogin);
                if(!ExisteErros("Login",resposta?.MensagensDeResposta?.Errors?.Mensagens))
                {
                    await authStateProvider.Login(resposta.UsuarioToken.Token);
                    Navigation.NavigateTo("/");
                }                
            }catch(Exception erro)
            {
                AdicionarMensagensDeErros("Login","Não foi possível realizar login!");             
            }

        }
    }
}
