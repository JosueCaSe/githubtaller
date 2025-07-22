using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface IMarcaDA
    {
        Task<IEnumerable<MarcaResponse>> Obtener();
        Task<MarcaResponse> Obtener(Guid Id);
        Task<Guid> Agregar(MarcaBase marca);
        Task<Guid> Editar(Guid Id, MarcaBase marca);
        Task<Guid> Eliminar(Guid Id);
    }
}
