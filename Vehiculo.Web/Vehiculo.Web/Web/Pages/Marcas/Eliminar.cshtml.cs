using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Marcas
{
    public class EliminarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        public MarcaResponse marca { get; set; } = default!;
        public EliminarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }


        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id == Guid.Empty)
                return NotFound();
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerMarca");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                marca = JsonSerializer.Deserialize<MarcaResponse>(resultado, opciones);
            }
            return Page();
        }
        public async Task<ActionResult> OnPost(Guid? id)
        {
            if (id == Guid.Empty)
                return NotFound();
            if (!ModelState.IsValid)
                return Page();
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarMarca");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Delete, string.Format(endpoint, id));
            
            var respuesta = await cliente.SendAsync(solicitud);
            if (!respuesta.IsSuccessStatusCode)
            {
                var mensajeError = await respuesta.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, mensajeError);
                return await OnGet(id);
            }
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }

    }
}
