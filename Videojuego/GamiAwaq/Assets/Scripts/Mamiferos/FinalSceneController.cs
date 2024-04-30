using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;


public class FinalSceneController : MonoBehaviour
{
    public Text numberOfAnimalsText;

    void Start()
    {
        // Obtener el valor de numberOfAnimalsToShow guardado en PlayerPrefs
        int numberOfAnimalsToShow = PlayerPrefs.GetInt("NumberOfAnimalsToShow", -1);

        // Verificar si se pudo obtener el valor correctamente
        if (numberOfAnimalsToShow != -1)
        {
            // Mostrar el valor en el objeto de texto
            numberOfAnimalsText.text = numberOfAnimalsToShow.ToString();
            StartCoroutine(saveScore());
        }
        else
        {
            // Mostrar un mensaje de error si no se pudo obtener el valor
            numberOfAnimalsText.text = "Error: No se pudo obtener el número de animales";
        }

    }

    public IEnumerator saveScore()
    {
        string JSONurl = "https://localhost:7176/api/Score?idUser=" + Sesion.Instance.getID() + "&score=" + (PlayerPrefs.GetInt("NumberOfAnimalsToShow", -1)) * 100 + "&muestreo=6"; // URL para obtener los datos del libro
                                                                                                                                                                   //string JSONurl = "https://localhost:7176/api/RegistroEspecie"; // URL para obtener los datos del libro

        WWWForm form = new WWWForm(); // Crea un formulario web
        form.AddField("idUser", Sesion.Instance.getID());
        form.AddField("score", (PlayerPrefs.GetInt("NumberOfAnimalsToShow", -1)) * 100);
        form.AddField("muestreo", 6);



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
            Debug.Log("Score saved");
        }
    }
}
