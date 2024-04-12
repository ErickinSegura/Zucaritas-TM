using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public GameObject popupError;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    public void openPopupError()
    {
        popupError.SetActive(true);
    }

    public void closePopupError()
    {
        popupError.SetActive(false);
    }

}
