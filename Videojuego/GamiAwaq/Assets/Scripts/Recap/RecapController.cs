using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecapController : MonoBehaviour
{
    public void returnToMenu()
    {
        SceneManager.LoadScene("Lobby");
    }   
}
