using Aplication.DTO.Request;
using Aplication.DTO.Response;
using Aplication.Interface.IComanda;
using Aplication.Interface.IFormaEntrega;
using Aplication.Interface.IMercaderia;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        private readonly IComandaCommandServices _comandaCommandServices;
        private readonly IComandaQueryServices _comandaQueryServices;
        private readonly IMercaderiaQueryServices _mercaderiaQueryServices;
        private readonly IFormaEntregaQueryServices _formaEntregaQueryServices;

        public ComandaController(IComandaCommandServices commandServices, IComandaQueryServices queryServices, IMercaderiaQueryServices mercaderiaQueryServices, IFormaEntregaQueryServices formaEntregaQueryServices)
        {
            _comandaCommandServices = commandServices;
            _comandaQueryServices = queryServices;
            _mercaderiaQueryServices = mercaderiaQueryServices;
            _formaEntregaQueryServices = formaEntregaQueryServices;
        }

        /// <summary>
        /// devuelve las comandas de daterminada fecha, o en caso de no ingresar fecha, se devuelven todas las comandas
        /// </summary>

        [HttpGet]

        [ProducesResponseType(typeof(IList<ComandaResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetComandaByFecha(string? fecha)
    {
            if (DateTime.TryParse(fecha, out _) || fecha==null)
            {
                var result = await _comandaQueryServices.GetComandaList(fecha);
                return new JsonResult(result) { StatusCode = 200 };
            }
            return new JsonResult(new BadRequest { message = "Verifique los datos ingresados." }) { StatusCode = 400 };
        }

        /// <summary>
        /// crea una comanda
        /// </summary>

        [HttpPost]

        [ProducesResponseType(typeof(ComandaResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddComanda([FromBody] ComandaRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new BadRequest { message = "Compruebe los datos ingresados." }) { StatusCode = 400};
            }

            if (!await _formaEntregaQueryServices.FormaEntregaExists(request.formaEntrega))
            {
                return new JsonResult(new BadRequest { message = "No se a encontrado una FormaEntrega para el ID ingresado." }) { StatusCode = 400};
            }

            if (!await _mercaderiaQueryServices.MercaderiasExist(request.mercaderias))
            {
                return new JsonResult(new BadRequest { message = "No se a encontrado una de las Mercaderias, asegurese de ingresar el ID correctamente." }) { StatusCode = 400};
            }
            var result = await _comandaCommandServices.CreateComanda(request);

            return new JsonResult(result) { StatusCode = 201};
        }

        /// <summary>
        /// devuelve una comanda por id
        /// </summary>

        [HttpGet("{id}")]

        [ProducesResponseType(typeof(ComandaGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetComandaById(string id)
        {
            Guid idGuid;

            if (!Guid.TryParse(id, out idGuid))
            {
                return new JsonResult(new BadRequest { message = "Compruebe que el ID ingresado tenga un formato Guid." }) { StatusCode = 400};
            }

            var result = await _comandaQueryServices.GetComandaOneComandaId(idGuid);

            if (result == null)
            {
                return new JsonResult(new BadRequest { message = "No se a encontrado una Comanda para el Id ingresado." }) { StatusCode = 404};
            }

            return new JsonResult(result) { StatusCode = 200};
        }
    }
}
