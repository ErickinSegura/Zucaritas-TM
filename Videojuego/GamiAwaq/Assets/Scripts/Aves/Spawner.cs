using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject pajaroPrefab;
    public float tiempoEntreSpawnMin = 1f;
    public float tiempoEntreSpawnMax = 3f;
    public float velocidadMinima = 1f;
    public float velocidadMaxima = 5f;
    public GameManager gameManager; // Agrega una referencia al GameManager

    void Start()
    {
        InvokeRepeating("SpawnPajaro", Random.Range(tiempoEntreSpawnMin, tiempoEntreSpawnMax), Random.Range(tiempoEntreSpawnMin, tiempoEntreSpawnMax));
    }

    void SpawnPajaro()
    {
        float spawnY = Random.Range(-2.5f, 2.5f);
        Vector3 spawnPosition = new Vector3(transform.position.x, spawnY, 0f);
        GameObject pajaro = Instantiate(pajaroPrefab, spawnPosition, Quaternion.identity);

        // Cambiar la velocidad del pájaro y dirección del movimiento
        MovimientoHorizontal movimientoPajaro = pajaro.GetComponent<MovimientoHorizontal>();
        if (movimientoPajaro != null)
        {
            float nuevaVelocidad = Random.Range(velocidadMinima, velocidadMaxima);
            movimientoPajaro.CambiarVelocidad(nuevaVelocidad);
        }
    }
}
