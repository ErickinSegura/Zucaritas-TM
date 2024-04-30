using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.Networking;


public class TutorialController : MonoBehaviour
{

    public VideoPlayer videoPlayer;

    public GameObject button;

    public void Start()
    {
        videoPlayer.loopPointReached += EndReached;
        StartCoroutine(checkTutorial());
    }

    public void EndReached(VideoPlayer vp)
    {
        SceneManager.LoadScene("Lobby");
    }

    public void skip()
    {
        SceneManager.LoadScene("Enciclopedia");
    }

    public IEnumerator checkTutorial()
    {
        string JSONurl = "https://localhost:7176/api/Tutorial?id=" + Sesion.Instance.getID();//Sesion.Instance.getID(); // URL para obtener los datos del libro
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
            if (int.Parse(request.downloadHandler.text) == 0)
            {
                StartCoroutine(AddTutorial());
            }
            else
            {
                Debug.Log("Tutorial ya visto");
                StartCoroutine(showButton());
            }

        }
    }

    
    public IEnumerator AddTutorial()
    {
        string JSONurl = "https://localhost:7176/api/Tutorial?idUser=" + Sesion.Instance.getID(); // URL para obtener los datos del libro
                                                                                                                                                                            //string JSONurl = "https://localhost:7176/api/RegistroEspecie"; // URL para obtener los datos del libro

        WWWForm form = new WWWForm(); // Crea un formulario web
        form.AddField("idUser", Sesion.Instance.getID()); // Agrega un campo al formulario

        UnityWebRequest request = UnityWebRequest.Post(JSONurl, form); // Crea una solicitud web para obtener los datos

        Debug.Log(request);

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
            Debug.Log("Registro exitoso");
        }
    }

    public IEnumerator showButton()
    {
        yield return new WaitForSeconds(5);
        button.SetActive(true);
    }
}
