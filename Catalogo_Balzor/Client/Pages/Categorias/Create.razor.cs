using Catalogo_Balzor.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace Catalogo_Balzor.Client.Pages.Categorias
{
    public class CreateBase : ComponentBase
    {
        [Inject]
        public HttpClient http { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        public Categoria Categoria { get; set; }

        public CreateBase()
        {
            Categoria = new Categoria();
        }

        public async Task CriarCategoria()
        {
            await http.PostAsJsonAsync("api/categoria", Categoria);
            navigationManager.NavigateTo("categoria");
        }

        public async Task Focus(string elementId)
        {
            await Js.InvokeVoidAsync("focusById", elementId);
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                await Focus("txtNomeCategoria");
            }
        }
    }
}
