using UnityEngine;

public class MovimientoHorizontal : MonoBehaviour
{
    public float velocidad = 2.0f;

    // M�todo para cambiar la velocidad del p�jaro
    public void CambiarVelocidad(float nuevaVelocidad)
    {
        velocidad = nuevaVelocidad;
    }
    void Update()
    {
        transform.Translate(Vector3.right * velocidad * Time.deltaTime);

        // Si el p�jaro sale de la pantalla, destruirlo
        if (transform.position.x > 10f || transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }

}

