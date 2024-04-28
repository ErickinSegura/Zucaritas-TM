using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VegetacionResults : MonoBehaviour
{
    public Text totalText;
    public Text correctText;

    public void returnToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void updateResults()
    {
        int score = PlayerPrefs.GetInt("PuntajeVegetacion");
        string correct = score.ToString();
        int total = PlayerPrefs.GetInt("RegistrosVegetacion");

        correctText.text = correct.ToString() + "/" + total.ToString();
        totalText.text = total.ToString() + "/15";

        resetScore();
    }

    public void resetScore()
    {
        PlayerPrefs.SetInt("Puntaje", 0);
        PlayerPrefs.SetInt("Registros", 0);
    }

    public void Start()
    {
        updateResults();
    }
}
