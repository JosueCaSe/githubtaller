using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class ModeloBase
    {
        [Required(ErrorMessage = "La propiedad Nombre es requerida")]
        [StringLength(50, ErrorMessage = "El nombre debe de ser no menor a 3 caracteres y no mayor a 50", MinimumLength = 3)]
        public string Nombre {  get; set; }
        
    }
    public class ModeloRequest : ModeloBase
    {
        [Required(ErrorMessage = "La propiedad IdMarca es requerido")]
        public Guid IdMarca { get; set; }
    }
    public class ModeloResponse: ModeloBase
    {
        public Guid Id { get; set; }
        public string Marca { get; set; }
    }

}
