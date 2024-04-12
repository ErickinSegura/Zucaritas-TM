using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EncicloController : MonoBehaviour
{
    public class Specie
    {
        public int ID;
        public string Name;
        public string Image;
    }

    public int index = 0;

    public Text text1;
    public Text text2;

    public Image image1;
    public Image image2;

    public List<Specie> especies = new List<Specie>
    {
        new Specie { ID = 1, Name = "Caimán Aguja", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep8.png?raw=true"},
        new Specie { ID = 2, Name = "Caimán Llanero", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep4.png?raw=true"},
        new Specie { ID = 3, Name = "Serpiente Sabanera", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep7.png?raw=true"},
        new Specie { ID = 4, Name = "Serpiente Terciopelo", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep3.png?raw=true"},
        new Specie { ID = 5, Name = "Serpiente San Andrés", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep5.png?raw=true"},
        new Specie { ID = 6, Name = "Tortuga Ciénega Col", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep9.png?raw=true"},
        new Specie { ID = 7, Name = "Tortuga Morrocoy", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep10.png?raw=true"},
        new Specie { ID = 8, Name = "Camaleón Cundimamarca", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep2.png?raw=true"},
        new Specie { ID = 9, Name = "Anolis Calima", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep6.png?raw=true"},
        new Specie { ID = 10, Name = "Lagartija Bogotá", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep1.png?raw=true"}
    };

    void Start()
    {
        UpdateSpeciesDisplay();
    }

    void UpdateSpeciesDisplay()
    {
        if (index < especies.Count)
        {
            text1.text = especies[index].Name;
            StartCoroutine(LoadImage(especies[index].Image, image1));
        }

        if (index + 1 < especies.Count)
        {
            text2.text = especies[index + 1].Name;
            StartCoroutine(LoadImage(especies[index + 1].Image, image2));
        }
    }

    IEnumerator LoadImage(string url, Image targetImage)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            targetImage.sprite = sprite;
        }
        else
        {
            Debug.Log("Error downloading image: " + www.error);
        }
    }

    public void NextSpecies()
    {
        if (index + 2 < especies.Count)
        {
            index += 2;
            UpdateSpeciesDisplay();
        }
    }

    public void PreviousSpecies()
    {
        if (index - 2 >= 0)
        {
            index -= 2;
            UpdateSpeciesDisplay();
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Lobby");
    }
}
