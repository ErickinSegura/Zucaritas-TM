using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Sesion : MonoBehaviour
{
    public static Sesion Instance;

    public int id;

    public void login()
    {
        string user = PlayerPrefs.GetString("username");
        string pass = PlayerPrefs.GetString("password");
        StartCoroutine(getID(user,pass));
    }

    public GameObject popup;

    public Text textPopup;  

    public int getID()
    {
        return id;
    }

    public void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    IEnumerator getID(string user, string pass)
    {
        popup.SetActive(true);
        string JSONurl = "https://localhost:7176/api/User?username="+user+"&password="+pass; // URL para obtener los datos del libro
        UnityWebRequest request = UnityWebRequest.Get(JSONurl); // Crea una solicitud web para obtener los datos
        request.useHttpContinue = true; // Configura para usar la continuación HTTP

        var cert = new ForceAcceptAll(); // Crea una instancia de la clase para aceptar todos los certificados SSL
        request.certificateHandler = cert; // Asigna el manejador de certificados a la solicitud
        cert?.Dispose(); // Libera la instancia de la clase ForceAceptAll

        yield return request.SendWebRequest(); // Envía la solicitud web y espera la respuesta

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error Downloading: " + request.error);
            textPopup.text = "Usuario o contraseña incorrectos";
            yield return new WaitForSeconds(2);
            textPopup.text = "Iniciando sesión";
            popup.SetActive(false);
            Debug.Log("Usuario o contraseña incorrectos");

        }
        else
        {
            if (int.Parse(request.downloadHandler.text) == -1)
            {
                textPopup.text = "Usuario o contraseña incorrectos";
                yield return new WaitForSeconds(2);
                textPopup.text = "Iniciando sesión";
                popup.SetActive(false);
                Debug.Log("Usuario o contraseña incorrectos");
            }
            else
            {
                id = int.Parse(request.downloadHandler.text);
                Debug.Log("ID: " + id);
                StartCoroutine(checkTutorial());
                
            }
            
        }
    }


    public IEnumerator checkTutorial()
    {
        string JSONurl = "https://localhost:7176/api/Tutorial?id=" + 17;//Sesion.Instance.getID(); // URL para obtener los datos del libro
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

            if (int.Parse(request.downloadHandler.text) == 1)
            {
                SFXContoller.Instance.Stop();
                SceneManager.LoadScene("Lobby");
            }
            else
            {
                SFXContoller.Instance.Stop();
                SceneManager.LoadScene("Tutorial");
            }

        }
    }

    



}
