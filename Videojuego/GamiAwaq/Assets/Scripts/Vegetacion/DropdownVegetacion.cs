using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropdownVegetacion : MonoBehaviour
{
    public static DropdownVegetacion Instance;

    [SerializeField] private TMP_Dropdown dropdown;

    private void Awake()
    {
        Instance = this;
    }

    public string GetDropdownValue()
    {
        int pickedValue = dropdown.value;
        string pickedVeg = dropdown.options[pickedValue].text;
        Debug.Log(pickedVeg);
        return pickedVeg;
    }

    public void ChangeDropdownOptions(string Name)
    {
        Debug.Log("El nombre correcto " + Name);
        dropdown.ClearOptions();
        List<string> vegeNames = new List<string>();
        int i = 0;

        while (i < 3)
        {
            int randVege = Random.Range(0, VegetacionBehaviour.Instance.veges.Count);
            string vegeName = VegetacionBehaviour.Instance.veges[(VegetacionBehaviour.VegeType)randVege].Name;
            if (!vegeNames.Contains(vegeName) && vegeName != Name)
            {
                vegeNames.Add(vegeName);
                i++;
            }
        }

        int insertIndex = Random.Range(0, vegeNames.Count);
        vegeNames.Insert(insertIndex, Name);

        dropdown.AddOptions(vegeNames);
    }
}
