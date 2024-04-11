using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MamiferosController : MonoBehaviour
{
    public void returnToMenu()
    {
        SceneManager.LoadScene("RecapMoni");
    }
}
