using Catalogo_Balzor.Shared.Models.Response;
using FluentValidation.Results;

namespace Catalogo_Balzor.Shared.Models
{
    public  class UsuarioRespostaLogin
    {
        public UserToken UsuarioToken { get; set; }
        //public ValidationResult Mensagens { get; set; }
        public RespostaDeErros MensagensDeResposta { get; set; }
    }
}
