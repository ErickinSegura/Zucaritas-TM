using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Results : MonoBehaviour
{
    public Text totalText;
    public Text correctText;

    public void returnToLobby()
    {
        SceneManager.LoadScene("Lobby Noche");
    }

    public void updateResults()
    {
        int score = PlayerPrefs.GetInt("Puntaje");
        string correct = score.ToString();
        int total = PlayerPrefs.GetInt("Registros");

        correctText.text = correct.ToString();
        totalText.text = total.ToString();

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
