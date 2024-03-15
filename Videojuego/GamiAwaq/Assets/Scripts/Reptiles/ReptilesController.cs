using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReptilesController : MonoBehaviour
{
    public void returnToLobby()
    {
        // This will load the scene named "MainMenu"
        SceneManager.LoadScene("Lobby Noche");
    }
}
