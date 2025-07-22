using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IModeloFlujo
    {
        Task<IEnumerable<ModeloResponse>> Obtener();
        Task<ModeloResponse> Obtener(Guid Id);
        Task<Guid> Agregar(ModeloRequest modelo);
        Task<Guid> Editar(Guid Id, ModeloRequest modelo);
        Task<Guid> Eliminar(Guid Id);
    }
}
