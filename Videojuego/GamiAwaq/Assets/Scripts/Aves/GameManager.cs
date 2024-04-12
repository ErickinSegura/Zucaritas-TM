using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public int tiempoTotal = 60;
    private float tiempoRestante;
    public Text textoTiempo;
    private int contadorPajarosClicados = 0;
    public Text textoContadorPajaros;

    public GameObject popup;

    public Image newImage;

    public Text incoText;

    Texture2D image;
    Sprite newSprite;

    static public GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void IncrementarContadorPajarosClicados()
    {
        contadorPajarosClicados++;
        Debug.Log("Contador de pájaros clicados incrementado: " + contadorPajarosClicados);
        ActualizarContadorPajarosUI();
    }

    void Start()
    {
        tiempoRestante = tiempoTotal;
        StartTimer(); // Iniciar el temporizador al inicio
        ActualizarContadorPajarosUI();
    }

    IEnumerator MatchTime()
    {
        while (tiempoRestante > 0)
        {
            yield return new WaitForSeconds(1);
            tiempoRestante -= 1;
            ActualizarTiempoUI();
        }
        continueToScore();
        
    }

    // Actualizar el texto del temporizador
    void ActualizarTiempoUI()
    {
        textoTiempo.text = "Tiempo Restante: " + tiempoRestante;
    }

    // Iniciar el temporizador
    public void StartTimer()
    {
        StartCoroutine(MatchTime());
    }

    // Actualizar el contador de pájaros en la interfaz de usuario
    void ActualizarContadorPajarosUI()
    {
        textoContadorPajaros.text = "Pájaros Clicados: " + contadorPajarosClicados;
    }

    public void continueToScore()
    {
        SceneManager.LoadScene("Arbol Cam");
    }



    public void enter()
    {
        PlayerPrefs.SetInt("Registros", PlayerPrefs.GetInt("Registros") + 1);
        string val = DropdownAves.Instance.GetDropdownValue();

        string correct = PlayerPrefs.GetString("ave");

        Debug.Log("El valor del dropdown " + val);

        if (val == correct)
        {
            incoText.text = "Correcto";
            incoText.color = Color.green;
            Debug.Log("Correcto");
            PlayerPrefs.SetInt("Puntaje", PlayerPrefs.GetInt("Puntaje") + 1);
            StartCoroutine(hideText());
        }
        else
        {
            incoText.text = "Incorrecto";
            incoText.color = Color.red;
            Debug.Log("Incorrecto");
            StartCoroutine(hideText());
        }
        popup.SetActive(false);
        Time.timeScale = 1f;
    }

    IEnumerator hideText()
    {
        yield return new WaitForSeconds(2);
        incoText.text = "";
    }

    public void openPopup(string imgUrl)
    {
        popup.SetActive(true);
        Time.timeScale = 0f;
        StartCoroutine(DownloadImageCoroutine(imgUrl));
    }

    IEnumerator DownloadImageCoroutine(string MediaUrl)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error Downloading: " + request.error);
            }
            else
            {
                image = DownloadHandlerTexture.GetContent(request);
                newSprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f), 100.0f);
                newImage.sprite = newSprite;
            }
        }
    }
}
