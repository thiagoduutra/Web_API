using System.Security.Cryptography;

namespace WebAPI.Services.Senha
{
    public class SenhaService : ISenhaInterface
    {
        public void CreatePasswordHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }
        }
    }
}
