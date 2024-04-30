using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public void Start()
    {
        SFXContoller.Instance.PlayMusic(SFXContoller.Instance.Horizon);
    }

    public void lobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
