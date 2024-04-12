using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float tiempoTotal = 60f;
    private float tiempoRestante;
    public Text textoTiempo;
    private int contadorPajarosClicados = 0;
    public Text textoContadorPajaros;

    static public GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    public void IncrementarContadorPajarosClicados()
    {
        contadorPajarosClicados++;
        Debug.Log("Contador de p�jaros clicados incrementado: " + contadorPajarosClicados);
        ActualizarContadorPajarosUI();
    }


    void Start()
    {
        tiempoRestante = tiempoTotal;
        ActualizarTiempoUI();
        ActualizarContadorPajarosUI();
    }

    void Update()
    {
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            ActualizarTiempoUI();
        }
        else
        {
            Debug.Log("Game Over");
            // Aqu� podr�as agregar l�gica para manejar el fin del juego
        }
    }

    void ActualizarTiempoUI()
    {
        textoTiempo.text = "Tiempo: " + Mathf.Round(tiempoRestante);
    }

    void ActualizarContadorPajarosUI()
    {
        textoContadorPajaros.text = "P�jaros Clicados: " + contadorPajarosClicados;
    }
}
