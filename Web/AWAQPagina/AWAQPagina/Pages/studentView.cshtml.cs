using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace AWAQPagina.Pages
{
    public class studentViewModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Usuario usuario { get; set; }

        public studentViewModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            usuario = new Usuario();

        }

        public void OnGet()
        {
            string userID = _httpContextAccessor.HttpContext.Request.Cookies["ID_USER"];
            string connectionString = "Server=127.0.0.1;Port=3306;Database=AWAQ;Uid=root;password=Vela0376;";
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

        }
    }
}

