using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ResultsAves : MonoBehaviour
{
    public Text totalText;
    public Text correctText;

    public void returnToLobby()
    {
        SceneManager.LoadScene("Arbol Cam");
    }

    public void updateResults()
    {
        int score = PlayerPrefs.GetInt("avesPuntaje");
        string correct = score.ToString();
        int total = PlayerPrefs.GetInt("avesRegistros");


        correctText.text = correct.ToString();
        totalText.text = total.ToString();

        StartCoroutine(saveScore());

        resetScore();
    }


    public IEnumerator saveScore()
    {
        string JSONurl = "https://localhost:7176/api/Score?idUser=" + Sesion.Instance.getID() + "&score=" + (PlayerPrefs.GetInt("avesPuntaje")) * 100 + "&muestreo=1"; // URL para obtener los datos del libro
                                                                                                                                                                             //string JSONurl = "https://localhost:7176/api/RegistroEspecie"; // URL para obtener los datos del libro

        WWWForm form = new WWWForm(); // Crea un formulario web
        form.AddField("idUser", Sesion.Instance.getID());
        form.AddField("score", (PlayerPrefs.GetInt("avesPuntaje")) * 100);
        form.AddField("muestreo", 1);



        UnityWebRequest request = UnityWebRequest.Post(JSONurl, form); // Crea una solicitud web para obtener los datos

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
            Debug.Log("Score saved");
        }
    }

    public void resetScore()
    {
        PlayerPrefs.SetInt("avesPuntaje", 0);
        PlayerPrefs.SetInt("avesRegistros", 0);
    }

    public void Start()
    {
        updateResults();
    }

}
