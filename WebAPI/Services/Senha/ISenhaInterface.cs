namespace WebAPI.Services.Senha
{
    public interface ISenhaInterface
    {
        void CreatePasswordHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
    }
}
