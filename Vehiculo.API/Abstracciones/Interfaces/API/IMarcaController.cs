using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API
{
    public interface IMarcaController
    {
        Task<IActionResult> Obtener();
        Task<IActionResult> Obtener([FromRoute] Guid Id);
        Task<IActionResult> Agregar([FromBody] MarcaBase marca);
        Task<IActionResult> Editar([FromRoute] Guid Id, [FromBody] MarcaBase marca);
        Task<IActionResult> Eliminar([FromRoute] Guid Id);
    }
}
