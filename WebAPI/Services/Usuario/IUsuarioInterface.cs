using WebAPI.Dto.Login;
using WebAPI.Dto.Usuario;
using WebAPI.Models;

namespace WebAPI.Services.Usuario
{
    public interface IUsuarioInterface
    {
        Task<ResponseModel<List<UsuarioModel>>> GetAllUsers();
        Task<ResponseModel<UsuarioModel>> GetUserById(int id);
        Task<ResponseModel<UsuarioModel>> DeleteUserById(int id);
        Task<ResponseModel<UsuarioModel>> CreateUser(UsuarioCriarDto usuarioCriarDto);
        Task<ResponseModel<UsuarioModel>> UpdateUser(UsuarioEditarDto usuarioEditarDto);
        Task<ResponseModel<UsuarioModel>> Login(UsuarioLoginDto usuarioLoginDto);
    }
}
