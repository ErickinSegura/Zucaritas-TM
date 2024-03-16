using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Threading.Tasks;

namespace AWAQPagina.Pages
{
    public class studentViewModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public studentViewModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string nombre { get; private set; }
        public string apellido { get; private set; }
        public string nombreUsuario { get; private set; }

        public void OnGet()
        {
             nombre = _httpContextAccessor.HttpContext.Request.Cookies["Nombre"];
             apellido = _httpContextAccessor.HttpContext.Request.Cookies["APELLIDO_PATERNO"];
            nombreUsuario = _httpContextAccessor.HttpContext.Request.Cookies["Usuario"];

        }


    }
}
