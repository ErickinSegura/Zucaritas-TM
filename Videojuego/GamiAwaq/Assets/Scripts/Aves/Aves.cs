using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Aves : MonoBehaviour
{
    public void returnToMenu()
    {
        SceneManager.LoadScene("RecapMoni");
    }
}
