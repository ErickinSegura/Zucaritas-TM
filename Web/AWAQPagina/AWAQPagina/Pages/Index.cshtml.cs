using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace AWAQPagina.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Usuario usuario { get; set; }

        public IndexModel()
        {
            usuario = new Usuario(); // Inicializa el objeto usuario
        }

        public IActionResult OnPost()
        {
            string connectionString = "Server=127.0.0.1;Port=3306;Database=AWAQ;Uid=root;password=Vela0376;";
            MySqlConnection conexion = new MySqlConnection(connectionString);

            conexion.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "get_id";
            cmd.Connection = conexion;

            cmd.Parameters.AddWithValue("@nombreUsuario", usuario.userName);
            cmd.Parameters.AddWithValue("@passcode", usuario.password);

            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                int userID = Convert.ToInt32(result);
                usuario.userID = userID;
                Response.Cookies.Append("ID_USER", usuario.userID.ToString());
                return RedirectToPage("/studentView");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Usuario o contrase�a incorrectos");
                return Page();
            }
        }
    }
}
