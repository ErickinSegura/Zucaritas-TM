using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReptileBehaviour : MonoBehaviour
{
    static public ReptileBehaviour Instance;

    public ReptileType reptileType = ReptileType.CaimanAguja;    

    public enum ReptileType
    {
        CaimanAguja,
        CaimanLlanero,
        SerpienteSabanera,
        SerpienteTerciopelo,
        SerpienteSanAndres,
        TortugaCienegaCol,
        TortugaMorrocoy,
        CamaleonCundimamarca,
        AnolisCalima,
        LagartijaBogota
    }

    [System.Serializable]
    public class Reptile
    {
        public int ID;
        public string Name;
        public string Image;
    }



    public Dictionary<ReptileType, Reptile> reptiles = new Dictionary<ReptileType, Reptile>
    {
        {ReptileType.CaimanAguja, new Reptile { ID = 1, Name = "Caimán Aguja", Image = "https://inaturalist-open-data.s3.amazonaws.com/photos/3715/large.jpg" } },
        {ReptileType.CaimanLlanero, new Reptile { ID = 2, Name = "Caimán Llanero", Image = "https://img.lalr.co/cms/2022/12/05112918/Caiman-llanero.jpg" } },
        {ReptileType.SerpienteSabanera, new Reptile { ID = 3, Name = "Serpiente Sabanera", Image = "https://static.inaturalist.org/photos/215474301/large.jpeg" } },
        {ReptileType.SerpienteTerciopelo, new Reptile { ID = 4, Name = "Serpiente Terciopelo", Image = "https://culturacientifica.utpl.edu.ec/wp-content/uploads/2023/07/Bothrops-asper-vibora-de-terciopelo-1.jpg" } },
        {ReptileType.SerpienteSanAndres, new Reptile { ID = 5, Name = "Serpiente San Andrés", Image = "https://inaturalist-open-data.s3.amazonaws.com/photos/130995125/large.jpg" } },
        {ReptileType.TortugaCienegaCol, new Reptile { ID = 6, Name = "Tortuga Ciénega Col", Image = "https://static.wikia.nocookie.net/colombia/images/b/b2/Tortuga_de_ci%C3%A9naga_colombiana.jpg/revision/latest?cb=20130120201413&path-prefix=es" } },
        {ReptileType.TortugaMorrocoy, new Reptile { ID = 7, Name = "Tortuga Morrocoy", Image = "https://upload.wikimedia.org/wikipedia/commons/a/ae/Chelonoidis_carbonaria_LoroParqueTenerife_red-foot_tortoise_IMG_5135.JPG" } },
        {ReptileType.CamaleonCundimamarca, new Reptile { ID = 8, Name = "Camaleón Cundimamarca", Image = "https://upload.wikimedia.org/wikipedia/commons/6/6f/Anolis_heterodermus01.jpeg" } },
        {ReptileType.AnolisCalima, new Reptile { ID = 9, Name = "Anolis Calima", Image = "https://inaturalist-open-data.s3.amazonaws.com/photos/65194113/large.jpg" } },
        {ReptileType.LagartijaBogota, new Reptile { ID = 10, Name = "Lagartija Bogotá", Image = "https://www.dicyt.com/data/04/29004.jpg" } }
    };
    
    private void Awake()
    {
        Instance = this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Reptile reptile = reptiles[reptileType];
            ReptilesController.Instance.activatePopup(reptile.Image);
            Dropdown.Instance.ChangeDropdownOptions(reptile.Name);
            GameObject.Destroy(this.gameObject);
            PlayerPrefs.SetString("reptile", reptile.Name);
        }
    }



    
}
