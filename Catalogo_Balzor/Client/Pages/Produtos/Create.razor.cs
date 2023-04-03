using Catalogo_Balzor.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Catalogo_Balzor.Client.Pages.Produtos
{
    public class CreateBase: ComponentBase
    {
        [Inject]
        public HttpClient http { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        public string ImagemRemota { get; set; }

        public Produto Produto { get; set; }

        public CreateBase()
        {
            Produto = new Produto();
        }

        public async Task CriarProduto()
        {
            await http.PostAsJsonAsync("api/produto", Produto);
            navigationManager.NavigateTo("/produto");
        }
    }
}
