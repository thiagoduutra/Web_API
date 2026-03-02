using WebAPI.Models;

namespace WebAPI.Services.Usuario
{
    public interface IUsuarioInterface
    {
        Task<ResponseModel<List<UsuarioModel>>> GetAllUsers();
        Task<ResponseModel<UsuarioModel>> GetUserById(int id);
        Task<ResponseModel<UsuarioModel>> DeleteUserById(int id);
    }
}
