using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MamiferosController : MonoBehaviour
{
    public Sprite[] animalImages;
    public string[] animalNames;
    public Image animalImageUI;
    public Text animalNameUI;
    public Button leftArrowButton;
    public Button rightArrowButton;

    private List<int> selectedIndices = new List<int>();
    private int currentImageIndex = 0;
    private int numberOfAnimalsToShow = 3; // N�mero de animales a mostrar

    void Start()
    {
        SetRandomAnimalUI();
        UpdateArrowButtons();

        // Asigna la funci�n ChangeImage() al evento de clic de la flecha derecha
        rightArrowButton.onClick.AddListener(ChangeImage);
        if (PlayerPrefs.HasKey("LastAnimalIndex"))
        {
            int lastIndex = PlayerPrefs.GetInt("LastAnimalIndex");
            // Usar lastIndex para cargar la �ltima imagen mostrada
        }
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene("RecapMoni");
    }

    void SetRandomAnimalUI()
    {
        // Limpiar la lista de �ndices seleccionados
        selectedIndices.Clear();

        // Seleccionar un n�mero espec�fico de �ndices de animales al azar
        for (int i = 0; i < numberOfAnimalsToShow; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, animalImages.Length);
            } while (selectedIndices.Contains(randomIndex)); // Evitar duplicados

            selectedIndices.Add(randomIndex);

            // Mostrar la imagen y el nombre del animal en la primera iteraci�n
            if (i == 0)
            {
                animalImageUI.sprite = animalImages[randomIndex];
                animalNameUI.text = animalNames[randomIndex];
            }
        }
    }

    void UpdateArrowButtons()
    {
        // Establecer la cantidad de animales generados seg�n la cantidad seleccionada
        int numberOfAnimals = selectedIndices.Count;
        leftArrowButton.gameObject.SetActive(numberOfAnimals >= 2);
        rightArrowButton.gameObject.SetActive(numberOfAnimals >= 2);
    }

    void ChangeImage()
    {
        // Avanzar al siguiente �ndice de imagen circularmente
        currentImageIndex = (currentImageIndex + 1) % numberOfAnimalsToShow;

        // Obtener el �ndice de animal correspondiente al �ndice actual
        int currentAnimalIndex = selectedIndices[currentImageIndex];

        // Mostrar la nueva imagen y su nombre correspondiente
        animalImageUI.sprite = animalImages[currentAnimalIndex];
        animalNameUI.text = animalNames[currentAnimalIndex];

        PlayerPrefs.SetInt("LastAnimalIndex", currentAnimalIndex);
        PlayerPrefs.Save();
    }
}
