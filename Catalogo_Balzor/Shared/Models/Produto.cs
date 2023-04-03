using System.ComponentModel.DataAnnotations;

namespace Catalogo_Balzor.Shared.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "O nome do produto deve ser informado!")]
        [MaxLength(100, ErrorMessage = "O nome do produto deve ter no máximo {1} caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição do produto deve ser informado!")]
        [MaxLength(200, ErrorMessage = "A descrição do produto deve ter no máximo {1} caracteres!")]
        public string Descricao { get; set; }

        public decimal Preco { get; set; }        
        public string ImageUrl { get; set; }

        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }
    }
}