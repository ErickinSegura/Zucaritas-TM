using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SFXContoller.Instance.PlayClick();
        SFXContoller.Instance.Stop();
        SFXContoller.Instance.PlayPajaros();
        SceneManager.LoadScene("Lobby");
    }
}
