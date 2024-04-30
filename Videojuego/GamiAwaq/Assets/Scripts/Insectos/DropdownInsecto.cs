using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DropdownInsecto : MonoBehaviour
{
    public static DropdownInsecto Instance;

    [SerializeField] private TMP_Dropdown dropdown;

    private void Awake()
    {
        Instance = this;
    }

    public string GetDropdownValue()
    {
        int pickedValue = dropdown.value;
        string pickedInse = dropdown.options[pickedValue].text;
        Debug.Log(pickedInse);
        return pickedInse;
    }

    public void ChangeDropdownOptions(string Name)
    {
        Debug.Log("El nombre correcto " + Name);
        dropdown.ClearOptions();
        List<string> inseNames = new List<string>();
        int i = 0;

        while (i < 3)
        {
            int randinse = Random.Range(0, Insecto.Instance.insectos.Count);
            string inseName = Insecto.Instance.insectos[(Insecto.InsectoType)randinse].Name;
            if (!inseNames.Contains(inseName) && inseName != Name)
            {
                inseNames.Add(inseName);
                i++;
            }
        }

        int insertIndex = Random.Range(0, inseNames.Count);
        inseNames.Insert(insertIndex, Name);

        dropdown.AddOptions(inseNames);
    }
}
