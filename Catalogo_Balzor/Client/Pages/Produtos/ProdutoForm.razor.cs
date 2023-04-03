using Catalogo_Balzor.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Catalogo_Balzor.Client.Pages.Produtos
{
    public class ProdutoFormBase : ComponentBase
    {
        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
        public HttpClient http { get; set; }

        [Parameter]
        public string ButtonTextSubmit { get; set; }

        [Parameter]
        public string ButtonTextCancelar { get; set; }

        [Parameter]
        public EventCallback OnValidSubmit { get; set; }

        [Parameter]
        public Produto Produto { get; set; }

        [Parameter]
        public string ImagemRemota { get; set; }

        public void ImagemSelecionada(string imagemBase64)
        {
            Produto.ImageUrl = imagemBase64;
            ImagemRemota = null;
            Produto.CategoriaId = Categorias[0].CategoriaId;
        }

        public List<Categoria> Categorias { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await CarregaCategorias();
        }

        public async Task CarregaCategorias()
        {
            Categorias = await http.GetFromJsonAsync<List<Categoria>>("api/categoria/todos");
        }

        public void CategoriaSelectionChange(ChangeEventArgs e)
        {
            if(int.TryParse(e.Value.ToString(), out int id))
            {
                Produto.CategoriaId = id;
            }
        }
    }
}
