using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;


public class RastrosController : MonoBehaviour
{
    static public RastrosController Instance;
    public GameObject finishPopup;
    public GameObject registerPopup;

    public Text timerText;

    public int time;

    public Image newImage;

    public Text incoText;

    public GameObject joystick;

    Texture2D image;
    Sprite newSprite;

    public class Specie
    {
        public int muestreo;
        public string nombre;
        public string url;
        public int rareza;
    }

    List<Specie> especies = new List<Specie>();
    List<Specie> registros = new List<Specie>();

    public IEnumerator getConection()
    {
        string JSONurl = "https://localhost:7176/api/RegistroEspecie?id=" + Sesion.Instance.getID(); // URL para obtener los datos del libro
        UnityWebRequest request = UnityWebRequest.Get(JSONurl); // Crea una solicitud web para obtener los datos
        request.useHttpContinue = true; // Configura para usar la continuaci�n HTTP

        var cert = new ForceAcceptAll(); // Crea una instancia de la clase para aceptar todos los certificados SSL
        request.certificateHandler = cert; // Asigna el manejador de certificados a la solicitud
        cert?.Dispose(); // Libera la instancia de la clase ForceAceptAll

        yield return request.SendWebRequest(); // Env�a la solicitud web y espera la respuesta

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error Downloading: " + request.error);
        }
        else
        {
            if ((request.downloadHandler.text) == "")
            {
                GameController.Instance.openPopupError();
                Debug.Log("No hay registros");
            }
            else
            {
                especies = JsonConvert.DeserializeObject<List<Specie>>(request.downloadHandler.text);
                Debug.Log(request.downloadHandler.text);
                foreach (Specie especie in especies)
                {
                    if (especie.muestreo == 2)
                    {
                        registros.Add(especie);
                    }
                }

                foreach (Specie especie in registros)
                {
                    Debug.Log(especie.nombre);
                }

            }
        }
    }

    IEnumerator finish()
    {
        joystick.SetActive(false);
        SFXContoller.Instance.PlayClick();
        finishPopup.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("FinalRastros");

    }

    public void exit()
    {
        SFXContoller.Instance.PlayClick();
        finishPopup.SetActive(false);
        joystick.SetActive(true);
    }

    public void continueToScore()
    {
        SFXContoller.Instance.PlayClick();
        SceneManager.LoadScene("FinalRastros");
    }

    public void enter()
    {
        SFXContoller.Instance.PlayClick();
        PlayerPrefs.SetInt("Registros", PlayerPrefs.GetInt("Registros") + 1);
        string val = DropdownRastros.Instance.GetDropdownValue();

        string correct = PlayerPrefs.GetString("rastro");

        Debug.Log("El valor del dropdown " + val);

        if (val == correct)
        {
            incoText.text = "Correcto";
            incoText.color = Color.green;
            Debug.Log("Correcto");
            Debug.Log("Puntaje: " + PlayerPrefs.GetInt("rastroID"));
            PlayerPrefs.SetInt("Puntaje", PlayerPrefs.GetInt("Puntaje") + 1);

            // Validar si ya est� hecho el registro en la base de datos
            bool especieRegistrada = false;
            foreach (Specie especie in registros)
            {
                if (especie.nombre == correct)
                {
                    especieRegistrada = true;
                    Debug.Log("Ya est� registrado");
                    break;
                }
            }

            if (!especieRegistrada)
            {
                StartCoroutine(registrarEspecie());
                Debug.Log("Registrando");
            }

            StartCoroutine(hideText());
        }
        else
        {
            incoText.text = "Incorrecto";
            incoText.color = Color.red;
            Debug.Log("Incorrecto");
            StartCoroutine(hideText());
        }
        registerPopup.SetActive(false);
        Time.timeScale = 1f;
        joystick.SetActive(true);
    }

    IEnumerator registrarEspecie()
    {
        string JSONurl = "https://localhost:7176/api/RegistroEspecie?idUser=" + Sesion.Instance.getID() + "&idEspecie=" + PlayerPrefs.GetInt("rastroID") + "&idMuestreo=2"; // URL para obtener los datos del libro
                                                                                                                                                                             //string JSONurl = "https://localhost:7176/api/RegistroEspecie"; // URL para obtener los datos del libro

        WWWForm form = new WWWForm(); // Crea un formulario web
        form.AddField("idUser", Sesion.Instance.getID()); // Agrega un campo al formulario
        form.AddField("idEspecie", PlayerPrefs.GetInt("rastroID")); // Agrega un campo al formulario
        form.AddField("idMuestreo", 2); // Agrega un campo al formulario



        UnityWebRequest request = UnityWebRequest.Post(JSONurl, form); // Crea una solicitud web para obtener los datos

        Debug.Log(request);

        request.useHttpContinue = true; // Configura para usar la continuaci�n HTTP

        var cert = new ForceAcceptAll(); // Crea una instancia de la clase para aceptar todos los certificados SSL
        request.certificateHandler = cert; // Asigna el manejador de certificados a la solicitud
        cert?.Dispose(); // Libera la instancia de la clase ForceAceptAll

        yield return request.SendWebRequest(); // Env�a la solicitud web y espera la respuesta

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error Downloading: " + request.error);
        }
        else
        {
            // Si el registro es exitoso, agrega la especie registrada a la lista de registros
            Specie nuevaEspecie = new Specie();
            nuevaEspecie.muestreo = 2;
            nuevaEspecie.nombre = PlayerPrefs.GetString("rastro");
            nuevaEspecie.url = ""; // Asigna la URL adecuada si es necesario
            nuevaEspecie.rareza = 0; // Asigna la rareza adecuada si es necesario

            registros.Add(nuevaEspecie);

            Debug.Log("Registro exitoso");
        }
    }


    IEnumerator hideText()
    {
        yield return new WaitForSeconds(2);
        incoText.text = "";

    }

    public void activatePopup(string url)
    {
        joystick.SetActive(false);
        registerPopup.SetActive(true);
        StartCoroutine(DownloadImageCoroutine(url));
    }

    public void Awake()
    {
        Instance = this;
        PlayerPrefs.SetInt("Puntaje", 0);
        PlayerPrefs.SetInt("Registros", 0);
        StartCoroutine(updatetimer());
        StartCoroutine(getConection());

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

    IEnumerator updatetimer()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time -= 1;
            updateTimeText(time.ToString());
        }
        StartCoroutine(finish());
    }

    public void updateTimeText(string time)
    {
        timerText.text = "Tiempo Restante: " + time;
    }

    public void Start()
    {
        SFXContoller.Instance.PlayMusic(SFXContoller.Instance.Rastros);
    }


}
