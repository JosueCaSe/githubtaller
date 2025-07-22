using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class MarcaBase
    {
        [Required(ErrorMessage = "La propiedad nombre es requerida")]
        [StringLength(50, ErrorMessage = "El nombre debe de ser no menor a 3 caracteres y no mayor a 50", MinimumLength = 3)]
        public string Nombre {  get; set; }
        
    }
    public class MarcaResponse: MarcaBase
    {
        public Guid Id { get; set; }
    }

}
