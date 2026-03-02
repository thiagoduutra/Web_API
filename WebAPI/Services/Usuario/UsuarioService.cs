using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Services.Usuario
{
    public class UsuarioService : IUsuarioInterface
    {

        private readonly AppDbContext _context;
        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<UsuarioModel>>> GetAllUsers()
        {
            ResponseModel<List<UsuarioModel>> response = new ResponseModel<List<UsuarioModel>>();

            try
            {
                var users = await _context.Users.ToListAsync();

                if (users.Count() == 0)
                {
                    response.Message = "Nenhum Usuário Cadastrado!";
                    return response;
                }

                response.Data = users;
                response.Message = "Usuários Localizados com sucesso!";

                return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<UsuarioModel>> GetUserById(int id)
        {
            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();

            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    response.Message = "Usuário não localizado!";
                    return response;
                }

                response.Data = user;
                response.Message = "Usuário localizado!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<UsuarioModel>> DeleteUserById(int id)
        {
            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();
            try
            {
                var user = await _context.Users.FindAsync(id);

                if(user == null)
                {
                    response.Message = "Usuário não localizado!";
                    return response;
                }

                _context.Remove(user);
                await _context.SaveChangesAsync();

                response.Message = $"Usuário {user.Name} removido com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
    }
}
