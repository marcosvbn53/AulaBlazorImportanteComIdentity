using Microsoft.AspNetCore.Components;
using W8lessLabs.Blazor.LocalFiles;

namespace Catalogo_Balzor.Client.Shared
{
    public class InputImagemBase :ComponentBase
    {
        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string ImagemRemota { get; set; }

        [Parameter]
        public EventCallback<string> ImagemSelecionada { get; set; }

        public string ImagemBase64 { get; set; }

        public FileSelect imagemSelecaoArquivo { get; set; }

        public void AbrirDialogoSelecaoDeImagem()
        {
            imagemSelecaoArquivo.SelectFiles();
        }

        public async Task ObterImagemSelecionada(SelectedFile[] arquivosSelecionados)
        {
            var arquivoSelecionado = arquivosSelecionados.FirstOrDefault();

            var fileBytes = await imagemSelecaoArquivo.GetFileBytesAsync(arquivoSelecionado.Name);

            ImagemBase64 = Convert.ToBase64String(fileBytes);

            await ImagemSelecionada.InvokeAsync(ImagemBase64);

            ImagemRemota = null;

            StateHasChanged();
        }
    }
}
