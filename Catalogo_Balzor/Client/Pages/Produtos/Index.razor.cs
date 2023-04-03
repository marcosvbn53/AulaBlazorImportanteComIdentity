using Catalogo_Balzor.Client.Pages.Categorias;
using Catalogo_Balzor.Client.Shared;
using Catalogo_Balzor.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;

namespace Catalogo_Balzor.Client.Pages.Produtos
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public HttpClient http { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        public int CodigoDoProduto { get; set; }

        public List<Produto> Produtos { get; set; }

        public Confirma confirma { get; set; }

        public int QuantidadeTotalPaginas { get; set; }
        public int PaginaAtual { get; set; } = 1;

        protected override async Task OnInitializedAsync()
        {
            await CarregaProduto();
        }

        public async Task PaginaSelecionada(int pagina)
        {
            PaginaAtual = pagina;
            await CarregaProduto(pagina);
        }

        public async Task CarregaProduto(int pagina = 1, int quantidadePorPagina = 5)
        {
            var httpResponse = await http.GetAsync($"api/produto?pagina={pagina}&quantiadePorPagina={quantidadePorPagina}&nome={NomeFiltro}");

            if (httpResponse.IsSuccessStatusCode)
            {
                QuantidadeTotalPaginas = int.Parse(httpResponse.Headers.GetValues("totalPaginas").FirstOrDefault());
                var stringResult = await httpResponse.Content.ReadAsStringAsync();
                Produtos = JsonSerializer.Deserialize<List<Produto>>(stringResult, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            //Produtos = await http.GetFromJsonAsync<List<Produto>>("/api/produto");
        }

        public void DeletarProduto(int codigoProduto)
        {
            CodigoDoProduto = codigoProduto;
            confirma.Exibir();            
        }

        public async Task DeletarConfirma()
        {
            await http.DeleteAsync($"api/produto/{CodigoDoProduto}");
            confirma.Ocultar();
            await CarregaProduto();
        }

        public async Task CancelarDeletar()
        {
            confirma.Ocultar();
        }

        public string NomeFiltro { get; set; }
        public async Task Filtrar()
        {
            PaginaAtual = 1;
            await CarregaProduto();
        }

        public async Task LimparFiltro()
        {
            NomeFiltro = string.Empty;
            PaginaAtual = 1;
            await CarregaProduto();
        }
    }
}
