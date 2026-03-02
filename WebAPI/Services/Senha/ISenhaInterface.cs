using WebAPI.Models;

namespace WebAPI.Services.Senha
{
    public interface ISenhaInterface
    {
        void CreatePasswordHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
        bool VerifyPasswordHash(string senha, byte[] senhaHash, byte[] senhaSalt);
        string CreateToken(UsuarioModel usuario);
    }
}
