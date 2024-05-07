using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintsController : MonoBehaviour
{
    public GameObject arrow;

    public GameObject cartelOG;
    public GameObject cartelBig;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(ShowHint());
            ScaleCartel();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopAllCoroutines();
            arrow.SetActive(false);
            resetCartel();
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

    public void ScaleCartel()
    {
        cartelOG.SetActive(false);
        cartelBig.SetActive(true);
    }

    public void resetCartel()
    {
        cartelOG.SetActive(true);
        cartelBig.SetActive(false);
    }
}
