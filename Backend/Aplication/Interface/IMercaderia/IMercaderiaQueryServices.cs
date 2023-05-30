using Aplication.DTO.Response;

namespace Aplication.Interface.IMercaderia
{
    public interface IMercaderiaQueryServices
    {
        Task<IList<MercaderiaResponse>> GetMercaderiaList();
        Task<IList<MercaderiaGetResponse>> GetListMercaderiaByQuerys(string nombre, int? tipo, string orden);

        Task<bool> MercaderiasExist(IList<int> mercaderias);
        Task<MercaderiaResponse> GetMercaderiaById(int mercaderiaId);

    }
}
