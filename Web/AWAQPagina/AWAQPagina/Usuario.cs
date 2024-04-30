using System.ComponentModel.DataAnnotations;

namespace AWAQPagina
{
	public class Usuario
	{
        [Required(ErrorMessage = "El campo de nombre de usuario es obligatorio")]
		public string? userName { get; set; }

        [Required(ErrorMessage = "El campo de contraseña es obligatorio")]
        public string? password { get; set; } 

        [Required(ErrorMessage = "El campo de nombre es obligatorio")]
        public string? name { get; set; }

        [Required(ErrorMessage = "El campo de apellido paterno es obligatorio")]
        public string? firstLastname { get; set; }

        [Required(ErrorMessage = "El campo de apellido materno es obligatorio")]
        public string? secondLastname { get; set; }

        public string? profilePicture { get; set; }
        public string? bio { get; set; }
        public bool isAdmin { get; set; }
        public bool active { get; set; }
        public int userID { get; set; }
        public string? directory { get; set; }
    }
}

