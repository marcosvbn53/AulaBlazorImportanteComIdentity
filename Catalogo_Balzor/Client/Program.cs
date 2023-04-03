using Catalogo_Balzor.Client;
using Catalogo_Balzor.Client.Auth;
using Catalogo_Balzor.Client.Services;
using Catalogo_Balzor.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Polly;
using Polly.Extensions.Http;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddAuthorizationCore();




        builder.Services.AddScoped(sp =>
            new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });



        IAsyncPolicy<HttpResponseMessage> tentarNovamentePolicy = Policy.HandleResult<HttpResponseMessage>(
            r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
            }, (outcome, timespan, retryCount, context) =>
            {
                Console.WriteLine($"Tentando pela {retryCount} vez!");
            });

        

        builder.Services.AddSingleton(tentarNovamentePolicy);


        builder.Services.AddHttpClient<IAutenticacaoServices, AutenticacaoServices>();        

        builder.Services.AddScoped<TokenAuthenticationProvider>();


        builder.Services.AddScoped<IAuthorizeService, TokenAuthenticationProvider>(
            provider => provider.GetRequiredService<TokenAuthenticationProvider>());

        builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationProvider>(
            provider => provider.GetRequiredService<TokenAuthenticationProvider>());


        await builder.Build().RunAsync();
    }
}