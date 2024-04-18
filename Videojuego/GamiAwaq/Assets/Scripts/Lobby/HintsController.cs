using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintsController : MonoBehaviour
{
    public GameObject arrow;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(ShowHint());
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopAllCoroutines();
            arrow.SetActive(false);
        }
    }

    IEnumerator ShowHint()
    {
        while (true)
        {
            arrow.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            arrow.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
