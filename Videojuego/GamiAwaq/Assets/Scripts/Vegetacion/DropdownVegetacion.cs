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
        string pickedReptile = dropdown.options[pickedValue].text;
        Debug.Log(pickedReptile);
        return pickedReptile;
    }

    public void ChangeDropdownOptions(string Name)
    {
        Debug.Log("El nombre correcto " + Name);
        dropdown.ClearOptions();
        List<string> reptileNames = new List<string>();
        int i = 0;

        while (i < 3)
        {
            int randReptile = Random.Range(0, ReptileBehaviour.Instance.reptiles.Count);
            string reptileName = ReptileBehaviour.Instance.reptiles[(ReptileBehaviour.ReptileType)randReptile].Name;
            if (!reptileNames.Contains(reptileName) && reptileName != Name)
            {
                reptileNames.Add(reptileName);
                i++;
            }
        }

        int insertIndex = Random.Range(0, reptileNames.Count);
        reptileNames.Insert(insertIndex, Name);

        dropdown.AddOptions(reptileNames);
    }
}