using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insecto : MonoBehaviour
{
    public Transform[] lamparas;
    public float velocidad = 3f; // velocidad de movimiento de los insectos

    private Transform objetivo; // lampara objetivo hacia la que se moverá el insecto

    public static Insecto Instance;

    public InsectoType insectosType = InsectoType.Julia;

    public enum InsectoType
    {
        Julia,
        EspinosaVerde,
        Esfinge,
        Morfo,
        Saltarina,
        Purpura,
        Azul,
        Tigre,
        Manchada,
        Sedosa
    }

    [System.Serializable]
    public class Insectos
    {
        public int ID;
        public string Name;
        public string Image;
    }

    public Dictionary<InsectoType, Insectos> insectos = new Dictionary<InsectoType, Insectos>
    {
        {InsectoType.Julia, new Insectos{ID = 31, Name = "Mariposa Julia", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/insecto1.png?raw=true"}},
        {InsectoType.EspinosaVerde, new Insectos{ID = 32, Name = "Rayadora Espinosa Verde", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/insecto2.png?raw=true"}},
        {InsectoType.Esfinge, new Insectos{ID = 33, Name = "Polilla Esfinge Tersa", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/insecto3.png?raw=true"}},
        {InsectoType.Morfo, new Insectos{ID = 34, Name = "Mariposa Morfo Azul", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/insecto4.png?raw=true"}},
        {InsectoType.Saltarina, new Insectos{ID = 35, Name = "Saltarina Blanca", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/insecto5.png?raw=true"}},
        {InsectoType.Purpura, new Insectos{ID = 36, Name = "Rayadora P�rpura", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/insecto6.png?raw=true"}},
        {InsectoType.Azul, new Insectos{ID = 37, Name = "Mariposa Azul", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/insecto7.png?raw=true"}},
        {InsectoType.Tigre, new Insectos{ID = 38, Name = "Mariposa Tigre", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/insecto8.png?raw=true"}},
        {InsectoType.Manchada, new Insectos{ID = 39, Name = "Polilla Manchada", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/insecto9.png?raw=true"}},
        {InsectoType.Sedosa, new Insectos{ID = 40, Name = "Mariposa Sedosa", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/insecto10.png?raw=true"}}
    };

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // encontrar la lampara mas cercana al insecto al inicio
        objetivo = EncontrarLamparaMasCercana();
        StartCoroutine(killBugs());
    }

    void Update()
    {
        // verificar si hay un objetivo asignado
        if (objetivo != null)
        {
            // calcular la dirección hacia el objetivo
            Vector3 direccion = (objetivo.position - transform.position).normalized;

            // mover el insecto hacia el objetivo
            transform.Translate(direccion * velocidad * Time.deltaTime);

            // verificar si el insecto ha llegado a la lampara
            if (Vector3.Distance(transform.position, objetivo.position) < 0.5f)
            {
                //detener el insecto
                velocidad = 0f;
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Pajaro"))
            {
                OnTouchDown();
            }
        }
    }


    IEnumerator killBugs()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    //encontrar la lampara mas cercana al insecto
    private Transform EncontrarLamparaMasCercana()
    {
        Transform lamparaMasCercana = null;
        float distanciaMasCercana = Mathf.Infinity;

        foreach (Transform lampara in lamparas)
        {
            float distancia = Vector3.Distance(transform.position, lampara.position);
            if (distancia < distanciaMasCercana)
            {
                distanciaMasCercana = distancia;
                lamparaMasCercana = lampara;
            }
        }

        return lamparaMasCercana;
    }

    void OnTouchDown()
    {
        Insectos insecto = insectos[insectosType];
        InsectoController.Instance.activatePopup(insecto.Image);
        Debug.Log(insecto.Name);
        DropdownInsecto.Instance.ChangeDropdownOptions(insecto.Name);
        GameObject.Destroy(this.gameObject);
        PlayerPrefs.SetString("insecto", insecto.Name);
        PlayerPrefs.SetInt("insectoID", insecto.ID);
    }
}
