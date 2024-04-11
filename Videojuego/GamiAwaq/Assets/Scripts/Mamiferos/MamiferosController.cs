using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MamiferosController : MonoBehaviour
{
    public Sprite[] allAnimalImages; // Lista completa de imágenes de animales
    public string[] allAnimalNames; // Lista completa de nombres de animales
    public Image[] animalImageUIs; // Imágenes de animales en la interfaz de usuario
    public Text[] animalNameUIs; // Textos de nombres de animales en la interfaz de usuario
    public Button leftArrowButton; // Flecha izquierda en la interfaz de usuario
    public Button rightArrowButton; // Flecha derecha en la interfaz de usuario

    private Sprite[] displayedAnimalImages = new Sprite[3]; // Imágenes de animales que se muestran actualmente
    private string[] displayedAnimalNames = new string[3]; // Nombres de animales que se muestran actualmente
    private int currentIndex = 0; // Índice del animal actualmente mostrado

    void Start()
    {
        GenerateAnimals();
        UpdateAnimalUIs();
        UpdateArrowButtons();
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene("RecapMoni");
    }

    void GenerateAnimals()
    {
        List<int> chosenIndices = new List<int>();

        int maxIndex = Mathf.Min(3, allAnimalImages.Length); // Determina el máximo de animales a mostrar
        for (int i = 0; i < maxIndex; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, allAnimalImages.Length);
            } while (chosenIndices.Contains(randomIndex));

            chosenIndices.Add(randomIndex);

            displayedAnimalImages[i] = allAnimalImages[randomIndex];
            displayedAnimalNames[i] = allAnimalNames[randomIndex];
        }
    }



    void UpdateAnimalUIs()
    {
        // Actualiza las imágenes y nombres de animales en la interfaz de usuario
        for (int i = 0; i < displayedAnimalImages.Length; i++)
        {
            if (displayedAnimalImages[i] != null)
            {
                animalImageUIs[i].sprite = displayedAnimalImages[i];
                animalNameUIs[i].text = displayedAnimalNames[i];
            }
            else
            {
                // Si no hay un animal para mostrar, desactiva la imagen y el nombre
                animalImageUIs[i].gameObject.SetActive(false);
                animalNameUIs[i].gameObject.SetActive(false);
            }
        }
    }

    void UpdateArrowButtons()
    {
        // Activa o desactiva las flechas según el número de animales generados
        leftArrowButton.gameObject.SetActive(displayedAnimalImages.Length > 1);
        rightArrowButton.gameObject.SetActive(displayedAnimalImages.Length > 1);
    }

    public void ShowPreviousAnimal()
    {
        currentIndex = (currentIndex - 1 + displayedAnimalImages.Length) % displayedAnimalImages.Length;
        UpdateAnimalUIs();
    }

    public void ShowNextAnimal()
    {
        currentIndex = (currentIndex + 1) % displayedAnimalImages.Length;
        UpdateAnimalUIs();
    }
}
