using Catalogo_Balzor.Client.Shared;
using Catalogo_Balzor.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;

namespace Catalogo_Balzor.Client.Pages.Categorias
{
    public class IndexBase : ComponentBase
    {
        public string perfis = "Registrador, Escrivão, Admin";

        public string AplicarPermissao { get; set; } = " disabled";


        [Inject]
        public HttpClient http { get; set; }

        public List<Categoria> Categorias { get; set; }

        public Confirma ConfirmaDialog { get; set; }

        private int CodigoCategoria { get; set; }

        public string ItemSelecionado { get; set; }

        public int QuantidadeTotalPaginas;
        public int PaginaAtual = 1;

        protected override async Task OnInitializedAsync()
        {
            await CarregarCategoria();
        }

        public async Task PaginaSelecionada(int pagina)
        {
            PaginaAtual = pagina;
            await CarregarCategoria(pagina);
        }

        public async Task CarregarCategoria(int pagina = 1, int quantidadePorPagina = 5)
        {
            var httpResponse = await http.GetAsync($"api/categoria?pagina={pagina}&quantiadePorPagina={quantidadePorPagina}&nome={NomeFiltro}");

            if(httpResponse.IsSuccessStatusCode)
            {
                QuantidadeTotalPaginas = int.Parse(httpResponse.Headers.GetValues("totalPaginas").FirstOrDefault());

                var responseString = await httpResponse.Content.ReadAsStringAsync();

                Categorias = JsonSerializer.Deserialize<List<Categoria>>(responseString,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }


        public async Task DeletarCategoria(int CategoriaId, string itemDescricao)
        {
            ItemSelecionado = itemDescricao;
            ConfirmaDialog.Exibir();
            CodigoCategoria = CategoriaId;
        }

        public async Task DeletaConfirma()
        {
            await http.DeleteAsync($"api/categoria/{CodigoCategoria}");
            ConfirmaDialog.Ocultar();
            await CarregarCategoria();            
        }

        public void CancelaConfirma()
        {
            ConfirmaDialog.Ocultar();
        }

        public string NomeFiltro { get; set; }

        public async Task Filtrar()
        {
            PaginaAtual = 1;
            await CarregarCategoria();
        }

        public async Task LimparFiltro()
        {
            NomeFiltro = string.Empty;
            PaginaAtual = 1;
            await CarregarCategoria();

        }
    }
}
