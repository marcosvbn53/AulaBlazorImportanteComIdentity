using Catalogo_Balzor.Client.BaseComponent;
using Catalogo_Balzor.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Catalogo_Balzor.Client.Pages.Catalogo
{
    public class IndexBase : BaseScreenComponent
    {
        public List<Categoria> Categorias { get; set; }

        public List<Produto> Produtos { get; set; }
        private int codigoCategoria { get; set; }

        [Parameter]
        public int CategoriaId { get; set; }

        public IndexBase()
        {
            Produtos = new List<Produto>();
        }

        public async Task CarregaProdutos()
        {
            Produtos = await http.GetFromJsonAsync<List<Produto>>($"api/produto/categorias/{codigoCategoria}");
            //Notificar o componente que o estado dele foi alterado, isso obriga o componente a renderiar 
            StateHasChanged();
        }

        public async void CategoriaSelectionChanged(ChangeEventArgs e)
        {
            if (int.TryParse(e.Value.ToString(), out int id))
            {
                codigoCategoria = id;
                await CarregaProdutos();
            }
        }

        public async Task CarregarCategorias()
        {
            Categorias = new List<Categoria>();
            var categorias = await http.GetFromJsonAsync<List<Categoria>>("api/categoria/todos");
            Categorias.AddRange(categorias);
        }

        protected override async Task OnParametersSetAsync()
        {
            await CarregarCategorias();
            if (Categorias.Count > 0)
            {
                if (CategoriaId == 0)
                {
                    codigoCategoria = Categorias[0].CategoriaId;
                }
                else
                {
                    codigoCategoria = CategoriaId;
                }
                await CarregaProdutos();
            }            
        }
    }
}
