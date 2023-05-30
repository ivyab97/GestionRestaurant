using Aplication.Interface.IComanda;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Query
{
    public class ComandaQuery : IComandaQuery
    {
        private readonly AppDbContext _context;

        public ComandaQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Comanda> GetComanda(Guid comandaId)
        {
            var comanda = await _context.Comanda
                .Include(c => c.FormaEntrega)
                .Include(c => c.ComandaMercaderia)
                .ThenInclude(cm => cm.Mercaderia)
                .ThenInclude(m => m.TipoMercaderia)
                .FirstOrDefaultAsync(c => c.ComandaId.ToString() == comandaId.ToString());

            return comanda;
        }

        public async Task<IList<Comanda>> GetListComanda(string? fecha)
        {
            IQueryable<Comanda> comandasQuery = _context.Comanda
                .Include(c => c.FormaEntrega)
                .Include(c => c.ComandaMercaderia)
                .ThenInclude(cm => cm.Mercaderia)
                .ThenInclude(m => m.TipoMercaderia);

            if (fecha!=null && DateTime.TryParse(fecha, out DateTime fechaConvertida))
            {
                comandasQuery = comandasQuery.Where(f => f.Fecha.Date == fechaConvertida.Date);
            }

            var comandas = await comandasQuery.ToListAsync();
            return comandas;
        }
    }
}
