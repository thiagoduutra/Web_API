using WebAPI.Models;

namespace WebAPI.Services.Auditoria
{
    public interface IAuditoriaInterface
    {
        Task CreateAuditsAsync(string action, string userId, string dataUpdate);
        Task<List<AuditoriaModel>> GetAllAudits();
    }
}
