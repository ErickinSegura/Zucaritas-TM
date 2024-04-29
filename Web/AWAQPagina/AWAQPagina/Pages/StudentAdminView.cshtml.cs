using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace AWAQPagina.Pages
{
	public class StudentAdminViewModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Usuario usuario { get; set; }
        public string dashboardLink { get; set; }

        public StudentAdminViewModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            usuario = new Usuario();

        }

        public IActionResult OnGet(string id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                if (HttpContext.Session.GetString("Role") != "Student")
                {
                    return RedirectToPage("Index");
                }

                else
                {
                    return RedirectToPage("/studentView");
                }
            }

            else
            {
                string userID = id; //id que se esta buscando  
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

                usuario.bio = "Biografia placeholder";
                // Agregar query

                
                dashboardLink = String.Format("https://lookerstudio.google.com/embed/reporting/ed3ae5e0-9da6-401f-8fbb-f4fd23a6d451/page/E3ZwD?params=%7B%22ds21.iduser%22%3A{0}%2C%22ds5.iduserbar%22%3A{0}%7D", userID);

                return Page();
            }
        }
    }
}
