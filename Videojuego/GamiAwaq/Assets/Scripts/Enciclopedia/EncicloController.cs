using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class EncicloController : MonoBehaviour
{
    public class Specie
    {
        public int muestreo;
        public string nombre;
        public string url;
        public int rareza;
    }

    public enum SpecieType
    {
        Reptiles,
        Mamiferos,
        Rastros,
        Aves
    }

    public SpecieType currentSpecieType = SpecieType.Reptiles;

    public int index = 1;

    public Text text1;
    public Text text2;

    public Text indice1;
    public Text indice2;

    public Image image1;
    public Image image2;

    List<Specie> especies = new List<Specie>();

    List<Specie> mamiferos = new List<Specie>();
    List<Specie> reptiles = new List<Specie>();
    List<Specie> rastros = new List<Specie>();
    List<Specie> aves = new List<Specie>();
    List<Specie> insectos = new List<Specie>();
    List<Specie> vegetacion = new List<Specie>();

    private List<Color> rarityColors = new List<Color>{
        Color.black,     // Common
        Color.black,     // Common
        new Color(0f, 0.5f, 0f),      // Uncommon
        Color.blue,      // Rare
        Color.magenta    // VeryRare
    };

    // Metodo para obtener las especies registradas por el usuario

    IEnumerator getID()
    {
        string JSONurl = "https://localhost:7176/api/RegistroEspecie?id="+Sesion.Instance.getID(); // URL para obtener los datos del libro
        UnityWebRequest request = UnityWebRequest.Get(JSONurl); // Crea una solicitud web para obtener los datos
        request.useHttpContinue = true; // Configura para usar la continuación HTTP

        var cert = new ForceAcceptAll(); // Crea una instancia de la clase para aceptar todos los certificados SSL
        request.certificateHandler = cert; // Asigna el manejador de certificados a la solicitud
        cert?.Dispose(); // Libera la instancia de la clase ForceAceptAll

        yield return request.SendWebRequest(); // Envía la solicitud web y espera la respuesta

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error Downloading: " + request.error);
        }
        else
        {
            if ((request.downloadHandler.text) == "")
            {
                GameController.Instance.openPopupError();
                Debug.Log("No hay registros");
            }
            else
            {
                especies = JsonConvert.DeserializeObject<List<Specie>>(request.downloadHandler.text);
                Debug.Log(request.downloadHandler.text);
                createListMuestreo();
            }
        }
    }

    public void createListMuestreo()
    {
        Debug.Log("Creando listas de muestreo");
        foreach (Specie especie in especies)
        {
            switch(especie.muestreo)
            {
                case 1:
                    aves.Add(especie);
                    break;
                case 2:
                    rastros.Add(especie);
                    break;
                case 3:
                    insectos.Add(especie);
                    break;
                case 4:
                    reptiles.Add(especie);
                    break;
                case 5:
                    vegetacion.Add(especie);
                    break;
                case 6:
                    mamiferos.Add(especie);
                    break;
            }
        }

        // Llenar cada lista con placeholders si no hay elementos, deben de haber 10 elementos en cada lista
        while (aves.Count < 10)
        {
            Specie placeholder = new Specie();
            placeholder.nombre = "Bloqueado";
            placeholder.url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEY_6ggdUcpy80ZvVLFwkLyyi2nD8Eze5t81oruVGXBA&s";
            aves.Add(placeholder);
        }

        while (rastros.Count < 10)
        {
            Specie placeholder = new Specie();
            placeholder.nombre = "Bloqueado";
            placeholder.url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEY_6ggdUcpy80ZvVLFwkLyyi2nD8Eze5t81oruVGXBA&s";
            rastros.Add(placeholder);
        }

        while (insectos.Count < 10)
        {
            Specie placeholder = new Specie();
            placeholder.nombre = "Bloqueado";
            placeholder.url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEY_6ggdUcpy80ZvVLFwkLyyi2nD8Eze5t81oruVGXBA&s";
            insectos.Add(placeholder);
        }

        while (reptiles.Count < 10)
        {
            Specie placeholder = new Specie();
            placeholder.nombre = "Bloqueado";
            placeholder.url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEY_6ggdUcpy80ZvVLFwkLyyi2nD8Eze5t81oruVGXBA&s";
            reptiles.Add(placeholder);
        }

        while (vegetacion.Count < 10)
        {
            Specie placeholder = new Specie();
            placeholder.nombre = "Bloqueado";
            placeholder.url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEY_6ggdUcpy80ZvVLFwkLyyi2nD8Eze5t81oruVGXBA&s";
            vegetacion.Add(placeholder);
        }

        while (mamiferos.Count < 10)
        {
            Specie placeholder = new Specie();
            placeholder.nombre = "Bloqueado";
            placeholder.url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEY_6ggdUcpy80ZvVLFwkLyyi2nD8Eze5t81oruVGXBA&s";
            mamiferos.Add(placeholder);
        }

        UpdateSpeciesDisplay();

    }

    public void Awake()
    {
        StartCoroutine(getID());
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

    
    void Start()
    {
        UpdateSpeciesDisplay();
    }

    void UpdateSpeciesDisplay()
    {
        List<Specie> currentSpeciesList = GetCurrentSpeciesList();

        if (index < currentSpeciesList.Count)
        {
            text1.text = currentSpeciesList[index].nombre;
            text1.color = rarityColors[currentSpeciesList[index].rareza];
            Debug.Log("Rareza: " + currentSpeciesList[index].rareza);
            indice1.text = (index + 1).ToString();
            StartCoroutine(LoadImage(currentSpeciesList[index].url, image1));

        }

        if (index + 1 < currentSpeciesList.Count)
        {
            text2.text = currentSpeciesList[index + 1].nombre;
            indice2.text = (index + 2).ToString();
            text2.color = rarityColors[currentSpeciesList[index + 1].rareza];
            Debug.Log("Rareza: " + currentSpeciesList[index + 1].rareza);

            StartCoroutine(LoadImage(currentSpeciesList[index + 1].url, image2));
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
        SFXContoller.Instance.PlayClick();
        List<Specie> currentSpeciesList = GetCurrentSpeciesList();

        if (index + 2 < currentSpeciesList.Count)
        {
            index += 2;
            UpdateSpeciesDisplay();
        }
    }

    public void PreviousSpecies()
    {
        SFXContoller.Instance.PlayClick();
        if (index - 2 >= 0)
        {
            index -= 2;
            UpdateSpeciesDisplay();
        }
    }

    public void ChangeSpecieType(int type)
    {
        currentSpecieType = (SpecieType)type;
        SFXContoller.Instance.PlayClick();
        index = 0; // Reset index when changing specie type
        UpdateSpeciesDisplay();
    }



    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Lobby");
    }

}
