using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AWAQPagina.Pages
{
    public class EspecieModel : PageModel
    {
        public string? IdEspecie { set; get; }
        public Especie? especie { set; get; }

        public void OnGet(string idEspecieParam)
        {
            IdEspecie = idEspecieParam;

            especie = new Especie();

            especie.nombre_Especie = "Caimán Aguja";
            especie.img_web = "https://inaturalist-open-data.s3.amazonaws.com/photos/3715/large.jpg";
            especie.descripcion = "Es una especie de cocodrílido que vive en Florida, algunas islas del Mar Caribe y varias zonas costeras del golfo de México y el océano Pacífico. Se le suele confundir con el Alligator, pero éste último es un animal poco emparentado con el anterior, más próximo a los caimanes como el yacaré overo. Las diferencias físicas entre ambos son importantes, lo que permite identificarlos";
            especie.nombre_cientifico = "Crocodylus acutus";
            especie.ID_Especie = "4";
        }
    }
}