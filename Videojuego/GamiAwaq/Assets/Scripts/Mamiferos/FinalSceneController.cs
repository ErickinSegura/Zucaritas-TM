using UnityEngine;
using UnityEngine.UI;

public class FinalSceneController : MonoBehaviour
{
    public Text numberOfAnimalsText;

    void Start()
    {
        // Obtener el valor de numberOfAnimalsToShow guardado en PlayerPrefs
        int numberOfAnimalsToShow = PlayerPrefs.GetInt("NumberOfAnimalsToShow", -1);

        // Verificar si se pudo obtener el valor correctamente
        if (numberOfAnimalsToShow != -1)
        {
            // Mostrar el valor en el objeto de texto
            numberOfAnimalsText.text = numberOfAnimalsToShow.ToString();
        }
        else
        {
            // Mostrar un mensaje de error si no se pudo obtener el valor
            numberOfAnimalsText.text = "Error: No se pudo obtener el número de animales";
        }
    }
}
