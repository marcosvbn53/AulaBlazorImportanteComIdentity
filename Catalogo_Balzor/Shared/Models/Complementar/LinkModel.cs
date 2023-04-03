namespace Catalogo_Balzor.Shared.Models.Complementar
{
    public class LinkModel 
    {
        public LinkModel(int page) : this(page, true) { }
        public LinkModel(int page, bool enabled) : this(page, enabled, page.ToString()) { }
        public LinkModel(int page, bool enabled, string text)
        {
            Page = page;
            Enabled = enabled;
            Text = text;
        }

        public string Text { get; set; }
        public int Page { get; set; }
        public bool Enabled { get; set; }
        public bool Active { get; set; }
    }
}
