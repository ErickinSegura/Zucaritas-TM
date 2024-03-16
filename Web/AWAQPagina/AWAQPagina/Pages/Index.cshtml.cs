using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;

namespace AWAQPagina.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly string _connectionString = "Server=127.0.0.1;Port=3306;Database=AWAQ;Uid=root;password=lala14;";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(ILogger<IndexModel> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId { get; private set; }
        public string nombre{ get; private set; }
        public string apellido { get; private set; }
        public string nombreUsuario { get; private set; }

        public IActionResult OnPost()
        {
            string usuario = Request.Form["usuario"];
            string contraseña = Request.Form["contraseña"];

            string sql = "SELECT COUNT(*) FROM USUARIO WHERE Usuario = @usuario AND Contraseña = @contraseña";
            string sql2 = "SELECT ID_USER FROM USUARIO WHERE Usuario = @usuario AND Contraseña = @contraseña";
            string sql3 = "SELECT Nombre FROM USUARIO WHERE Usuario = @usuario AND Contraseña = @contraseña";
            string sql4 = "SELECT APELLIDO_PATERNO FROM USUARIO WHERE Usuario = @usuario AND Contraseña = @contraseña";
            string sql5 = "SELECT Usuario FROM USUARIO WHERE Usuario = @usuario AND Contraseña = @contraseña";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                // Check if the user exists
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@usuario", usuario);
                    command.Parameters.AddWithValue("@contraseña", contraseña);

                    int count = Convert.ToInt32(command.ExecuteScalar());


                    if (count > 0)
                    {
                        // If the user exists, execute the second query to get the user ID
                        using (MySqlCommand command2 = new MySqlCommand(sql2, connection))
                        {
                            command2.Parameters.AddWithValue("@usuario", usuario);
                            command2.Parameters.AddWithValue("@contraseña", contraseña);

                            object result = command2.ExecuteScalar();
                            if (result != null)
                            {
                                UserId = result.ToString(); // Store the user ID in the cookie

                                // Set the UserId cookie
                                _httpContextAccessor.HttpContext.Response.Cookies.Append("UserId", UserId);
                            }
                        }


                        using (MySqlCommand command3 = new MySqlCommand(sql3, connection))
                        {
                            command3.Parameters.AddWithValue("@usuario", usuario);
                            command3.Parameters.AddWithValue("@contraseña", contraseña);

                            object result = command3.ExecuteScalar();

                            if (result != null)
                            {
                                nombre = result.ToString();
                                _httpContextAccessor.HttpContext.Response.Cookies.Append("Nombre", nombre);

                            }
                        }

                        using (MySqlCommand command4 = new MySqlCommand(sql4, connection))
                        {
                            command4.Parameters.AddWithValue("@usuario", usuario);
                            command4.Parameters.AddWithValue("@contraseña", contraseña);

                            object result = command4.ExecuteScalar();

                            if (result != null)
                            {
                                apellido = result.ToString();
                                _httpContextAccessor.HttpContext.Response.Cookies.Append("APELLIDO_PATERNO", apellido);
                            }
                        }


                        using (MySqlCommand command5 = new MySqlCommand(sql5, connection))
                        {
                            command5.Parameters.AddWithValue("@usuario", usuario);
                            command5.Parameters.AddWithValue("@contraseña", contraseña);

                            object result = command5.ExecuteScalar();

                            if (result != null)
                            {
                                nombreUsuario = result.ToString();
                                _httpContextAccessor.HttpContext.Response.Cookies.Append("Usuario", nombreUsuario);
                            }
                        }


                        // Redirect to studentView page
                        return RedirectToPage("/studentView");
                    }
                }
            }

            // If authentication fails, return to the same page
            return Page();
        }

        public void OnGet()
        {
            // Quitar cookies
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("UserId");
            

            
        }
    }
}
