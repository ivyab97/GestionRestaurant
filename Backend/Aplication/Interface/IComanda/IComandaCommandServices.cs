using Aplication.DTO.Request;
using Aplication.DTO.Response;

namespace Aplication.Interface.IComanda
{
    public interface IComandaCommandServices
    {
        Task<ComandaResponse> CreateComanda(ComandaRequest request);
        Task<int> UpdateComandaPrecio(Guid comandaId);

    }
}
