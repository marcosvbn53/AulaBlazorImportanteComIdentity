using Catalogo_Balzor.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Catalogo_Balzor.Client.Pages.Categorias
{
    public class CategoriaFormBase: ComponentBase
    {
        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Parameter]
        public Categoria Categoria { get; set; }

        [Parameter]
        public string ButtonTextSubmit { get; set; } = "Salvar";

        [Parameter]
        public string ButtonTextCancelar { get; set; } = "Cancelar";

        [Parameter]
        public EventCallback OnValidSubmit { get; set;
        }
    }
}
