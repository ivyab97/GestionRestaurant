using Domain.Entities;

namespace Aplication.Interface.IComandaMercaderia
{
    public interface IComandaMercaderiaCommand
    {
        public Task<Mercaderia> InsertComandaMercaderia(ComandaMercaderia comandaMercaderia);
    }
}
