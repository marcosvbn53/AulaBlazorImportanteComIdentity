using Catalogo_Balzor.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Catalogo_Balzor.Client.Pages.Produtos
{
    public class EditBase :ComponentBase
    {
        [Inject]
        public HttpClient http { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Parameter]
        public int ProdutoId { get; set; }

        public string ImagemRemota { get; set; }

        public Produto Produto { get; set; }

        public EditBase()
        {
            Produto = new Produto();
        }

       
        protected override async Task OnParametersSetAsync()
        {
            Produto = await http.GetFromJsonAsync<Produto>($"api/produto/{ProdutoId}");
            ImagemRemota = Produto.ImageUrl;
        }

        public async Task EditarProduto()
        {
            ImagemRemota = null;
            await http.PutAsJsonAsync("api/produto", Produto);
            navigationManager.NavigateTo("produto");
        }
    }
}
