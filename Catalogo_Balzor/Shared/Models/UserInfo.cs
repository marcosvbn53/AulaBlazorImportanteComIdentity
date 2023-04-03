using System.ComponentModel.DataAnnotations;

namespace Catalogo_Balzor.Shared.Models
{
    public class UserInfo
    {
        [Required(ErrorMessage = "Informe o e-mail")]
        [EmailAddress(ErrorMessage = "Formato do e-mail inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe o password")]
        public string Password { get; set; }
    }
}
