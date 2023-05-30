using Domain.Entities;

namespace Aplication.Interface.IComanda
{
    public interface IComandaCommand
    {
        public Task<Comanda> InsertComanda(Comanda comanda);

        public Task<int> UpdateComandaPrecio(Guid comandaId);

    }
}
