using Aplication.Interface.IComanda;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Command
{
    public class ComandaCommand : IComandaCommand
    {
        private readonly AppDbContext _context;

        public ComandaCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Comanda> InsertComanda(Comanda comanda)
        {
            _context.Add(comanda);
            await _context.SaveChangesAsync();

            var comandaWithFormaEntrega = await _context.Comanda
                .Include(fe=>fe.FormaEntrega)
                .FirstOrDefaultAsync(c=>c.ComandaId==comanda.ComandaId);

            return comandaWithFormaEntrega;
        }

        public async Task<int> UpdateComandaPrecio(Guid comandaId)
        {
            var comanda = await _context.Comanda
                .Include(c => c.ComandaMercaderia)
                .ThenInclude(cm => cm.Mercaderia)
                .FirstOrDefaultAsync(c => c.ComandaId == comandaId);

            comanda.PrecioTotal = comanda.ComandaMercaderia.Sum(cm => cm.Mercaderia.Precio);

            await _context.SaveChangesAsync();

            return comanda.PrecioTotal;
        }

        public async Task<List<Comanda>> ComandaList()
        {
            var comandaList = await _context.Comanda
                .Include(c => c.FormaEntrega)
                .Include(c => c.ComandaMercaderia)
                .ThenInclude(cm => cm.Mercaderia)
                .ThenInclude(m => m.TipoMercaderia)
                .ToListAsync();

            return comandaList;
        }
    }
}
