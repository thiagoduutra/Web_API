using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Services.Auditoria
{
    public class AuditoriaService : IAuditoriaInterface
    {
        private readonly AppDbContext _context;

        public AuditoriaService(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAuditsAsync(string action, string userId, string dataUpdate)
        {
            var auditoria = new AuditoriaModel
            {
                Action = action,
                UserId = userId,
                DataUpdate = dataUpdate
            };

            _context.Audits.Add(auditoria);
            await _context.SaveChangesAsync();
           
        }

        public async Task<List<AuditoriaModel>> GetAllAudits()
        {
            var auditorias = await _context.Audits.OrderByDescending(a=>a.Data).ToListAsync();
            return auditorias;
        }
    }
}
