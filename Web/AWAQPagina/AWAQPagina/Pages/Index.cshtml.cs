using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace AWAQPagina.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

   /* public void OnPost()
    {
        string connectionString = "Server=127.0.0.1;Port=3306;Database=AWAQ;Uid=root;password=Vela0376;";
        string usuario = Request.Form["usuario"];
        string contraseña = Request.Form["contraseña"];
       
        string sql = "SELECT COUNT(*) FROM USUARIO WHERE Usuario == @usuario AND Contraseña == @contraseña";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                    using(MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@username", usuario);
                    command.Parameters.AddWithValue("@password", contraseña);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count > 0)
                    {
                        Response.Redirect("");
                    }
                }

          
            }
        }



    }*/

    public void OnGet()
    {

    }

}