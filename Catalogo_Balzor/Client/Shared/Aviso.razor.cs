using FluentValidation.Results;
using Microsoft.AspNetCore.Components;

namespace Catalogo_Balzor.Client.Shared
{
    public class AvisoBase : ComponentBase
    {
        [Parameter]
        public bool Exibir { get; set; }

        [Parameter]
        public ValidationResult Mensagens { get; set; }
        
    }
}
