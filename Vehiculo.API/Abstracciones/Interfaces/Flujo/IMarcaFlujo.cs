using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IMarcaFlujo
    {
        Task<IEnumerable<MarcaResponse>> Obtener();
        Task<MarcaResponse> Obtener(Guid Id);
        Task<Guid> Agregar(MarcaBase marca);
        Task<Guid> Editar(Guid Id, MarcaBase marca);
        Task<Guid> Eliminar(Guid Id);
    }
}
