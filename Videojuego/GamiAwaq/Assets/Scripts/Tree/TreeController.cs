using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TreeController : MonoBehaviour
{
    public enum Direction { N, E, S, O };

    public Direction currentDirection = Direction.N;
    public Text directionText;

    void Start()
    {
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
    }

    public void MamiferosLoad()
    {
        SceneManager.LoadScene("Mamiferos");
    }
}
