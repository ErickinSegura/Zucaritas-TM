using System;

namespace AWAQPagina
{
	public class Usuario
	{
		public int userID { get; set; }
		public string userName { get; set; }
		public string password { get; set; }
		public string name { get; set; }
		public string firstLastname { get; set; }
		public string secondLastname { get; set; }
		public string profilePicture { get; set; }
		public string bio { get; set; }
		public bool isAdmin { get; set; }

		public Usuario()
		{

		}
	}
}

