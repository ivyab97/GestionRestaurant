using Aplication.DTO.Request;
using Aplication.DTO.Response;

namespace Aplication.Interface.IComandaMercaderia
{
    public interface IComandaMercaderiaCommandServices
    {
        Task<IList<MercaderiaComandaResponse>> CreateComandaMercaderia(MercaderiasComandaRequest request);
    }
}
