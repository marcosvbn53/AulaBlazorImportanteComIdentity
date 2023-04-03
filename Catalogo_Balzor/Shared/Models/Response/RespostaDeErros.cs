namespace Catalogo_Balzor.Shared.Models.Response
{
    public class RespostaDeErros
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public MensagensDeErros Errors { get; set; }
        
    }
}
