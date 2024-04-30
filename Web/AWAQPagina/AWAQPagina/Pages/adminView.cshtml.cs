using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Data;

namespace AWAQPagina.Pages
{
    public class adminViewModel : PageModel
    {
        public Usuario usuario { get; set; }

        public adminViewModel()
        {
            usuario = new Usuario();
        }

        public IActionResult OnGet()
        {
            if(HttpContext.Session.GetString("Role") != "Admin")
            {
                if (HttpContext.Session.GetString("Role") != "Student")
                {
                    return RedirectToPage("/Index");
                }

                else
                {
                    return RedirectToPage("/studentView");
                }
            }

            else
            {
                string? userID = HttpContext.Request.Cookies["ID_USER"];
                string connectionString = System.IO.File.ReadAllText(".connectionstring.txt");
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
                conexion.Close();
                return Page();
            }
        }
    }
}

