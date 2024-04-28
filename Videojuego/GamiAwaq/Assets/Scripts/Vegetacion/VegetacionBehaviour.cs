using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetacionBehaviour : MonoBehaviour
{
    static public VegetacionBehaviour Instance;

    public VegeType vegeType = VegeType.Rosa;

    public enum VegeType
    {
        Rosa,
        Crisantemo,
        Clavel,
        Lirio,
        Bromelia,
        Orquidea,
        Frailejon,
        Magnolia,
        Helecho,
        Drosera
    }

    [System.Serializable]
    public class Vegetacion
    {
        public int ID;
        public string Name;
        public string Image;
    }


    public Dictionary<VegeType, Vegetacion> veges = new Dictionary<VegeType, Vegetacion>
    {
        {VegeType.Rosa, new Vegetacion { ID = 1, Name = "Rosa", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep8.png?raw=true" } },
        {VegeType.Crisantemo, new Vegetacion { ID = 2, Name = "Crisantemo", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep4.png?raw=true" } },
        {VegeType.Clavel, new Vegetacion { ID = 3, Name = "Clavel", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep7.png?raw=true" } },
        {VegeType.Lirio, new Vegetacion { ID = 4, Name = "Lirio", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep3.png?raw=true" } },
        {VegeType.Bromelia, new Vegetacion { ID = 5, Name = "Bromelia", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep5.png?raw=true" } },
        {VegeType.Orquidea, new Vegetacion { ID = 6, Name = "Orquidea", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep9.png?raw=true" } },
        {VegeType.Frailejon, new Vegetacion { ID = 7, Name = "Frailejon", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep10.png?raw=true" } },
        {VegeType.Magnolia, new Vegetacion { ID = 8, Name = "Magnolia", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep2.png?raw=true" } },
        {VegeType.Helecho, new Vegetacion { ID = 9, Name = "Helecho", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep6.png?raw=true" } },
        {VegeType.Drosera, new Vegetacion { ID = 10, Name = "Drosera", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep1.png?raw=true" } }
    };

    private void Awake()
    {
        Instance = this;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vegetacion vegetacion = veges[vegeType];
            VegetacionContoller.Instance.activatePopup(vegetacion.Image);
            DropdownVegetacion.Instance.ChangeDropdownOptions(vegetacion.Name);
            GameObject.Destroy(this.gameObject);
            PlayerPrefs.SetString("vegetacion", vegetacion.Name);
        }
    }

}
