using Aplication.Interface.IMercaderia;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Command
{
    public class MercaderiaCommand : IMercaderiaCommand
    {
        private readonly AppDbContext _context;

        public MercaderiaCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Mercaderia> InsertMercaderia(Mercaderia mercaderia)
        {
            await _context.AddAsync(mercaderia);
            await _context.SaveChangesAsync();

            var mercaderiaWithTipoMercaderia = await _context.Mercaderia
                .Include(tm => tm.TipoMercaderia)
                .FirstOrDefaultAsync(mi => mi.MercaderiaId == mercaderia.MercaderiaId);

            return mercaderiaWithTipoMercaderia;
        }

        public async Task<Mercaderia> RemoveMercaderia(int mercaderiaId)
        {
            var mercaderia = _context.Mercaderia
                .Include(tm => tm.TipoMercaderia)
                .FirstOrDefault(mi => mi.MercaderiaId==mercaderiaId);

            if(mercaderia != null)
            {
                _context.Remove(mercaderia);
                await _context.SaveChangesAsync();
            }
            
            return mercaderia;
        }

        public async Task<Mercaderia> UpdateMercaderia(int id, Mercaderia mercaderia)
        {
           var mercaderiaToUpdate = await _context.Mercaderia
                .FirstOrDefaultAsync(m => m.MercaderiaId == id);

           if(mercaderiaToUpdate != null)
            {
                mercaderiaToUpdate.Nombre = mercaderia.Nombre;
                mercaderiaToUpdate.TipoMercaderiaId = mercaderia.TipoMercaderiaId;
                mercaderiaToUpdate.Precio = mercaderia.Precio;
                mercaderiaToUpdate.Ingredientes = mercaderia.Ingredientes;
                mercaderiaToUpdate.Preparacion = mercaderia.Preparacion;
                mercaderiaToUpdate.Imagen = mercaderia.Imagen;

                _context.Entry(mercaderiaToUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var mercaderiaUpdate = await _context.Mercaderia
                .Include(m => m.TipoMercaderia)
                .FirstOrDefaultAsync(m => m.MercaderiaId == id);

                return mercaderiaUpdate;
            }

            return mercaderiaToUpdate;
        }
    }
}
