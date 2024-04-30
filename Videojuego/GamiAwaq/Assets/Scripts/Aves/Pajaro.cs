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
        {TipoAve.Cantil, new Ave { ID = 11, Name = "Pájaro cantil", Imagen = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave1.png?raw=true"}},
        {TipoAve.Gallineta, new Ave { ID = 12, Name = "Gallineta morada", Imagen = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave2.png?raw=true"}},
        {TipoAve.Monja, new Ave { ID = 13, Name = "Monja frentiblanca", Imagen = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave3.png?raw=true"}},
        {TipoAve.Jacamar, new Ave { ID = 14, Name = "Jacamar Cola Canela", Imagen = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave4.png?raw=true"}},
        {TipoAve.Bobo, new Ave { ID = 15, Name = "Bobo barrado", Imagen = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave5.png?raw=true"}},
        {TipoAve.Momoto, new Ave { ID = 16, Name = "Momoto corona azul", Imagen = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave6.png?raw=true"}},
        {TipoAve.Cuclillo, new Ave { ID = 17, Name = "Cuclillo pico negro", Imagen = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave7.png?raw=true"}},
        {TipoAve.Martin, new Ave { ID = 18, Name = "Martí­n pescador amazónico", Imagen = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave8.png?raw=true"}},
        {TipoAve.Perdiz, new Ave { ID = 19, Name = "Perdiz", Imagen = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave9.png?raw=true"}},
        {TipoAve.Pava, new Ave { ID = 20, Name = "Pava cojolita", Imagen = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave10.png?raw=true"}}
    };

    public void Awake()
    {
        Instance = this;
    }


    public void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Pajaro"))
            {
                Ave ave = aves[tipoAve];
                PlayerPrefs.SetString("ave", ave.Name);
                PlayerPrefs.SetInt("aveID", ave.ID);

                GameManager.instance.openPopup(ave.Imagen);
                Debug.Log("Ave tocada: " + ave.Name);
                DropdownAves.Instance.ChangeDropdownOptions(ave.Name);

                GameObject.Destroy(this.gameObject);
            }
        }
    }
}
