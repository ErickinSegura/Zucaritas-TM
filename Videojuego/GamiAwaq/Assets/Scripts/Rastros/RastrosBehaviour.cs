using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RastrosBehaviour : MonoBehaviour
{
    static public RastrosBehaviour Instance;

    public RestrosType rastrosType = RestrosType.HuellaDeTortuga;


    public enum RestrosType
    {
        HuellaDeTortuga,
        HuellaDeCocodrilo,
        RastroDeSerpiente,
        HuellaDeLagartija,
        HuellaDeCamaleon,
        HuellaDeOcelote,
        HuellaDeZorro,
        HuellaDeArmadillo,
        HuellaDeMapache,
        HuellaDeCoati
    }

    [System.Serializable]
    public class Rastros
    {
        public int ID;
        public string Name;
        public string Image;
    }


    public Dictionary<RestrosType, Rastros> rastros = new Dictionary<RestrosType, Rastros>
    {
        {RestrosType.HuellaDeTortuga, new Rastros{ID = 0, Name = "Huella de Tortuga", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ras1.jpg?raw=true"}},
        {RestrosType.HuellaDeCocodrilo, new Rastros{ID = 1, Name = "Huella de Cocodrilo", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ras2.jpg?raw=true"}},
        {RestrosType.RastroDeSerpiente, new Rastros{ID = 2, Name = "Rastro de Serpiente", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ras3.jpg?raw=true"}},
        {RestrosType.HuellaDeLagartija, new Rastros{ID = 3, Name = "Huella de Lagartija", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ras4.jpg?raw=true"}},
        {RestrosType.HuellaDeCamaleon, new Rastros{ID = 4, Name = "Huella de Camaleon", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ras5.jpg?raw=true"}},
        {RestrosType.HuellaDeOcelote, new Rastros{ID = 5, Name = "Huella de Ocelote", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ras6.jpg?raw=true"}},
        {RestrosType.HuellaDeZorro, new Rastros{ID = 6, Name = "Huella de Zorro", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ras7.jpg?raw=true"}},
        {RestrosType.HuellaDeArmadillo, new Rastros{ID = 7, Name = "Huella de Armadillo", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ras8.jpg?raw=true"}},
        {RestrosType.HuellaDeMapache, new Rastros{ID = 8, Name = "Huella de Mapache", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ras9.jpg?raw=true"}},
        {RestrosType.HuellaDeCoati, new Rastros{ID = 9, Name = "Huella de Coati", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ras9.jpg?raw=true"}}
    };

    private void Awake()
    {
        Instance = this;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rastros rastro = rastros[rastrosType];
            RastrosController.Instance.activatePopup(rastro.Image);
            DropdownRastros.Instance.ChangeDropdownOptions(rastro.Name);
            Debug.Log(rastro.Name);
            GameObject.Destroy(this.gameObject);
            PlayerPrefs.SetString("rastro", rastro.Name);
        }
    }
}
