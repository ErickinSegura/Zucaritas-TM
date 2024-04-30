using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class ReptilesController : MonoBehaviour
{
    public class Specie
    {
        public int muestreo;
        public string nombre;
        public string url;
        public int rareza;
    }

    static public ReptilesController Instance;
    public GameObject finishPopup;
    public GameObject registerPopup;

    public GameObject joystick;

    public Image newImage;

    public Text incoText;

    Texture2D image;
    Sprite newSprite;

    List<Specie> especies = new List<Specie>();
    List<Specie> registros = new List<Specie>();


    public IEnumerator getConection()
    {
        string JSONurl = "https://localhost:7176/api/RegistroEspecie?id=" + Sesion.Instance.getID(); // URL para obtener los datos del libro
        UnityWebRequest request = UnityWebRequest.Get(JSONurl); // Crea una solicitud web para obtener los datos
        request.useHttpContinue = true; // Configura para usar la continuación HTTP

        var cert = new ForceAcceptAll(); // Crea una instancia de la clase para aceptar todos los certificados SSL
        request.certificateHandler = cert; // Asigna el manejador de certificados a la solicitud
        cert?.Dispose(); // Libera la instancia de la clase ForceAceptAll

        yield return request.SendWebRequest(); // Envía la solicitud web y espera la respuesta

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
                    if (especie.muestreo == 4)
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

    public void finish()
    {
        SFXContoller.Instance.PlaySFX(SFXContoller.Instance.click);
        finishPopup.SetActive(true);
        joystick.SetActive(false);
    }

    public void exit()
    {
        SFXContoller.Instance.PlaySFX(SFXContoller.Instance.click);
        finishPopup.SetActive(false);
        joystick.SetActive(true);
    }

    public void continueToScore()
    {
        SFXContoller.Instance.PlaySFX(SFXContoller.Instance.click);
        SceneManager.LoadScene("FinalReptile");
    }



    public void enter()
    {
        SFXContoller.Instance.PlaySFX(SFXContoller.Instance.click);
        PlayerPrefs.SetInt("Registros", PlayerPrefs.GetInt("Registros")+1);
        string val = Dropdown.Instance.GetDropdownValue();

        string correct = PlayerPrefs.GetString("reptile");

        Debug.Log("El valor del dropdown "+val);

        if (val == correct)
        {
            incoText.text = "Correcto";
            incoText.color = Color.green;
            Debug.Log("Correcto");
            Debug.Log("Puntaje: " + PlayerPrefs.GetInt("reptileID"));
            PlayerPrefs.SetInt("Puntaje", PlayerPrefs.GetInt("Puntaje") + 1);

            // Validar si ya está hecho el registro en la base de datos
            bool especieRegistrada = false;
            foreach (Specie especie in registros)
            {
                if (especie.nombre == correct)
                {
                    SFXContoller.Instance.PlaySFX(SFXContoller.Instance.correct);
                    especieRegistrada = true;
                    Debug.Log("Ya está registrado");
                    break;
                }
            }

            if (!especieRegistrada)
            {
                SFXContoller.Instance.PlaySFX(SFXContoller.Instance.register);
                StartCoroutine(registrarEspecie());
                Debug.Log("Registrando");
            }

            StartCoroutine(hideText());
        }
        else
        {
            SFXContoller.Instance.PlaySFX(SFXContoller.Instance.error);
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
        string JSONurl = "https://localhost:7176/api/RegistroEspecie?idUser="+ Sesion.Instance.getID() + "&idEspecie="+ PlayerPrefs.GetInt("reptileID") + "&idMuestreo=4"; // URL para obtener los datos del libro
        //string JSONurl = "https://localhost:7176/api/RegistroEspecie"; // URL para obtener los datos del libro
        
        WWWForm form = new WWWForm(); // Crea un formulario web
        form.AddField("idUser", Sesion.Instance.getID()); // Agrega un campo al formulario
        form.AddField("idEspecie", PlayerPrefs.GetInt("reptileID")); // Agrega un campo al formulario
        form.AddField("idMuestreo", 4); // Agrega un campo al formulario

        UnityWebRequest request = UnityWebRequest.Post(JSONurl, form); // Crea una solicitud web para obtener los datos

        request.useHttpContinue = true; // Configura para usar la continuación HTTP

        var cert = new ForceAcceptAll(); // Crea una instancia de la clase para aceptar todos los certificados SSL
        request.certificateHandler = cert; // Asigna el manejador de certificados a la solicitud
        cert?.Dispose(); // Libera la instancia de la clase ForceAceptAll

        yield return request.SendWebRequest(); // Envía la solicitud web y espera la respuesta

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error Downloading: " + request.error);
        }
        else
        {
            // Si el registro es exitoso, agrega la especie registrada a la lista de registros
            Specie nuevaEspecie = new Specie();
            nuevaEspecie.muestreo = 4;
            nuevaEspecie.nombre = PlayerPrefs.GetString("reptile");
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
        SFXContoller.Instance.PlaySFX(SFXContoller.Instance.encounter);
        registerPopup.SetActive(true);
        StartCoroutine(DownloadImageCoroutine(url));
        Time.timeScale = 0f;
        joystick.SetActive(false);
    }

    public void Awake()
    {
        Instance = this;
        PlayerPrefs.SetInt("Puntaje", 0);
        PlayerPrefs.SetInt("Registros", 0);
        StartCoroutine(getConection());
    }

    public void Start()
    {
        SFXContoller.Instance.PlayMusic(SFXContoller.Instance.Reptiles);
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
