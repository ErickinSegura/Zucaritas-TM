using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenu : MonoBehaviour
{
    public TMP_InputField inputField;

    public void Awake()
    {
        PlayerPrefs.SetString("username", "");
        PlayerPrefs.SetString("password", "");
    }

    public void saveUser()
    {
        PlayerPrefs.SetString("username", inputField.text);
    }

    public void savePass()
    {
        PlayerPrefs.SetString("password", inputField.text);
    }

    public void PlayGame()
    {
        Sesion.Instance.login();
    }
}
