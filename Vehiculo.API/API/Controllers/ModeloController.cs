using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Flujo;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeloController : ControllerBase, IModeloController
    {
        private IModeloFlujo _modeloFlujo;
        private ILogger<ModeloController> _logger;

        public ModeloController(IModeloFlujo modeloFlujo, ILogger<ModeloController> logger)
        {
            _modeloFlujo = modeloFlujo;
            _logger = logger;
        }
        #region "Operaciones"
        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] ModeloRequest modelo)
        {
            try
            {
                var resultado = await _modeloFlujo.Agregar(modelo);
                return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Editar([FromRoute] Guid Id, [FromBody] ModeloRequest modelo)
        {
            try
            {
                if (!await VerificarModeloExiste(Id))
                    return NotFound("El modelo no existe");
                var resultado = await _modeloFlujo.Editar(Id, modelo);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{Id}")] 
        public async Task<IActionResult> Eliminar([FromRoute] Guid Id)
        {
            try
            {
                if (!await VerificarModeloExiste(Id))
                    return NotFound("El modelo no existe");
                var resultado = await _modeloFlujo.Eliminar(Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _modeloFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid Id)
        {
            var resultado = await _modeloFlujo.Obtener(Id);
            return Ok(resultado);
        }
        #endregion

        #region "Helpers"
        private async Task<bool> VerificarModeloExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoModeloExiste = await _modeloFlujo.Obtener(Id);
            if (resultadoModeloExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }
        #endregion
    }
}
