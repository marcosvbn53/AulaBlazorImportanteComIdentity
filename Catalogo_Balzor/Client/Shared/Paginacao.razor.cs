using Catalogo_Balzor.Shared.Models.Complementar;
using Microsoft.AspNetCore.Components;

namespace Catalogo_Balzor.Client.Shared
{
    public class PaginacaoBase : ComponentBase
    {
        [Parameter]
        public int PaginaAtual { get; set; }

        [Parameter]
        public int QuantidadeTotalPaginas { get; set; }

        [Parameter]
        public int Raio { get; set; }

        [Parameter]
        public EventCallback<int> PaginaSelecionada { get; set; }

        public List<LinkModel> links;

        private void CarregarPaginas()
        {
            links = new List<LinkModel>();

            var isLinkPaginaAnteriorHabilidade = PaginaAtual != 1;
            var paginaAnterior = PaginaAtual - 1;
            links.Add(new LinkModel(paginaAnterior, isLinkPaginaAnteriorHabilidade, "Anterior"));

            for(int i = 1; i <= QuantidadeTotalPaginas; i++)
            {
                if(i >= PaginaAtual - Raio && i <= PaginaAtual + Raio)
                {
                    links.Add(new LinkModel(i)
                    {
                        Active = PaginaAtual == i
                    });
                }
            }

            var isLinkProximaPaginaHabilitada = PaginaAtual != QuantidadeTotalPaginas;
            var proximaPagina = PaginaAtual + 1;

            links.Add(new LinkModel(proximaPagina, isLinkProximaPaginaHabilitada, "Proxima"));
        }

        protected override void OnParametersSet()
        {
            CarregarPaginas();
        }

        public async Task PaginaSelecionadaLink(LinkModel link)
        {
            if(link.Page == PaginaAtual || !link.Enabled)
            {
                return;
            }
            PaginaAtual = link.Page;
            await PaginaSelecionada.InvokeAsync(link.Page);          
        }
    }
}
