using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Aves : MonoBehaviour
{
    
    void Update()
    {

        // Detectar si se hace clic con el mouse
        if (Input.GetMouseButtonDown(0))
        {
            // Obtener la posición del clic en el mundo
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Lanzar un rayo desde la cámara hacia la posición del clic
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Si el rayo golpea un objeto
            if (hit.collider != null)
            {
                // Verificar si el objeto golpeado es uno de los pájaros
                if (hit.collider.gameObject.CompareTag("Pajaro"))
                {
                    // Desactivar el objeto (hacerlo desaparecer)
                    hit.collider.gameObject.SetActive(false);
                }
            }
        }
    }
    public void returnToTree()
    {
        SceneManager.LoadScene("Arbol Cam");
    }
}
