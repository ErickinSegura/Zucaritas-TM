using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncicloController : MonoBehaviour
{
    public void returnToMenu()
    {
        SceneManager.LoadScene("Lobby");
    }
}
