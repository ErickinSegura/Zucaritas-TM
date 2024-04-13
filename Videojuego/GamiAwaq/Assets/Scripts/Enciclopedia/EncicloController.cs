using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EncicloController : MonoBehaviour
{

    public enum SpecieType
    {
        Reptiles,
        Mamiferos,
        Rastros,
        Aves
    }

    public SpecieType currentSpecieType = SpecieType.Reptiles;

    public int index = 0;

    public Text text1;
    public Text text2;

    public Image image1;
    public Image image2;

    public List<Specie> reptiles = new List<Specie>
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

    public List<Specie> mamiferos = new List<Specie>
    {
        new Specie { ID = 1, Name = "Perezoso didáctilo de Hoffmann ", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/mam3.png?raw=true"},
        new Specie { ID = 2, Name = "Puercoesín arbícola", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/mam5.png?raw=true"},
        new Specie { ID = 3, Name = "Tepezcuintle", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/mam7.png?raw=true"},
        new Specie { ID = 4, Name = "Agutí centroamericano", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/mam8.png?raw=true"},
        new Specie { ID = 5, Name = "Armadillo de nueve bandas", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/mam6.png?raw=true"},
        new Specie { ID = 6, Name = "Tayra", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/mam10.png?raw=true"},
        new Specie { ID = 7, Name = "Zorro cangrejero", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/mam4.png?raw=true"},
        new Specie { ID = 8, Name = "Coatí de cola anillada", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/mam2.png?raw=true"},
        new Specie { ID = 9, Name = "Puma", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/mam1.png?raw=true"},
        new Specie { ID = 10, Name = "Ocelote", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/mam9.png?raw=true"}

    };

    public List<Specie> rastros = new List<Specie>
    {
        new Specie { ID = 1, Name = "Huella de Tortuga", Image = ""},
        new Specie { ID = 2, Name = "Huella de Cocodrilo", Image = ""},
        new Specie { ID = 3, Name = "Rastro de Serpiente", Image = ""},
        new Specie { ID = 4, Name = "Huella de Lagartija", Image = ""},
        new Specie { ID = 5, Name = "Huella de Camaleon", Image = ""},
        new Specie { ID = 6, Name = "Huella de Ocelote", Image = ""},
        new Specie { ID = 7, Name = "Huella de Zorro", Image = ""},
        new Specie { ID = 8, Name = "Huella de Armadillo", Image = ""},
        new Specie { ID = 9, Name = "Huella de Mapache", Image = ""},
        new Specie { ID = 10, Name = "Huella de Coati", Image = ""}
    };

    public List<Specie> aves = new List<Specie>
    {
        new Specie { ID = 1, Name = "Pájaro cantil", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave1.png?raw=true"},
        new Specie { ID = 2, Name = "Gallineta morada", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave2.png?raw=true"},
        new Specie { ID = 3, Name = "Monja frentiblanca", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave3.png?raw=true"},
        new Specie { ID = 4, Name = "Jacamar Cola Canela", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave4.png?raw=true"},
        new Specie { ID = 5, Name = "Bobo barrado", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave5.png?raw=true"},
        new Specie { ID = 6, Name = "Momoto corona azul", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave6.png?raw=true"},
        new Specie { ID = 7, Name = "Cuclillo pico negro", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave7.png?raw=true"},
        new Specie { ID = 8, Name = "Martí­n pescador amazónico", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave8.png?raw=true"},
        new Specie { ID = 9, Name = "Perdiz", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave9.png?raw=true"},
        new Specie { ID = 10, Name = "Pava cojolita", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/ave10.png?raw=true"}
    };

    void Start()
    {
        UpdateSpeciesDisplay();
    }

    void UpdateSpeciesDisplay()
    {
        List<Specie> currentSpeciesList = GetCurrentSpeciesList();

        if (index < currentSpeciesList.Count)
        {
            text1.text = currentSpeciesList[index].Name;
            StartCoroutine(LoadImage(currentSpeciesList[index].Image, image1));
        }

        if (index + 1 < currentSpeciesList.Count)
        {
            text2.text = currentSpeciesList[index + 1].Name;
            StartCoroutine(LoadImage(currentSpeciesList[index + 1].Image, image2));
        }
    }

    List<Specie> GetCurrentSpeciesList()
    {
        switch (currentSpecieType)
        {
            case SpecieType.Reptiles:
                return reptiles;
            case SpecieType.Mamiferos:
                return mamiferos;
            case SpecieType.Rastros:
                return rastros;
            case SpecieType.Aves:
                    return aves;
            default:
                return reptiles;
        }
    }

    public void NextSpecies()
    {
        List<Specie> currentSpeciesList = GetCurrentSpeciesList();

        if (index + 2 < currentSpeciesList.Count)
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

    public void ChangeSpecieType(int type)
    {
        currentSpecieType = (SpecieType)type;
        index = 0; // Reset index when changing specie type
        UpdateSpeciesDisplay();
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

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Lobby");
    }

    public class Specie
    {
        public int ID;
        public string Name;
        public string Image;
    }
}
