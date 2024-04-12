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
                // Verifica si el GameManager está asignado
                if (gameManager != null)
                {
                    // Incrementa el contador de pájaros clicados
                    gameManager.IncrementarContadorPajarosClicados();
                }
                else
                {
                    Debug.LogWarning("El GameManager no está asignado en el objeto Pajaro.");
                }

                // Hacer desaparecer el pájaro
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
