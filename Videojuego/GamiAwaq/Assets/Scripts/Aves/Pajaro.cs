using UnityEngine;

public class Pajaro : MonoBehaviour
{
    public static Pajaro Instance;
    private DropdownAves dropdownController;
    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Buscar el controlador del dropdown en la escena
        dropdownController = GameObject.FindObjectOfType<DropdownAves>();
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
                dropdownController.ShowDropdown(gameObject);

                // Hacer desaparecer el pájaro
                Destroy(hit.collider.gameObject);
            }

        }
    }
    private void OnMouseDown()
    {
        // Verificar si el controlador del dropdown existe y mostrar el dropdown
        if (dropdownController != null)
        {
            // Mostrar el dropdown y pasar el GameObject del pájaro
            dropdownController.ShowDropdown(gameObject);
        }
    }
}
