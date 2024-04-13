using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pajaro : MonoBehaviour
{
    public static Pajaro Instance;

    public TipoAve tipoAve = TipoAve.Cantil; 

    public enum TipoAve
    {
        Cantil,
        Gallineta,
        Monja,
        Jacamar,
        Bobo,
        Momoto,
        Cuclillo,
        Martin,
        Perdiz,
        Pava
    }

    [System.Serializable]
    public class Ave
    {
        public int ID;
        public string Name;
        public string Imagen;
    }

    public Dictionary<TipoAve, Ave> aves = new Dictionary<TipoAve, Ave>
    {
        {TipoAve.Cantil, new Ave { ID = 1, Name = "Pájaro cantil", Imagen = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave1.png?raw=true"}},
        {TipoAve.Gallineta, new Ave { ID = 2, Name = "Gallineta morada", Imagen = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave2.png?raw=true"}},
        {TipoAve.Monja, new Ave { ID = 3, Name = "Monja frentiblanca", Imagen = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave3.png?raw=true"}},
        {TipoAve.Jacamar, new Ave { ID = 4, Name = "Jacamar Cola Canela", Imagen = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave4.png?raw=true"}},
        {TipoAve.Bobo, new Ave { ID = 5, Name = "Bobo barrado", Imagen = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave5.png?raw=true"}},
        {TipoAve.Momoto, new Ave { ID = 6, Name = "Momoto corona azul", Imagen = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave6.png?raw=true"}},
        {TipoAve.Cuclillo, new Ave { ID = 7, Name = "Cuclillo pico negro", Imagen = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave7.png?raw=true"}},
        {TipoAve.Martin, new Ave { ID = 8, Name = "Martí­n pescador amazónico", Imagen = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave8.png?raw=true"}},
        {TipoAve.Perdiz, new Ave { ID = 9, Name = "Perdiz", Imagen = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave9.png?raw=true"}},
        {TipoAve.Pava, new Ave { ID = 10, Name = "Pava cojolita", Imagen = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave10.png?raw=true"}}
    };

    public void Awake()
    {
        Instance = this;
    }


    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Pajaro"))
            {
                Ave ave = aves[tipoAve];
                PlayerPrefs.SetString("ave", ave.Name);
                GameManager.instance.openPopup(ave.Imagen);
                Debug.Log("Ave clickeada: " + ave.Name);
                DropdownAves.Instance.ChangeDropdownOptions(ave.Name);

                GameObject.Destroy(this.gameObject);

            }

        }
    }

}
