namespace AWAQPagina
{
    public class Especie
    {
        public string? ID_Especie { set; get; }
        public string? nombre_Especie { set; get; }
        public string? img_web { set; get; }
        public string? descripcion { set; get; }
        public string? nombre_cientifico { set; get; }
        public string? directory { set; get; }

        Especie(string idEspecie)
        {
            directory = "/Especie?idEspecieParam=" + idEspecie.ToString();
        }
    }
}
