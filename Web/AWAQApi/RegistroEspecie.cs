namespace AWAQApi
{
    public class RegistroEspecie
    {
        public int? ID_Registro { get; set; }
        public int? ID_Especie { get; set; }
        public string URL { get; set; }
        public string Nombre { get; set; }
        public int? ID_Usuario { get; set; }
        public int? ID_Muestreo { get; set; }
        public DateTime? Fecha { get; set; }

    }
}
