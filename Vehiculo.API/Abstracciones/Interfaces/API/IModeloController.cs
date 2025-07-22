using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API
{
    public interface IModeloController
    {
        Task<IActionResult> Obtener();
        Task<IActionResult> Obtener([FromRoute] Guid Id);
        Task<IActionResult> Agregar([FromBody] ModeloRequest modelo);
        Task<IActionResult> Editar([FromRoute] Guid Id, [FromBody] ModeloRequest modelo);
        Task<IActionResult> Eliminar([FromRoute] Guid Id);
    }
}
