using Catalogo_Balzor.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Catalogo_Balzor.Client.Pages.Catalogo
{
    public class DetalheBase:ComponentBase
    {
        [Inject]
        public HttpClient http { get; set; }

        public Produto Produto { get; set; }

        public DetalheBase()
        {
            Produto = new Produto();
        }

        [Parameter]
        public int produtoid { get; set; }

        protected async override Task OnParametersSetAsync()
        {
            Produto = await http.GetFromJsonAsync<Produto>($"api/produto/{produtoid}");
        }


    }
}
