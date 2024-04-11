using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ReptilesController : MonoBehaviour
{

    static public ReptilesController Instance;
    public GameObject parentObject;
    public GameObject registerPopup;
    public Image newImage;


    Texture2D image;
    Sprite newSprite;

    public string dropdownParentTag = "DropdownParent";
    private Dropdown dropdown;



    public void returnToLobby()
    {
        SceneManager.LoadScene("Lobby Noche");
    }

    public void enter()
    {
        registerPopup.SetActive(false);
    }

    public void activatePopup(string url)
    {
        registerPopup.SetActive(true);
        StartCoroutine(DownloadImageCoroutine(url));
    }

    public void Awake()
    {
        Instance = this;
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
