using Catalogo_Balzor.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Catalogo_Balzor.Client.Pages.Categorias
{
    public class EditBase : ComponentBase
    {
        [Inject]
        public HttpClient http { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Parameter]
        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }

        public EditBase()
        {
            Categoria = new Categoria();
        }

        protected override async Task OnParametersSetAsync()
        {
            Categoria = await http.GetFromJsonAsync<Categoria>($"api/categoria/{CategoriaId}");
        }

        public async Task EditarCategoria()
        {
            await http.PutAsJsonAsync("api/categoria", Categoria);
            navigationManager.NavigateTo("categoria");
        }
    }
}
