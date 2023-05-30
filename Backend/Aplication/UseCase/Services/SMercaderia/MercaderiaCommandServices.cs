using Aplication.DTO.Request;
using Aplication.DTO.Response;
using Aplication.Interface.IMercaderia;
using Application.DTO.Response;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Aplication.UseCase.Services.SMercaderia
{
    public class MercaderiaCommandServices : IMercaderiaCommandServices
    {
        private readonly IMercaderiaCommand _command;

        public MercaderiaCommandServices(IMercaderiaCommand command)
        {
            _command = command;
        }

        public async Task<ResponseMessage> CreateMercaderia(MercaderiaRequest mercaderiaRequest)
        {
            var mercaderia = new Mercaderia
            {
                Nombre = mercaderiaRequest.nombre,
                TipoMercaderiaId = mercaderiaRequest.tipo,
                Precio = (int)mercaderiaRequest.precio,
                Ingredientes = mercaderiaRequest.ingredientes,
                Preparacion = mercaderiaRequest.preparacion,
                Imagen = mercaderiaRequest.imagen
            };

            try
            {
                mercaderia = await _command.InsertMercaderia(mercaderia);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException)
                {
                    if (sqlException.Number == 547) // / Se comprueba si hay una violación de clave externa
                    {
                        return new ResponseMessage(400, new BadRequest {message = "Ingrese un ID para un tipo de mercaderia existente." });
                    }
                }
                return new ResponseMessage(409, new BadRequest { message = "Ingrese un nombre que no exista actualmente." });
            }

            var response = new MercaderiaResponse
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

            return new ResponseMessage(201, response);
        }

        public async Task<ResponseMessage> DeleteMercaderia(int mercaderiaId)
        {

            try
            {
                var mercaderia = await _command.RemoveMercaderia(mercaderiaId);
                if (mercaderia == null)
                {
                    return new ResponseMessage(400, new BadRequest{message = "Ingrese el ID de una mercaderia existente."});
                }

                return new ResponseMessage(200, new MercaderiaResponse
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
            catch (DbUpdateException ex)
            {
                return new ResponseMessage(409, new BadRequest { message = "La meraderia no puede ser eliminada; existe una comanda que depende de esta." });
            }
        }

        public async Task<ResponseMessage> ModifyMercaderia(int mercaderiaId, MercaderiaRequest mercaderiaRequest)
        {
            var mercaderia = new Mercaderia
            {
                Nombre = mercaderiaRequest.nombre,
                TipoMercaderiaId = mercaderiaRequest.tipo,
                Precio = (int)mercaderiaRequest.precio,
                Ingredientes = mercaderiaRequest.ingredientes,
                Preparacion = mercaderiaRequest.preparacion,
                Imagen = mercaderiaRequest.imagen
            };

            try
            {
                mercaderia = await _command.UpdateMercaderia(mercaderiaId, mercaderia);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException)
                {
                    if (sqlException.Number == 547) // Se comprueba si hay una violación de clave externa
                    {
                        return new ResponseMessage(400, new BadRequest { message = "Ingrese un ID para un tipo de mercaderia existente." });
                    }
                }
                return new ResponseMessage(409, new BadRequest { message = "Ingrese un nombre que no exista actualmente." });
            }

            if (mercaderia==null)
            {
                return new ResponseMessage(404, new BadRequest { message = "Mercaderia no encontrada." });
            }

            return new ResponseMessage(200, new MercaderiaResponse
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
    }
}
