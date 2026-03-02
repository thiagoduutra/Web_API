using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dto.Usuario
{
    public class UsuarioCriarDto
    {
        [Required(ErrorMessage ="Digite o usuário")]
        public string User { get; set; }
        [Required(ErrorMessage = "Digite o Nome")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Digite o Sobrenome")]
        public string LastName { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite a senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Digite a Confirmação de Senha"),
        Compare("Password", ErrorMessage ="As senhas não são iguais")]
        public string ConfirmPassword { get; set; }
    }
}
