using Aplication.DTO.Response;
using Aplication.Interface.IComanda;

namespace Aplication.UseCase.Services.SComanda
{
    public class ComandaQueryServices:IComandaQueryServices
    {
        private readonly IComandaQuery _query;

        public ComandaQueryServices(IComandaQuery query)
        { 
            _query = query;
        }

        public async Task<ComandaGetResponse> GetComandaOneComandaId(Guid comandaId)
        {
            var comanda = await _query.GetComanda(comandaId);

            if (comanda == null)
            {
                return null;
            }

            var comandaGetResponse = new ComandaGetResponse
            {
                id = comanda.ComandaId,
                mercaderias = new List<MercaderiaGetResponse>(),
                formaEntrega = new FormaEntregaResponse
                {
                    id = comanda.FormaEntrega.FormaEntregaId,
                    descripcion = comanda.FormaEntrega.Descripcion
                },
                total = comanda.PrecioTotal,
                fecha = comanda.Fecha
            };

            foreach (var comandaMercaderia in comanda.ComandaMercaderia)
            {
                comandaGetResponse.mercaderias.Add(new MercaderiaGetResponse
                {
                    id = comandaMercaderia.Mercaderia.MercaderiaId,
                    nombre = comandaMercaderia.Mercaderia.Nombre,
                    precio = comandaMercaderia.Mercaderia.Precio,
                    tipo = new TipoMercaderiaResponse
                    {
                        id = comandaMercaderia.Mercaderia.TipoMercaderia.TipoMercaderiaId,
                        descripcion = comandaMercaderia.Mercaderia.TipoMercaderia.Descripcion
                    },
                    imagen = comandaMercaderia.Mercaderia.Imagen
                });
            }

            return comandaGetResponse;   
        }

        public async Task<IList<ComandaResponse>> GetComandaList(string? fecha)
        {
            var comandaList = await _query.GetListComanda(fecha);
            var comandaResponseList = new List<ComandaResponse>();

            foreach (var comanda in comandaList)
            {
                var comandaResponse = new ComandaResponse
                {
                    id = comanda.ComandaId,
                    mercaderias = new List<MercaderiaComandaResponse>(),
                    formaEntrega = new FormaEntregaResponse
                    {
                        id = comanda.FormaEntrega.FormaEntregaId,
                        descripcion = comanda.FormaEntrega.Descripcion
                    },
                    total = comanda.PrecioTotal,
                    fecha = comanda.Fecha
                };

                foreach (var comandaMercaderia in comanda.ComandaMercaderia)
                {
                    comandaResponse.mercaderias.Add(new MercaderiaComandaResponse
                    {
                        id = comandaMercaderia.MercaderiaId,
                        nombre = comandaMercaderia.Mercaderia.Nombre,
                        precio = comandaMercaderia.Mercaderia.Precio
                    });
                }
                comandaResponseList.Add(comandaResponse);
            }

            return comandaResponseList;
        }
    }
}
