using AutoMapper;
using WebAPI.Dto.Usuario;
using WebAPI.Models;

namespace WebAPI.Profiles
{
    public class ProfilesAutoMapper : Profile
    {
        public ProfilesAutoMapper() {
            CreateMap<UsuarioCriarDto, UsuarioModel>().ReverseMap();
        }
    }
}
