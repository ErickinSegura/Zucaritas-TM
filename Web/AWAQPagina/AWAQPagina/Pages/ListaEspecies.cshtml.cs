using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AWAQPagina.Pages
{
	public class ListaEspeciesModel : PageModel
    {
        public List<Especie>? especies { set; get; }


        public void OnGet()
        {
            especies = new List<Especie>();
            for (int i=0; i<3; i++)
            {
                Especie especie = new Especie();
                especie.nombre_Especie = "nombre" + i.ToString();
                especie.nombre_cientifico = "cientifico" + i.ToString();
                especie.descripcion = "descripcion" + i.ToString();
                especie.img_web = "/images/1.jpg";

                especies.Add(especie);

            }
        }
    }
}
