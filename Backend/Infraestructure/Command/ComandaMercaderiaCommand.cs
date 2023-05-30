using Aplication.Interface.IComandaMercaderia;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Command
{
    public class ComandaMercaderiaCommand : IComandaMercaderiaCommand
    {
        private readonly AppDbContext _context;

        public ComandaMercaderiaCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Mercaderia> InsertComandaMercaderia(ComandaMercaderia comandamercaderia)
        {
            _context.Add(comandamercaderia);
            await _context.SaveChangesAsync();

            var mercaderia = await _context.Mercaderia
                .FirstOrDefaultAsync(mi => mi.MercaderiaId == comandamercaderia.MercaderiaId);

            return mercaderia;
        }
    }
}
