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
        {VegeType.Rosa, new Vegetacion { ID = 51, Name = "Rosa", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/pla1.png?raw=true" } },
        {VegeType.Crisantemo, new Vegetacion { ID = 52, Name = "Crisantemo", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/pla2.png?raw=true" } },
        {VegeType.Clavel, new Vegetacion { ID = 53, Name = "Clavel", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/pla3.png?raw=true" } },
        {VegeType.Lirio, new Vegetacion { ID = 54, Name = "Lirio", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/pla4.png?raw=true" } },
        {VegeType.Bromelia, new Vegetacion { ID = 55, Name = "Bromelia", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/pla5.png?raw=true" } },
        {VegeType.Orquidea, new Vegetacion { ID = 56, Name = "Orquídea", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/pla6.png?raw=true" } },
        {VegeType.Frailejon, new Vegetacion { ID = 57, Name = "Frailejón", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/pla7.png?raw=true" } },
        {VegeType.Magnolia, new Vegetacion { ID = 58, Name = "Magnolia", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/pla8.png?raw=true" } },
        {VegeType.Helecho, new Vegetacion { ID = 59, Name = "Helecho", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/pla9.png?raw=true" } },
        {VegeType.Drosera, new Vegetacion { ID = 60, Name = "Drosera", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/pla10.png?raw=true" } }
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
            PlayerPrefs.SetInt("vegetacionID", vegetacion.ID);
        }
    }

}
