using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace AWAQPagina.Pages
{
    public class studentViewModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Usuario usuario { get; set; }
        public string dashboardLink { get; set; }

        public studentViewModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            usuario = new Usuario();

        }

        public void OnGet()
        {
            string userID = _httpContextAccessor.HttpContext.Request.Cookies["ID_USER"];
            string connectionString = System.IO.File.ReadAllText("../.connectionstring.txt");
            MySqlConnection conexion = new MySqlConnection(connectionString);

            conexion.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "get_user_info";
            cmd.Connection = conexion;

            cmd.Parameters.AddWithValue("@userID", userID);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    usuario.name = reader["Nombre"].ToString();
                    usuario.firstLastname = reader["APELLIDO_PATERNO"].ToString();
                    usuario.userName = reader["Usuario"].ToString();
                    usuario.profilePicture = reader["Imagen_USUARIO"].ToString().Substring(1);
                }
            }

            dashboardLink = String.Format("https://lookerstudio.google.com/embed/reporting/ed3ae5e0-9da6-401f-8fbb-f4fd23a6d451/page/E3ZwD?params=%7B%22ds21.iduser%22%3A{0}%2C%22ds5.iduserbar%22%3A{0}%7D", userID);

        }
    }
}

