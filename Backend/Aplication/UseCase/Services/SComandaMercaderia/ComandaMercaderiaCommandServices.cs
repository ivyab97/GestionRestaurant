using Aplication.DTO.Request;
using Aplication.DTO.Response;
using Aplication.Interface.IComandaMercaderia;
using Domain.Entities;

namespace Aplication.UseCase.Services.SComandaMercaderia
{
    public class ComandaMercaderiaCommandServices:IComandaMercaderiaCommandServices
    {
        private readonly IComandaMercaderiaCommand _command;

        public ComandaMercaderiaCommandServices(IComandaMercaderiaCommand command)
        {
            _command = command;
        }

        public async Task<IList<MercaderiaComandaResponse>> CreateComandaMercaderia(MercaderiasComandaRequest request)
        {
            var mercaderiasComandaResponse = new List<MercaderiaComandaResponse>();

            foreach (var mercaderiaId in request.mercaderias)
            {
                var comandaMercaderia = new ComandaMercaderia
                {
                    MercaderiaId = mercaderiaId,
                    ComandaId = request.comandaId
                };

                var mercaderia = await _command.InsertComandaMercaderia(comandaMercaderia);

                mercaderiasComandaResponse.Add(new MercaderiaComandaResponse
                {
                    id = mercaderiaId,
                    nombre = mercaderia.Nombre,
                    precio = mercaderia.Precio
                });
            }

            return mercaderiasComandaResponse;
        }
    }
}
