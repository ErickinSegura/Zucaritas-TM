using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Data;

namespace AWAQPagina.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public Usuario? usuario { get; set; }

        public bool hasError { get; set; }

        public bool userCreated { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
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
                hasError = false;
                userCreated = false;

                return Page();
            }
        }

        public IActionResult OnPost(Usuario usuario)
        {
            string connectionString = System.IO.File.ReadAllText(".connectionstring.txt");
            MySqlConnection conexion = new MySqlConnection(connectionString);
            bool existing_user;

            conexion.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "existing_user";
            cmd.Connection = conexion;

            cmd.Parameters.AddWithValue("@userName", usuario.userName);
            object result = cmd.ExecuteScalar();
            existing_user = Convert.ToBoolean(result);

            if (existing_user != true)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "new_register";

                try
                {
                    cmd.Parameters.AddWithValue("@nombre", usuario.name);
                    cmd.Parameters.AddWithValue("@apellidoPaterno", usuario.firstLastname);
                    cmd.Parameters.AddWithValue("@apellidoMaterno", usuario.secondLastname);
                    cmd.Parameters.AddWithValue("@usuario", usuario.userName);
                    cmd.Parameters.AddWithValue("@passcode", usuario.password);
                    cmd.Parameters.AddWithValue("@admin", 0);
                    cmd.Parameters.AddWithValue("@active", 1);
                    cmd.ExecuteNonQuery();

                    userCreated = true;
                    conexion.Close();
                    return Page();

                }

                catch
                {
                    conexion.Close();
                    return Page();
                }
            }
            else
            {
                hasError = true;
                conexion.Close();
                return Page();
            }

        }

    }

}