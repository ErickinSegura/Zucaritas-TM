using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RastrosController : MonoBehaviour
{
    static public RastrosController Instance;
    public GameObject finishPopup;
    public GameObject registerPopup;

    public Text timerText;

    public int time;

    public Image newImage;

    public Text incoText;

    Texture2D image;
    Sprite newSprite;


    IEnumerator finish()
    {
        SFXContoller.Instance.PlayClick();
        finishPopup.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("FinalRastros");

    }

    public void exit()
    {
        SFXContoller.Instance.PlayClick();
        finishPopup.SetActive(false);
    }

    public void continueToScore()
    {
        SFXContoller.Instance.PlayClick();
        SceneManager.LoadScene("FinalRastros");
    }

    public void enter()
    {
        SFXContoller.Instance.PlayClick();
        PlayerPrefs.SetInt("Registros", PlayerPrefs.GetInt("Registros") + 1);
        string val = DropdownRastros.Instance.GetDropdownValue();

        string correct = PlayerPrefs.GetString("reptile");

        Debug.Log("El valor del dropdown " + val);

        if (val == correct)
        {
            incoText.text = "Correcto";
            incoText.color = Color.green;
            Debug.Log("Correcto");
            PlayerPrefs.SetInt("Puntaje", PlayerPrefs.GetInt("Puntaje") + 1);
            StartCoroutine(hideText());
        }
        else
        {
            incoText.text = "Incorrecto";
            incoText.color = Color.red;
            Debug.Log("Incorrecto");
            StartCoroutine(hideText());
        }
        registerPopup.SetActive(false);
    }

    IEnumerator hideText()
    {
        yield return new WaitForSeconds(2);
        incoText.text = "";

    }

    public void activatePopup(string url)
    {
        registerPopup.SetActive(true);
        StartCoroutine(DownloadImageCoroutine(url));
    }

    public void Awake()
    {
        Instance = this;
        PlayerPrefs.SetInt("Puntaje", 0);
        PlayerPrefs.SetInt("Registros", 0);
        StartCoroutine(updatetimer());

    }

    IEnumerator DownloadImageCoroutine(string MediaUrl)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error Downloading: " + request.error);
            }
            else
            {
                image = DownloadHandlerTexture.GetContent(request);
                newSprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f), 100.0f);
                newImage.sprite = newSprite;
            }
        }
    }

    IEnumerator updatetimer()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time -= 1;
            updateTimeText(time.ToString());
        }
        StartCoroutine(finish());
    }

    public void updateTimeText(string time)
    {
        timerText.text = "Tiempo Restante: " + time;
    }



}
