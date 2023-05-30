using Aplication.DTO.Request;
using Aplication.DTO.Response;
using Aplication.Interface.IComanda;
using Aplication.Interface.IComandaMercaderia;
using Domain.Entities;

namespace Aplication.UseCase.Services.SComanda
{
    public class ComandaCommandServices : IComandaCommandServices
    {
        private readonly IComandaCommand _commandComanda;
        private readonly IComandaMercaderiaCommandServices _commandServicesComandaMercaderia;

        public ComandaCommandServices(IComandaCommand commandComanda, IComandaMercaderiaCommandServices commandComandaMercaderia)
        {
            _commandComanda = commandComanda;
            _commandServicesComandaMercaderia = commandComandaMercaderia;
        }

        public async Task<ComandaResponse> CreateComanda(ComandaRequest request)
        {
            var comanda = new Comanda
            {
                ComandaId = new Guid(),
                FormaEntregaId = request.formaEntrega,
                Fecha = DateTime.Now
            };

            comanda = await _commandComanda.InsertComanda(comanda);

            var mercaderiasComandaRequest = new MercaderiasComandaRequest
            {
                comandaId = comanda.ComandaId,
                mercaderias = request.mercaderias
            };

            var mercaderiasComandaResponse = await _commandServicesComandaMercaderia.CreateComandaMercaderia(mercaderiasComandaRequest);

            return new ComandaResponse
            {
                id = comanda.ComandaId,
                mercaderias = mercaderiasComandaResponse,
                formaEntrega = new FormaEntregaResponse
                {
                    id = comanda.FormaEntrega.FormaEntregaId,
                    descripcion = comanda.FormaEntrega.Descripcion
                },
                total = await this.UpdateComandaPrecio(comanda.ComandaId),
                fecha = comanda.Fecha
            };


        }

        public async Task<int> UpdateComandaPrecio(Guid comandaId)
        {
           return await _commandComanda.UpdateComandaPrecio(comandaId);
        }
    }
}
