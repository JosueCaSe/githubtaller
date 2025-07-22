using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DA
{
    public class ModeloDA : IModeloDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlconnection;

        public ModeloDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlconnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region Operaciones

        public async Task<Guid> Agregar(ModeloRequest modelo)
        {
            await VerificarNombreDuplicado(modelo.Nombre);
            string query = @"AgregarModelo";
            var resultadoConsulta = await _sqlconnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Guid.NewGuid(),
                Nombre = modelo.Nombre,
                IdMarca = modelo.IdMarca
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid Id, ModeloRequest modelo)
        {
            await VerificarModeloExiste(Id);
            await VerificarNombreDuplicado(modelo.Nombre);
            string query = @"EditarModelo";
            var resultadoConsulta = await _sqlconnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id,
                Nombre = modelo.Nombre,
                IdMarca = modelo.IdMarca
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await VerificarModeloExiste(Id);
            await MarcaAsociadaValida(Id); 
            string query = @"EliminarModelo";
            var resultadoConsulta = await _sqlconnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<ModeloResponse>> Obtener()
        {
            string query = @"ObtenerModelos";
            var resultadoConsulta = await _sqlconnection.QueryAsync<ModeloResponse>(query);
            return resultadoConsulta;
        }

       public async Task<ModeloResponse> Obtener(Guid Id)
        {
            string query = @"ObtenerModelo";
            var resultadoConsulta = await _sqlconnection.QueryAsync<ModeloResponse>(query, new
            {
                Id = Id
            });
            return resultadoConsulta.FirstOrDefault();
        }
        #endregion

        #region Helpers

        private async Task VerificarModeloExiste(Guid Id)
        {
            ModeloResponse? resultadoConsultaModelo = await Obtener(Id);
            if (resultadoConsultaModelo == null)
                throw new Exception("No se encontró el Modelo");
        }
        private async Task VerificarNombreDuplicado(string nombre)
        {
            string query = @"ObtenerModelos";
            var modelos = await _sqlconnection.QueryAsync<ModeloResponse>(query);

            bool existe = modelos.Any(m => m.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase)
            );

            if (existe)
            {
                throw new Exception("Ya existe una modelo con este nombre");
            }
        }
        private async Task MarcaAsociadaValida (Guid IdModelo)
        {
            var modelo = await Obtener(IdModelo);
            if (string.IsNullOrEmpty(modelo.Marca)) 
                throw new Exception("El modelo no tiene una marca asociada válida");

            string queryVerificarMarca = @"SELECT COUNT(1) FROM Marcas WHERE Nombre = @Marca";
            var marcaExiste = await _sqlconnection.ExecuteScalarAsync<int>(queryVerificarMarca, new { Marca = modelo.Marca }
            );

            if (marcaExiste == 0)
                throw new Exception($"La marca '{modelo.Marca}' no existe en la base de datos");
        }
        #endregion
    }
}
