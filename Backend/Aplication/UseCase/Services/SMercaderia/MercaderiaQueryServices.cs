using Aplication.DTO.Response;
using Aplication.Interface.IMercaderia;

namespace Aplication.UseCase.Services.SMercaderia
{
    public class MercaderiaQueryServices:IMercaderiaQueryServices
    {
        private readonly IMercaderiaQuery _query;

        public MercaderiaQueryServices(IMercaderiaQuery query)
        {
            _query = query;
        }

        public async Task<MercaderiaResponse> GetMercaderiaById(int mercaderiaId)
        {
            var mercaderia = await _query.GetOneMercaderia(mercaderiaId);

            if (mercaderia == null)
            {
                return null;
            }

            return new MercaderiaResponse
            {
                id = mercaderia.MercaderiaId,
                nombre = mercaderia.Nombre,
                tipo = new TipoMercaderiaResponse
                {
                    id = mercaderia.TipoMercaderia.TipoMercaderiaId,
                    descripcion = mercaderia.TipoMercaderia.Descripcion
                },
                precio = mercaderia.Precio,
                ingredientes = mercaderia.Ingredientes,
                preparacion = mercaderia.Preparacion,
                imagen = mercaderia.Imagen
            };
        }


        public async Task<IList<MercaderiaResponse>> GetMercaderiaList()
        {
            var mercaderias = await _query.GetListMercaderia();
            var mercaderiasResponse = new List<MercaderiaResponse>();

            foreach (var mercaderia in mercaderias)
            {
                mercaderiasResponse.Add(new MercaderiaResponse
                {
                    id = mercaderia.MercaderiaId,
                    nombre = mercaderia.Nombre,
                    tipo = new TipoMercaderiaResponse
                    {
                        id = mercaderia.TipoMercaderia.TipoMercaderiaId,
                        descripcion = mercaderia.TipoMercaderia.Descripcion
                    },
                    precio = mercaderia.Precio,
                    ingredientes = mercaderia.Ingredientes,
                    preparacion = mercaderia.Preparacion,
                    imagen = mercaderia.Imagen
                });
            }
            return mercaderiasResponse;
        }

        public async Task<IList<MercaderiaGetResponse>> GetListMercaderiaByQuerys(string nombre, int? tipo, string orden)
        {
            var mercaderias = await _query.GetListMercaderiaByFilters(nombre, tipo, orden);
            var mercaderiasResponse = new List<MercaderiaGetResponse>();

            foreach (var mercaderia in mercaderias)
            {
                mercaderiasResponse.Add(new MercaderiaGetResponse
                {
                    id = mercaderia.MercaderiaId,
                    nombre = mercaderia.Nombre,
                    tipo = new TipoMercaderiaResponse
                    {
                        id = mercaderia.TipoMercaderia.TipoMercaderiaId,
                        descripcion = mercaderia.TipoMercaderia.Descripcion
                    },
                    precio = mercaderia.Precio,
                    imagen = mercaderia.Imagen
                });
            }
            return mercaderiasResponse;
        }

        public async Task<bool> MercaderiasExist(IList<int> mercaderias)
        {
            foreach (var item in mercaderias)
            {
                if (await _query.GetOneMercaderia(item)==null)
                {
                    return false;
                } 
            }
            return true;
        }
    }
}
