using Aplication.DTO.Response;

namespace Aplication.Interface.IComanda
{
    public interface IComandaQueryServices
    {
        Task<IList<ComandaResponse>> GetComandaList(string? fecha);
        Task<ComandaGetResponse> GetComandaOneComandaId(Guid comandaId);
    }
}
