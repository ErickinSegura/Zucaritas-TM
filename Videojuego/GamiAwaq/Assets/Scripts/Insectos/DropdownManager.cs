using UnityEngine;
using TMPro;

public class DropdownManager : MonoBehaviour
{
    public static DropdownManager Instance;

    [SerializeField] private TMP_Dropdown dropdown;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowDropdown()
    {
        // Mmostrar el dropdown con las opciones de insectos
        dropdown.gameObject.SetActive(true);
    }

    public string GetSelectedAnimal()
    {
        // obtener el insecto seleccionado en el dropdown
        return dropdown.options[dropdown.value].text;
    }

    public void HideDropdown()
    {
        // ocultar el dropdown
        dropdown.gameObject.SetActive(false);
    }
}
