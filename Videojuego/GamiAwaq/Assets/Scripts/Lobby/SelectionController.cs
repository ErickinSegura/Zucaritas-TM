using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionController : MonoBehaviour
{
    public string sceneName1;
    public string sceneName2;
    public GameObject popup;


    public void loadScene1()
    {
        if (PlayerPrefs.GetString("Scene") != sceneName1)
        {
            SceneManager.LoadScene(sceneName1);   
            PlayerPrefs.SetString("Scene", sceneName1);
        }
        else
        {
            openPopupError();
        }
        
    }

    public void loadScene2()
    {
        if (PlayerPrefs.GetString("Scene") != sceneName2)
        {
            SceneManager.LoadScene(sceneName2);
            PlayerPrefs.SetString("Scene", sceneName2);
        }
        else
        {
            openPopupError();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            popup.SetActive(true);
            GameController.Instance.closeJoystick();
        }
    }

    public void openPopupError()
    {
        popup.SetActive(false);
        GameController.Instance.openPopupError();
        GameController.Instance.openJoystick();

    }

    public void closePopupError()
    {
        GameController.Instance.closePopupError();
    }



}
