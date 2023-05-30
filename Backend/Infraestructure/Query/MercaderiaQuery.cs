using Aplication.Interface.IMercaderia;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Query
{
    public class MercaderiaQuery : IMercaderiaQuery
    {
        private readonly AppDbContext _context;

        public MercaderiaQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Mercaderia>> GetListMercaderia()
        {
            var mercaderias = await _context.Mercaderia.ToListAsync();
            return mercaderias;
        }

        public async Task<IList<Mercaderia>> GetListMercaderiaByFilters(string? nombre, int? tipoId, string orden)
        {
            var mercaderias = _context.Mercaderia
                .Include(tm => tm.TipoMercaderia)
                .AsQueryable();

            if (!string.IsNullOrEmpty(nombre))
            {
                mercaderias =  mercaderias.Where(m => m.Nombre.Contains(nombre));
            }

            if (tipoId.HasValue)
            {
                mercaderias = mercaderias.Where(m => m.TipoMercaderiaId == tipoId);
            }

            switch (orden.ToUpper())
            {
                case "ASC":
                    mercaderias = mercaderias.OrderBy(m => m.Precio);
                    break;
                case "DESC":
                    mercaderias = mercaderias.OrderByDescending(m => m.Precio);
                    break;
                default:
                    break;
            }

            return await mercaderias.ToListAsync();
        }

        public async Task<Mercaderia> GetOneMercaderia(int mercaderiaId)
        {
            var mercaderia = await _context.Mercaderia
                .Include(tm => tm.TipoMercaderia)
                .FirstOrDefaultAsync(mi => mi.MercaderiaId == mercaderiaId);

            return mercaderia;
        }

    }
}
