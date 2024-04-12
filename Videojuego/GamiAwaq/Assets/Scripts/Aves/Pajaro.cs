using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pajaro : MonoBehaviour
{
    public static Pajaro Instance;

    public TipoAve tipoAve = TipoAve.Gorrion; 

    public enum TipoAve
    {
        Gorrion,
        Canario,
        Perico,
        Loro,
        Cacatua,
        Guacamaya,
        Tucan,
        Colibri,
        Pinguino,
        Pato
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
        {TipoAve.Gorrion, new Ave { ID = 1, Name = "Gorrion", Imagen = ""}},
        {TipoAve.Canario, new Ave { ID = 2, Name = "Canario", Imagen = ""}},
        {TipoAve.Perico, new Ave { ID = 3, Name = "Perico", Imagen = ""}},
        {TipoAve.Loro, new Ave { ID = 4, Name = "Loro", Imagen = ""}},
        {TipoAve.Cacatua, new Ave { ID = 5, Name = "Cacatua", Imagen = ""}},
        {TipoAve.Guacamaya, new Ave { ID = 6, Name = "Guacamaya", Imagen = ""}},
        {TipoAve.Tucan, new Ave { ID = 7, Name = "Tucan", Imagen = ""}},
        {TipoAve.Colibri, new Ave { ID = 8, Name = "Colibri", Imagen = ""}},
        {TipoAve.Pinguino, new Ave { ID = 9, Name = "Pinguino", Imagen = ""}},
        {TipoAve.Pato, new Ave { ID = 10, Name = "Pato", Imagen = ""}}
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
                GameManager.instance.IncrementarContadorPajarosClicados();
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
