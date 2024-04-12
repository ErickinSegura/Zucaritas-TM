using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropdownAves : MonoBehaviour
{
    public static DropdownAves Instance;

    [SerializeField] private TMP_Dropdown dropdownAve;

    private void Awake()
    {
        Instance = this;
    }

    public string GetDropdownValue()
    {
        int pickedValue = dropdownAve.value;
        string pickedBird = dropdownAve.options[pickedValue].text;
        Debug.Log(pickedBird);
        return pickedBird;  
    }

    public void ChangeDropdownOptions(string Name)
    {
        Debug.Log("El nombre correcto "+Name);
        dropdownAve.ClearOptions();
        List<string> birdNames = new List<string>();
        int i = 0;

        while (i < 3)
        {
            int randBird = Random.Range(0, Pajaro.Instance.aves.Count);
            string birdName = Pajaro.Instance.aves[(Pajaro.TipoAve)randBird].Name;
            if (!birdNames.Contains(birdName) && birdName != Name)
            {
                birdNames.Add(birdName);
                i++;
            }
        }

        int insertIndex = Random.Range(0, birdNames.Count);
        birdNames.Insert(insertIndex, Name);

        dropdownAve.AddOptions(birdNames);
    }




}
