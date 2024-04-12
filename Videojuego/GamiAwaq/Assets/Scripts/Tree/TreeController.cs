using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TreeController : MonoBehaviour
{
    public enum Direction { N, E, S, O };

    public Direction currentDirection;
    public Text directionText;

    void Start()
    {
        int randomDirectionIndex = Random.Range(0, 4);
        // Asigna la dirección correspondiente al número aleatorio generado
        currentDirection = (Direction)randomDirectionIndex;
        UpdateDirectionText();
    }

    void UpdateDirectionText()
    {
        directionText.text = currentDirection.ToString();
    }

    public void ChangeDirection(int directionChange)
    {
        int directionCount = System.Enum.GetValues(typeof(Direction)).Length;
        int newDirectionIndex = ((int)currentDirection + directionChange + directionCount) % directionCount;
        currentDirection = (Direction)newDirectionIndex;
        UpdateDirectionText();
        PlayerPrefs.SetString("currentDirection", currentDirection.ToString());
        Debug.Log("Dirección actual: " + currentDirection);
    }

    public void MamiferosLoad()
    {
        SceneManager.LoadScene("Mamiferos");
    }
}
