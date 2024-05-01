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
    public int id;
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
    private List<AnimalInfo> shownAnimals = new List<AnimalInfo>();

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
        Debug.Log("Llamando a la conexión");
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
                Debug.Log("Hay registros");
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
        SetRandomAnimalUI();
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
            SFXContoller.Instance.PlayMusic(SFXContoller.Instance.Mamiferos);
            StartCoroutine(getConection());
            

            PlayerPrefs.SetInt("NumberOfAnimalsToShow", numberOfAnimalsToShow);
            PlayerPrefs.Save();

            UpdateArrowButtons();
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
        Debug.Log("Registros: " + registros.Count);
        Debug.Log("Cargando animales random");
        for (int i = 0; i < numberOfAnimalsToShow; i++)
        {
            RarityLevel selectedRarity = GetRandomRarityLevel();
            List<AnimalInfo> filteredAnimals = new List<AnimalInfo>();

            foreach (AnimalInfo animal in animals)
            {
                if (animal.rarity == selectedRarity && !shownAnimals.Contains(animal)) // Verifica si el animal ya ha sido mostrado
                {
                    filteredAnimals.Add(animal);
                }
            }

            if (filteredAnimals.Count == 0)
            {
                Debug.Log("Todos los animales de esta rareza ya han sido mostrados.");
                return;
            }

            AnimalInfo randomAnimal = filteredAnimals[Random.Range(0, filteredAnimals.Count)];
            shownAnimals.Add(randomAnimal);


            PlayerPrefs.SetString("mamifero", randomAnimal.name);
            PlayerPrefs.SetInt("mamiferoID", randomAnimal.id);

            bool especieRegistrada = false;
            foreach (Specie especie in registros)
            {
                Debug.Log("Comparación Random: " + randomAnimal.name + " DB:" + especie.nombre);
                if (especie.nombre == randomAnimal.name)
                {
                    especieRegistrada = true;
                    Debug.Log("Ya esta registrado");
                    break;
                }
            }

            if (!especieRegistrada)
            {
                StartCoroutine(registrarEspecie());
                Debug.Log("Registrando");
            }

            // Configuración de la interfaz de usuario
            if (i == 0)
            {
                animalImageUI.sprite = randomAnimal.image;
                animalNameUI.text = randomAnimal.name;
                animalNameUI.color = rarityColors[(int)randomAnimal.rarity];
            }
        }
    }




    IEnumerator registrarEspecie()
    {
        string JSONurl = "https://localhost:7176/api/RegistroEspecie?idUser=" + Sesion.Instance.getID() + "&idEspecie=" + PlayerPrefs.GetInt("mamiferoID") + "&idMuestreo=6"; // URL para obtener los datos del libro
                                                                                                                                                                            //string JSONurl = "https://localhost:7176/api/RegistroEspecie"; // URL para obtener los datos del libro

        WWWForm form = new WWWForm(); // Crea un formulario web
        form.AddField("idUser", Sesion.Instance.getID()); // Agrega un campo al formulario
        form.AddField("idEspecie", PlayerPrefs.GetInt("mamiferoID")); // Agrega un campo al formulario
        form.AddField("idMuestreo", 6); // Agrega un campo al formulario



        UnityWebRequest request = UnityWebRequest.Post(JSONurl, form); // Crea una solicitud web para obtener los datos

        Debug.Log(request);

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
            Debug.Log("Registro exitoso");
        }

        // Si el registro es exitoso, agrega la especie registrada a la lista de registros
        Specie nuevaEspecie = new Specie();
        nuevaEspecie.muestreo = 6;
        nuevaEspecie.nombre = PlayerPrefs.GetString("mamifero");
        nuevaEspecie.url = ""; // Asigna la URL adecuada si es necesario
        nuevaEspecie.rareza = 0; // Asigna la rareza adecuada si es necesario

        registros.Add(nuevaEspecie);
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
        Debug.Log("Current Animal Index: " + currentAnimalIndex);
        leftArrowButton.gameObject.SetActive(currentAnimalIndex > 0);
        rightArrowButton.gameObject.SetActive(currentAnimalIndex < shownAnimals.Count - 1 || currentAnimalIndex == 0);

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
        AnimalInfo currentAnimal = shownAnimals[currentAnimalIndex];
        animalImageUI.sprite = currentAnimal.image;
        animalNameUI.text = currentAnimal.name;
        animalNameUI.color = rarityColors[(int)currentAnimal.rarity];
        UpdateArrowButtons();
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Awake()
    {
        PlayerPrefs.SetInt("Puntaje", 0);
        PlayerPrefs.SetInt("Registros", 0);

    }
}
