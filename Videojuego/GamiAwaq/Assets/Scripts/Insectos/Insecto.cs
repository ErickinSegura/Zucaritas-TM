using UnityEngine;

public class Insecto : MonoBehaviour
{
    public Transform[] lamparas;
    public float velocidad = 3f; // velocidad de movimiento de los insectos

    private Transform objetivo; // lampara objetivo hacia la que se moverá el insecto

    void Start()
    {
        // encontrar la lampara mas cercana al insecto al inicio
        objetivo = EncontrarLamparaMasCercana();
    }

    void Update()
    {
        // verificar si hay un objetivo asignado
        if (objetivo != null)
        {
            // calcular la dirección hacia el objetivo
            Vector3 direccion = (objetivo.position - transform.position).normalized;

            // mover el insecto hacia el objetivo
            transform.Translate(direccion * velocidad * Time.deltaTime);

            // verificar si el insecto ha llegado a la lampara
            if (Vector3.Distance(transform.position, objetivo.position) < 0.5f)
            {
                //detener el insecto
                velocidad = 0f;
            }
        }
    }

    //encontrar la lampara mas cercana al insecto
    private Transform EncontrarLamparaMasCercana()
    {
        Transform lamparaMasCercana = null;
        float distanciaMasCercana = Mathf.Infinity;

        foreach (Transform lampara in lamparas)
        {
            float distancia = Vector3.Distance(transform.position, lampara.position);
            if (distancia < distanciaMasCercana)
            {
                distanciaMasCercana = distancia;
                lamparaMasCercana = lampara;
            }
        }

        return lamparaMasCercana;
    }

    void OnMouseDown()
    {
        // Hacer desaparecer el insecto al hacer clic
        Destroy(gameObject);

        // Mostrar el dropdown con los insectos:
        DropdownManager.Instance.ShowDropdown();
    }
}
