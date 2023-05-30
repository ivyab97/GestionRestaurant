using Domain.Entities;

namespace Aplication.Interface.IMercaderia
{
    public interface IMercaderiaQuery
    {
        Task<Mercaderia> GetOneMercaderia(int mercaderiaId);
        Task<IList<Mercaderia>> GetListMercaderia();
        Task<IList<Mercaderia>> GetListMercaderiaByFilters(string nombre, int? tipoId, string orden);
    }
}
