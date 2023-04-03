using Microsoft.AspNetCore.Components;

namespace Catalogo_Balzor.Client.Shared
{
    public class ConfirmaBase : ComponentBase
    {
        [Parameter]
        public bool ExibirConfirmacao { get; set; }

        [Parameter]
        public string Titulo { get; set; } = "Confirmar exclusão!";

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public EventCallback OnConfirma { get; set; }

        [Parameter]
        public EventCallback OnCancelar { get; set; }

        public void Exibir() => ExibirConfirmacao = true;
        public void Ocultar() => ExibirConfirmacao = false;
    }
}
