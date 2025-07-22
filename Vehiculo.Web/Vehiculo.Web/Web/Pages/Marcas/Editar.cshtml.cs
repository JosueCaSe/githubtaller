using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Marcas
{
    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        [BindProperty]
        public MarcaResponse marca { get; set; } = default!;
        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }


        public async Task OnGet(Guid? id)
        {
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
        }
        public async Task<ActionResult> OnPost(Guid? id)
        {
            if (!ModelState.IsValid)
                return Page();
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarMarca");
            var cliente = new HttpClient();

            var respuesta = await cliente.PutAsJsonAsync<MarcaResponse>(string.Format(endpoint, marca.Id), marca);
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }

    }
}
