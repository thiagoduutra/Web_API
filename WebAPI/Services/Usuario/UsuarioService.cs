using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Dto.Login;
using WebAPI.Dto.Usuario;
using WebAPI.Models;
using WebAPI.Services.Senha;

namespace WebAPI.Services.Usuario
{
    public class UsuarioService : IUsuarioInterface
    {

        private readonly AppDbContext _context;
        private readonly ISenhaInterface _senhaInterface;
        private readonly IMapper _mapper;

        public UsuarioService(AppDbContext context, ISenhaInterface senhaInterface, IMapper mapper)
        {
            _context = context;
            _senhaInterface = senhaInterface;
            _mapper = mapper;
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

                if (user == null)
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
        public async Task<ResponseModel<UsuarioModel>> CreateUser(UsuarioCriarDto usuarioCriarDto)
        {
            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();
            try
            {
                if (!VerifyUserEmailExists(usuarioCriarDto))
                {
                    response.Message = "Email/Usuário já cadastrado!";
                }

                _senhaInterface.CreatePasswordHash(usuarioCriarDto.Password, out byte[] senhaHash, out byte[] senhaSalt);

                UsuarioModel user = _mapper.Map<UsuarioModel>(usuarioCriarDto);

                user.PasswordHash = senhaHash;
                user.PasswordSalt = senhaSalt;
                user.DataCreate = DateTime.Now;
                user.DataUpdate = DateTime.Now;


                _context.Add(user);
                await _context.SaveChangesAsync();

                response.Message = $"Usuário {user.Name} cadastrado com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<UsuarioModel>> UpdateUser(UsuarioEditarDto usuarioEditarDto)
        {
            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();

            try
            {
                var user = await _context.Users.FindAsync(usuarioEditarDto.Id);
                if (user == null)
                {
                    response.Message = "Usuário não localizado!";
                    return response;
                }

                user.Name = usuarioEditarDto.Name;
                user.LastName = usuarioEditarDto.LastName;
                user.Email = usuarioEditarDto.Email;
                user.User = usuarioEditarDto.User;
                user.DataUpdate = DateTime.Now;

                _context.Update(user);
                await _context.SaveChangesAsync();

                response.Message = $"Usuário {user.Name} editado com sucesso!";
                response.Data = user;

                return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<UsuarioModel>> Login(UsuarioLoginDto usuarioLoginDto)
        {
            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(d => d.Email == usuarioLoginDto.Email);

                if (user == null)
                {
                    response.Message = "Credenciais inválidas!";
                    return response;
                }

                if (!_senhaInterface.VerifyPasswordHash(usuarioLoginDto.Password, user.PasswordHash, user.PasswordSalt))
                {
                    response.Message = "Credenciais inválidas!";
                    return response;
                }
                var token = _senhaInterface.CreateToken(user);
                user.Token = token;

                _context.Update(user);
                await _context.SaveChangesAsync();

                response.Data = user;
                response.Message = "Usuário logado com sucesso!";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
        private bool VerifyUserEmailExists(UsuarioCriarDto usuarioCriacaoDto)
        {
            var user = _context.Users.FirstOrDefault(d => d.Email == usuarioCriacaoDto.Email || d.User == usuarioCriacaoDto.User);

            if (user != null)
            {
                return false;
            }
            return true;
        }


    }
}
