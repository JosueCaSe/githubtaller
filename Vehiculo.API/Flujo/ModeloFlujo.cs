using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class ModeloFlujo : IModeloFlujo
    {
        private IModeloDA _modeloDA;

        public ModeloFlujo(IModeloDA modeloDA)
        {
            _modeloDA = modeloDA;
        }

        public Task<Guid> Agregar(ModeloRequest modelo)
        {
            return _modeloDA.Agregar(modelo);
        }

        public Task<Guid> Editar(Guid Id, ModeloRequest modelo)
        {
            return _modeloDA.Editar(Id, modelo);
        }

        public Task<Guid> Eliminar(Guid Id)
        {
            return _modeloDA.Eliminar(Id);
        }

        public Task<IEnumerable<ModeloResponse>> Obtener()
        {
            return _modeloDA.Obtener();
        }

        public Task<ModeloResponse> Obtener(Guid Id)
        {
            return _modeloDA.Obtener(Id);
        }
    }
}
