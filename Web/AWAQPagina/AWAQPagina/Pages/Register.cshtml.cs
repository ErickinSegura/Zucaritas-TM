using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Data;

namespace AWAQPagina.Pages
{
    public class Register : PageModel
    {
        public Usuario usuario { get; set; }

        public IActionResult OnPost(Usuario usuario)
        {
            string connectionString = "Server=127.0.0.1;Port=3306;Database=AWAQ;Uid=root;password=password;";
            MySqlConnection conexion = new MySqlConnection(connectionString);

            conexion.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "get_id";
            cmd.Connection = conexion;

            cmd.Parameters.AddWithValue("@nombreUsuario", usuario.userName);
            cmd.Parameters.AddWithValue("@passcode", usuario.password);

            object result = cmd.ExecuteScalar();
            int userID = Convert.ToInt32(result);

            usuario.userID = userID;

            if (result != null)
            {
                Response.Cookies.Append("ID_USER", usuario.userID.ToString());
                return RedirectToPage("/studentView");
            }
            else
            {
                return Page();
            }
        }


    }
}
