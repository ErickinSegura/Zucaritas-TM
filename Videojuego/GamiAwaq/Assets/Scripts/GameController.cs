using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public GameObject popupError;

    public GameObject joystick;

    private void Awake()
    {
        Instance = this;;
    }
    public void openPopupError()
    {
        popupError.SetActive(true);
    }

    public void closePopupError()
    {
        popupError.SetActive(false);
    }

    public void closeJoystick()
    {
        joystick.SetActive(false);
    }

    public void openJoystick()
    {
        joystick.SetActive(true);
    }

}
