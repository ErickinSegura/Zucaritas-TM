using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownAves : MonoBehaviour
{
    public static DropdownAves Instance;

    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private Image birdImage;
    [SerializeField] private GameObject dropdownPanel;

    private bool isDropdownActive = false;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowDropdown(GameObject birdObject)
    {
        isDropdownActive = true;
        dropdownPanel.SetActive(true);
        StartCoroutine(DisableInputWhileDropdownIsOpen());
        ChangeDropdownOptions(birdObject);
    }

    public void HideDropdown()
    {
        isDropdownActive = false;
        dropdownPanel.SetActive(false);
    }

    public string GetDropdownValue()
    {
        int pickedValue = dropdown.value;
        string pickedBird = dropdown.options[pickedValue].text;
        Debug.Log("El ave seleccionada es: " + pickedBird);
        return pickedBird;
    }

    private IEnumerator DisableInputWhileDropdownIsOpen()
    {
        while (isDropdownActive)
        {
            yield return null;
        }

        // Activar nuevamente la entrada cuando se cierra el dropdown
        yield return new WaitForSecondsRealtime(0.1f); // Asegúrate de que la pausa sea lo suficientemente corta para que Unity reconozca el cambio
        Time.timeScale = 1f;
    }

    private void ChangeDropdownOptions(GameObject birdObject)
    {
        dropdown.ClearOptions();
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        // Obtener todos los GameObjects de los pájaros en la escena
        GameObject[] birds = GameObject.FindGameObjectsWithTag("Pajaro");

        foreach (var bird in birds)
        {
            // Obtener el nombre del pájaro del GameObject y agregarlo a las opciones del dropdown
            string birdName = bird.name;
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = birdName;
            options.Add(option);
        }

        dropdown.options = options;
    }

    public void DropdownValueChanged(int index)
    {
        string selectedBird = dropdown.options[index].text;
        // Aquí podrías hacer lo que necesites con el ave seleccionada, como mostrar su imagen
        Debug.Log("El ave seleccionada es: " + selectedBird);
        // Ocultar el dropdown después de seleccionar una opción
        HideDropdown();
    }
}
