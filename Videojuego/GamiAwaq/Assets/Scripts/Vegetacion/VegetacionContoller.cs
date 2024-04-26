using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class VegetacionContoller : MonoBehaviour
{
    static public VegetacionContoller Instance;
    public GameObject finishPopup;
    public GameObject registerPopup;

    public Image newImage;

    public Text incoText;

    Texture2D image;
    Sprite newSprite;


    public void finish()
    {
        SFXContoller.Instance.PlayClick();
        finishPopup.SetActive(true);
    }

    public void exit()
    {
        SFXContoller.Instance.PlayClick();
        finishPopup.SetActive(false);
    }

    public void continueToScore()
    {
        SceneManager.LoadScene("FinalReptile");
    }



    public void enter()
    {
        SFXContoller.Instance.PlayClick();
        PlayerPrefs.SetInt("Registros", PlayerPrefs.GetInt("Registros") + 1);
        string val = Dropdown.Instance.GetDropdownValue();

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
        Time.timeScale = 1f;
    }

    IEnumerator hideText()
    {
        yield return new WaitForSeconds(2);
        incoText.text = "";

    }

    public void activatePopup(string url)
    {
        SFXContoller.Instance.PlayClick();
        registerPopup.SetActive(true);
        StartCoroutine(DownloadImageCoroutine(url));
        Time.timeScale = 0f;
    }

    public void Awake()
    {

        Instance = this;
        PlayerPrefs.SetInt("Puntaje", 0);
        PlayerPrefs.SetInt("Registros", 0);

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
}
