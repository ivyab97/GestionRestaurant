using Aplication.Interface.IFormaEntrega;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Query
{
    public class FormaEntregaQuery : IFormaEntregaQuery
    {
        private readonly AppDbContext _context;

        public FormaEntregaQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> FormaEntregaIdExists(int formaEntregaId)
        {
            return await _context.FormaEntrega.AnyAsync(c => c.FormaEntregaId == formaEntregaId);
        }
    }
}
