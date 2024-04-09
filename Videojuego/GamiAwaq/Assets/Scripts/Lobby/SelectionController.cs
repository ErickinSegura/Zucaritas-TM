using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionController : MonoBehaviour
{
    public string sceneName1;
    public string sceneName2;
    public GameObject popup;

    public void closePopup()
    {
        popup.SetActive(false);
    }   

    public void loadScene1()
    {
        SceneManager.LoadScene(sceneName1);
    }

    public void loadScene2()
    {
        SceneManager.LoadScene(sceneName2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            popup.SetActive(true);
        }
    }



}
