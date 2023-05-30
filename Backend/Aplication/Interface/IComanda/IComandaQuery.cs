using Domain.Entities;

namespace Aplication.Interface.IComanda
{
    public interface IComandaQuery
    {
        Task<IList<Comanda>> GetListComanda(string? fecha);
        Task<Comanda> GetComanda(Guid comandaId);
    }
}
