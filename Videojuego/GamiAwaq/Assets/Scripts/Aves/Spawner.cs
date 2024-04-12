using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject pajaroPrefab;
    public float tiempoEntreSpawnMin = 1f;
    public float tiempoEntreSpawnMax = 3f;
    public float velocidadMinima = 1f;
    public float velocidadMaxima = 5f;
    public GameManager gameManager; // Agrega una referencia al GameManager

    public Pajaro.TipoAve[] availableTypes;

    void Start()
    {
        Debug.Log("Lista:" + availableTypes);

        InvokeRepeating("SpawnPajaro", Random.Range(tiempoEntreSpawnMin, tiempoEntreSpawnMax), Random.Range(tiempoEntreSpawnMin, tiempoEntreSpawnMax));
    }

    void SpawnPajaro()
    {
        float spawnY = Random.Range(-2.5f, 2.5f);
        Vector3 spawnPosition = new Vector3(transform.position.x, spawnY, 0f);

       


        // Cambiar el tipo de ave en el GameManager

        Pajaro.TipoAve randomType = availableTypes[Random.Range(0, availableTypes.Length)];


        GameObject PajaroPrefab = Instantiate(pajaroPrefab, spawnPosition, Quaternion.identity);
        Pajaro pajaroBeh = PajaroPrefab.GetComponent<Pajaro>();

        pajaroBeh.tipoAve = randomType;



        // Cambiar la velocidad del pájaro y dirección del movimiento
        MovimientoHorizontal movimientoPajaro = PajaroPrefab.GetComponent<MovimientoHorizontal>();
        if (movimientoPajaro != null)
        {
            float nuevaVelocidad = Random.Range(velocidadMinima, velocidadMaxima);
            movimientoPajaro.CambiarVelocidad(nuevaVelocidad);
        }
    }
}
