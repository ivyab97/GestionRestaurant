using Aplication.DTO.Request;
using Aplication.DTO.Response;
using Aplication.Interface.IMercaderia;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MercaderiaController : ControllerBase
    {
        private readonly IMercaderiaCommandServices _mercaderiaCommandServices;
        private readonly IMercaderiaQueryServices _mercaderiaQueryServices;

        public MercaderiaController(IMercaderiaCommandServices commandServices, IMercaderiaQueryServices queryServices)
        {
            _mercaderiaCommandServices = commandServices;
            _mercaderiaQueryServices = queryServices;
        }

        /// <summary>
        /// devuelve mercaderias aplicando filtros opcionales, como tipo, nombre o orden (ASC o DESC)
        /// </summary>

        [HttpGet]

        [ProducesResponseType(typeof(IList<MercaderiaGetResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IList<MercaderiaGetResponse>>> GetMercaderias(int? tipo, string? nombre, string orden = "ASC")
        {
            if (tipo != null && tipo is string)
            {
                return new JsonResult(new BadRequest { message = "El campo <tipo> solo puede recibir int o null." }) { StatusCode = 400 };
            }

            if (orden.ToUpper() != "DESC" && orden.ToUpper() != "ASC")
            {
                return new JsonResult(new BadRequest { message = "El valor de <orden> no es válido." }) { StatusCode = 400 };

            }
            var mercaderias = await _mercaderiaQueryServices.GetListMercaderiaByQuerys(nombre, tipo, orden);

            return new JsonResult(mercaderias) { StatusCode = 200 };
        }

        /// <summary>
        /// crea una mercaderia nueva
        /// </summary>

        [HttpPost]

        [ProducesResponseType(typeof(MercaderiaResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddMercaderia([FromBody] MercaderiaRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new BadRequest { message = "Compruebe los datos ingresados." }) { StatusCode = 400};
            }
            var result = await _mercaderiaCommandServices.CreateMercaderia(request);

            return StatusCode(result.code, result.result);
        }

        /// <summary>
        /// devuelve una mercaderia por id
        /// </summary>

        [HttpGet("{id}")]

        [ProducesResponseType(typeof(MercaderiaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMercaderia(int id)
        {  
            if (!ModelState.IsValid)
            {
                return new JsonResult(new BadRequest { message = "Ingrese un numero entero para el ID." }) { StatusCode = 400 };
            }
            var mercaderia = await _mercaderiaQueryServices.GetMercaderiaById(id);

            if (mercaderia == null)
            {
                return new JsonResult(new BadRequest { message = "Mercaderia no encontrada." }) { StatusCode = 404};
            }
            return new JsonResult(mercaderia) { StatusCode = 200};
        }

        /// <summary>
        /// modifica los datos de una mercaderia por id
        /// </summary>

        [HttpPut("{id}")]

        [ProducesResponseType(typeof(MercaderiaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateMercaderia(int id, [FromBody] MercaderiaRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new BadRequest { message = "Compruebe los datos ingresados." }) { StatusCode = 400};
            }
            var response = await _mercaderiaCommandServices.ModifyMercaderia(id, request);

            return StatusCode(response.code, response.result);
        }

        /// <summary>
        /// elimina una mercaderia por id
        /// </summary>

        [HttpDelete("{id}")]

        [ProducesResponseType(typeof(MercaderiaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteOferta(int id)
        {
            if (!(ModelState.IsValid))
            {
                return new JsonResult(new BadRequest { message = "Compruebe los datos ingresados." }) { StatusCode = 400};
            }
            var result = await _mercaderiaCommandServices.DeleteMercaderia(id);

            return StatusCode(result.code, result.result);
        }
    }
}
