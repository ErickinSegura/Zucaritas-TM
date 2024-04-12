using UnityEngine;

public class Pajaro : MonoBehaviour
{
    public static Pajaro Instance;
    public GameObject dropdownGameObject;

    public void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Pajaro"))
            {
                GameManager.instance.IncrementarContadorPajarosClicados();
                // Hacer desaparecer el pájaro
                dropdownGameObject.SetActive(true);
                Time.timeScale = 0f;
                Destroy(hit.collider.gameObject);
                
            }

        }
    }
}
