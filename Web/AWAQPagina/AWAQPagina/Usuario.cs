using System;
using System.ComponentModel.DataAnnotations;

namespace AWAQPagina
{
	public class Usuario
	{
		public int userID { get; set; }
        public string name { get; set; }
        public string firstLastname { get; set; }
        public string secondLastname { get; set; }
        public string profilePicture { get; set; }
        public string bio { get; set; }
        public bool isAdmin { get; set; }

        [Required(ErrorMessage = "El campo de nombre de usuario es obligatorio")]
		public string userName { get; set; }

        [Required(ErrorMessage = "El campo de contraseña es obligatorio")]
        public string password { get; set; }

        
    }
}

