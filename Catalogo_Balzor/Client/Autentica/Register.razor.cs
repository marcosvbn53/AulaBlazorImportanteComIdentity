using Catalogo_Balzor.Client.BaseComponent;
using Catalogo_Balzor.Shared.Models;

namespace Catalogo_Balzor.Client.Autentica
{
    public class Registerbase : BaseScreenComponent
    {
        public Registerbase()
        {
            UsuarioRegistro = new UserInfo();
        }

        public UserInfo UsuarioRegistro { get; set; }

        public async Task RegistrarUsuario()
        {
            UsuarioRespostaLogin usuarioRespostaLogin = null;
            try
            {
                LimparMensagens();
                if (!ExisteErros("Registro",usuarioRespostaLogin?.MensagensDeResposta?.Errors?.Mensagens))
                {
                    var registro = await authServico.Registro(UsuarioRegistro);
                    Navigation.NavigateTo("/login");
                }
            }
            catch
            {
                AdicionarMensagensDeErros("Registro", "Não foi possível registrar o usuário!");                
            }
        }

    }
}
