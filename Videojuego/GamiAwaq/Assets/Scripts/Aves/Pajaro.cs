using UnityEngine;

public class Pajaro : MonoBehaviour
{
    private GameManager gameManager;

    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Pajaro"))
            {
                // Verifica si el GameManager est� asignado
                if (gameManager != null)
                {
                    // Incrementa el contador de p�jaros clicados
                    gameManager.IncrementarContadorPajarosClicados();
                }
                else
                {
                    Debug.LogWarning("El GameManager no est� asignado en el objeto Pajaro.");
                }

                // Hacer desaparecer el p�jaro
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
