using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class MarcaDA : IMarcaDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlconnection;

        public MarcaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlconnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region Operaciones

        public async Task<Guid> Agregar(MarcaBase marca)
        {
            await VerificarNombreDuplicado(marca.Nombre);
            string query = @"AgregarMarca";
            var resultadoConsulta = await _sqlconnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Guid.NewGuid(),
                Nombre = marca.Nombre
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid Id, MarcaBase marca)
        {
            await VerificarMarcaExiste(Id);
            await VerificarNombreDuplicado(marca.Nombre);
            string query = @"EditarMarca";
            var resultadoConsulta = await _sqlconnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id,
                Nombre = marca.Nombre
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await VerificarMarcaExiste(Id);
            await VerificarModelosAsociados(Id);
            string query = @"EliminarMarca";
            var resultadoConsulta = await _sqlconnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<MarcaResponse>> Obtener()
        {
            string query = @"ObtenerMarcas";
            var resultadoConsulta = await _sqlconnection.QueryAsync<MarcaResponse>(query);
            return resultadoConsulta;
        }

       public async Task<MarcaResponse> Obtener(Guid Id)
        {
            string query = @"ObtenerMarca";
            var resultadoConsulta = await _sqlconnection.QueryAsync<MarcaResponse>(query, new
            {
                Id = Id
            });
            return resultadoConsulta.FirstOrDefault();
        }
        #endregion

        #region Helpers

        private async Task VerificarMarcaExiste(Guid Id)
        {
            MarcaResponse? resultadoConsultaMarca = await Obtener(Id);
            if (resultadoConsultaMarca == null)
                throw new Exception("No se encontró la Marca");
        }

        private async Task VerificarNombreDuplicado(string nombre)
        {
            string query = @"ObtenerMarcas";
            var marcas = await _sqlconnection.QueryAsync<MarcaResponse>(query);

            bool existe = marcas.Any(m => m.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase)
            );

            if (existe)
            {
                throw new Exception("Ya existe una marca con este nombre");
            }
        }

        private async Task VerificarModelosAsociados(Guid IdMarca)
        {
            string query = @"SELECT COUNT(1) FROM Modelos WHERE IdMarca = @IdMarca";
            var cantidadModelos = await _sqlconnection.ExecuteScalarAsync<int>(query, new { IdMarca = IdMarca });

            if (cantidadModelos > 0)
            {
                throw new Exception("No se puede eliminar la marca porque tiene modelos asociados");
            }
        }
        #endregion
    }
}
