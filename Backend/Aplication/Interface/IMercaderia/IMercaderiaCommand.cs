using Domain.Entities;

namespace Aplication.Interface.IMercaderia
{
    public interface IMercaderiaCommand
    {
        public Task<Mercaderia> InsertMercaderia(Mercaderia mercaderia);

        public Task<Mercaderia> RemoveMercaderia(int mercaderiaId);

        public Task<Mercaderia> UpdateMercaderia(int id, Mercaderia mercaderia);
    }
}
