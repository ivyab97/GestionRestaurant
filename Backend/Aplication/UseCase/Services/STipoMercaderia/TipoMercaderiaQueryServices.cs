using Aplication.DTO;
using Aplication.Interface.ITipoMercaderia;

namespace Aplication.UseCase.Services.STipoMercaderia
{
    public class TipoMercaderiaQueryServices:ITipoMercaderiaQueryServices
    {
        private readonly ITipoMercaderiaQuery _query;

        public TipoMercaderiaQueryServices(ITipoMercaderiaQuery query)
        {
            _query = query;
        }

        public Task<bool> TipoMercaderiaExists(int tipoMercaderiaId)
        {
            return _query.TipoMercaderiaIdExists(tipoMercaderiaId);
        }
    }
}
