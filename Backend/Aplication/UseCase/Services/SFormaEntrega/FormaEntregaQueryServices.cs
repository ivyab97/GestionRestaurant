using Aplication.Interface.IFormaEntrega;

namespace Aplication.UseCase.Services.SFormaEntrega
{
    public class FormaEntregaQueryServices:IFormaEntregaQueryServices
    {
        private readonly IFormaEntregaQuery _query;

        public FormaEntregaQueryServices(IFormaEntregaQuery query)
        {
            _query = query;
        }

        public Task<bool> FormaEntregaExists(int formaEntregaId)
        {
            return _query.FormaEntregaIdExists(formaEntregaId);
        }
    }
}
