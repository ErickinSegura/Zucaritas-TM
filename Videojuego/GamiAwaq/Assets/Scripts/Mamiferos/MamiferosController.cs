using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json;


public enum RarityLevel
{
    Common,
    Uncommon,
    Rare,
    VeryRare
}

[System.Serializable]
public class AnimalInfo
{
    public string name;
    public Sprite image;
    public RarityLevel rarity;
}

public class MamiferosController : MonoBehaviour
{
    public AnimalInfo[] animals;
    public Image animalImageUI;
    public Text animalNameUI;
    public Button leftArrowButton;
    public Button rightArrowButton;
    public GameObject popupError;

    private int numberOfAnimalsToShow;
    private List<AnimalInfo> selectedAnimals = new List<AnimalInfo>();

    public class Specie
    {
        public int muestreo;
        public string nombre;
        public string url;
        public int rareza;
    }

    List<Specie> especies = new List<Specie>();
    List<Specie> registros = new List<Specie>();

    public IEnumerator getConection()
    {
        string JSONurl = "https://localhost:7176/api/RegistroEspecie?id=" + Sesion.Instance.getID(); // URL para obtener los datos del libro
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
                foreach (Specie especie in especies)
                {
                    if (especie.muestreo == 6)
                    {
                        registros.Add(especie);
                    }
                }

                foreach (Specie especie in registros)
                {
                    Debug.Log(especie.nombre);
                }

            }
        }
    }

    private int currentAnimalIndex = 0;
    private List<Color> rarityColors = new List<Color>{
        Color.black,     // Common
        new Color(0f, 0.5f, 0f),      // Uncommon
        Color.blue,      // Rare
        Color.magenta    // VeryRare
    };

    private List<float> rarityProbabilities = new List<float>{
        0.6f,   // Common
        0.3f,   // Uncommon
        0.08f,  // Rare
        0.02f   // VeryRare
    };

    void Start()
    {
        if (PlayerPrefs.GetString("currentDirection") == "N")
        {
            numberOfAnimalsToShow = Random.Range(3, 5);

            PlayerPrefs.SetInt("NumberOfAnimalsToShow", numberOfAnimalsToShow);
            PlayerPrefs.Save();
            SetRandomAnimalUI();
            UpdateArrowButtons();
            //rightArrowButton.onClick.AddListener(ChangeAnimal);
            if (PlayerPrefs.HasKey("LastAnimalIndex"))
            {
                int lastIndex = PlayerPrefs.GetInt("LastAnimalIndex");
                // Usar lastIndex para cargar la última imagen mostrada
            }
        }
        else
        {
            popupError.SetActive(true);
        }
    }

    void SetRandomAnimalUI()
    {
        selectedAnimals.Clear();

        for (int i = 0; i < numberOfAnimalsToShow; i++)
        {
            RarityLevel selectedRarity = GetRandomRarityLevel();

            List<AnimalInfo> filteredAnimals = new List<AnimalInfo>();

            foreach (AnimalInfo animal in animals)
            {
                if (animal.rarity == selectedRarity)
                {
                    filteredAnimals.Add(animal);
                    
                }
            }


            AnimalInfo randomAnimal = filteredAnimals[Random.Range(0, filteredAnimals.Count)];
            Debug.Log("Animal: " + randomAnimal.name + " Rarity: " + randomAnimal.rarity);
            selectedAnimals.Add(randomAnimal);

            if (i == 0)
            {
                animalImageUI.sprite = randomAnimal.image;
                animalNameUI.text = randomAnimal.name;
                animalNameUI.color = rarityColors[(int)randomAnimal.rarity];
            }
        }
    }

    RarityLevel GetRandomRarityLevel()
    {
        float randomValue = Random.value;
        float cumulativeProbability = 0f;

        for (int i = 0; i < rarityProbabilities.Count; i++)
        {
            cumulativeProbability += rarityProbabilities[i];
            if (randomValue <= cumulativeProbability)
            {
                return (RarityLevel)i;
            }
        }

        // Fallback to the most common rarity level
        return RarityLevel.Common;
    }


    void UpdateArrowButtons()
    {
        leftArrowButton.gameObject.SetActive(currentAnimalIndex > 0);
        rightArrowButton.gameObject.SetActive(currentAnimalIndex < selectedAnimals.Count - 1);
    }

    void NextAnimal()
    {
        currentAnimalIndex = (currentAnimalIndex + 1);
        Debug.Log("Current Animal Index: " + currentAnimalIndex);
        UpdateAnimalUI();
    }

    void PreviousAnimal()
    {
        currentAnimalIndex = (currentAnimalIndex - 1);
        Debug.Log("Current Animal Index: " + currentAnimalIndex);
        UpdateAnimalUI();
    }

    void UpdateAnimalUI()
    {
        AnimalInfo currentAnimal = selectedAnimals[currentAnimalIndex];
        animalImageUI.sprite = currentAnimal.image;
        animalNameUI.text = currentAnimal.name;
        animalNameUI.color = rarityColors[(int)currentAnimal.rarity];
        UpdateArrowButtons();
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
