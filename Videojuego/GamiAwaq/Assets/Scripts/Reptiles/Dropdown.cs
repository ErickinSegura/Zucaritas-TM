using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dropdown : MonoBehaviour
{
    public static Dropdown Instance;

    [SerializeField] private TMP_Dropdown dropdown;

    private void Awake()
    {
        Instance = this;
    }

    public void GetDropdownValue()
    {
        int pickedValue = dropdown.value;
        string pickedReptile = dropdown.options[pickedValue].text;
        Debug.Log(pickedReptile);
    }

    public void ChangeDropdownOptions(string Name)
    {
        Debug.Log(Name);
        dropdown.ClearOptions();
        List<string> reptileNames = new List<string>();
        int i = 0;

        // Add 3 random reptile names to the list
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

        // Insert the correct reptile name at a random position in the list
        int insertIndex = Random.Range(0, reptileNames.Count);
        reptileNames.Insert(insertIndex, Name);

        dropdown.AddOptions(reptileNames);
    }
}
