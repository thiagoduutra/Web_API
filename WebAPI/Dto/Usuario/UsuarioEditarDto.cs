using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dto.Usuario
{
    public class UsuarioEditarDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o usuário")]
        public string User { get; set; }
        [Required(ErrorMessage = "Digite o Nome")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Digite o Sobrenome")]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

    }
}
