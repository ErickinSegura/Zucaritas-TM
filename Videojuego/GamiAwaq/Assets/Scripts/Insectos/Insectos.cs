using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Insectos : MonoBehaviour
{
    public bool isDay;
    public void returnToLobby()
    {
        if (isDay)
        {
            SceneManager.LoadScene("Lobby");
        }
        else
        {
            SceneManager.LoadScene("Lobby Noche");
        }
    }
}
