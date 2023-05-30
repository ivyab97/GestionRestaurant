using Aplication.Interface.ITipoMercaderia;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Query
{
    public class TipoMercaderiaQuery : ITipoMercaderiaQuery
    {
        private readonly AppDbContext _context;

        public TipoMercaderiaQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> TipoMercaderiaIdExists(int tipoMercaderiaId)
        {
            return await _context.TipoMercaderia.AnyAsync(tm => tm.TipoMercaderiaId == tipoMercaderiaId);
        }
    }
}
