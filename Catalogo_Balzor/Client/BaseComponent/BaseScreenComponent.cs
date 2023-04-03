using Catalogo_Balzor.Client.Auth;
using Catalogo_Balzor.Client.Services.Interfaces;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;

namespace Catalogo_Balzor.Client.BaseComponent
{
    public class BaseScreenComponent : ComponentBase
    {
        [Inject]
        public HttpClient http { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public TokenAuthenticationProvider authStateProvider { get; set; }

        [Inject]
        public IAutenticacaoServices authServico { get; set; }

        public bool ExisteErros()
        {
            return Mensagens.Errors.Any();
        }

        public bool ExisteErros(string pPropriedade, List<string> erros)
        {
            AdicionarMensagensDeErros(pPropriedade, erros);
            return Mensagens.Errors.Any();
        }

        public ValidationResult Mensagens { get; set; } = new ValidationResult();

        public void LimparMensagens()
        {
            Mensagens.Errors.Clear();
        }

        public void AdicionarMensagensDeErros(string TituloOuPropriedade,string Mensagem)
        {
            Mensagens.Errors.Add(new ValidationFailure(TituloOuPropriedade, Mensagem));
        }

        public void AdicionarMensagensDeErros(ValidationResult validationResult)
        {
            Mensagens.Errors.AddRange(validationResult.Errors);
        }

        public void AdicionarMensagensDeErros(string pPropriedade,List<string> erros)
        {
            erros?.ToList()?.ForEach(px =>
            {
                Mensagens.Errors.Add(new ValidationFailure(pPropriedade,px));
            });
            
        }
    }
}
