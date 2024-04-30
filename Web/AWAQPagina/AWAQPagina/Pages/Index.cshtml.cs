using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Data;

namespace AWAQPagina.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Usuario usuario { get; set; }

        public IndexModel()
        {
            usuario = new Usuario(); 
        }

        public IActionResult OnPost()
        {
            string connectionString = System.IO.File.ReadAllText(".connectionstring.txt");
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

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "get_active";
                cmd.Parameters.AddWithValue("@userID", userID);

                object active = cmd.ExecuteScalar();
                usuario.active = Convert.ToBoolean(active);

                if (usuario.active == true)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "get_isAdmin";

                    object isAdmin = cmd.ExecuteScalar();
                    usuario.isAdmin = Convert.ToBoolean(isAdmin);

                    if (usuario.isAdmin == true)
                    {
                        HttpContext.Session.SetString("Role", "Admin");
                        conexion.Close();
                        return Redirect("/adminView");
                    }
                    else
                    {
                        HttpContext.Session.SetString("Role", "Student");
                        conexion.Close();
                        return Redirect("/studentView");

                    }
                }
                else
                {
                    conexion.Close();
                    return RedirectToPage("/Index");
                }

            }

            else if(usuario.userName == null || usuario.password == null){
                ModelState.AddModelError(string.Empty, "");
                return Page();
            }

            else
            {
                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos");
                return Page();
            }
        }
    }
}