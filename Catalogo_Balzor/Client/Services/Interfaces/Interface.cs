using Catalogo_Balzor.Shared.Models;

namespace Catalogo_Balzor.Client.Services.Interfaces
{
    public interface IAutenticacaoServices
    {
        Task<UsuarioRespostaLogin> Login(UserInfo UsuarioLogin);
        Task<UsuarioRespostaLogin> Registro(UserInfo UsuarioRegistro);
    }
}
