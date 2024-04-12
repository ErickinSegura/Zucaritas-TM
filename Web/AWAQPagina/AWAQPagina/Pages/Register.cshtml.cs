using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Data;

namespace AWAQPagina.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public Usuario usuario { get; set; }

        public IActionResult OnPost(Usuario usuario)
        {
            string connectionString = "Server=127.0.0.1;Port=3306;Database=AWAQ;Uid=root;password=Vela0376;";
            MySqlConnection conexion = new MySqlConnection(connectionString);

            conexion.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "new_register";
            cmd.Connection = conexion;

            cmd.Parameters.AddWithValue("@nombre", usuario.name);
            cmd.Parameters.AddWithValue("@apellidoPaterno", usuario.firstLastname);
            cmd.Parameters.AddWithValue("@apellidoMaterno", usuario.secondLastname);
            cmd.Parameters.AddWithValue("@usuario", usuario.userName);
            cmd.Parameters.AddWithValue("@passcode", usuario.password);
            cmd.Parameters.AddWithValue("@admin", 0);

            cmd.ExecuteNonQuery();

            return RedirectToPage();


        }   
    }
}
