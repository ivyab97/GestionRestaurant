using Aplication.DTO.Request;
using Application.DTO.Response;

namespace Aplication.Interface.IMercaderia
{
    public interface IMercaderiaCommandServices
    {
        public Task<ResponseMessage> CreateMercaderia(MercaderiaRequest mercaderiaRequest);

        public Task<ResponseMessage> DeleteMercaderia(int mercaderiaId);

        public Task<ResponseMessage> ModifyMercaderia(int mercaderiaId, MercaderiaRequest request);
    }
}
