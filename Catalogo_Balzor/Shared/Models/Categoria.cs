using System.ComponentModel.DataAnnotations;

namespace Catalogo_Balzor.Shared.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "O nome da categoria é obrigatório!")]
        [MaxLength(100, ErrorMessage = "O nome da categoria deve ter no máximo {1} caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição da categoria é obrigatória!")]
        [MaxLength(200, ErrorMessage = "A descrição da categoria deve ter no máximo {1} caracteres!")]
        public string Descricao { get; set; }

        public ICollection<Produto> Produtos { get; set; }
    }
}
