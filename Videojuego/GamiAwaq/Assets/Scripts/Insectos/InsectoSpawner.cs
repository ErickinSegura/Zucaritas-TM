using UnityEngine;

public class InsectoSpawner : MonoBehaviour
{
    public GameObject insectoPrefab;
    public float tiempoEntreSpawnMin = 1f;
    public float tiempoEntreSpawnMax = 3f;
    public Transform[] lamparas;

    void Start()
    {
        InvokeRepeating("SpawnInsecto", Random.Range(tiempoEntreSpawnMin, tiempoEntreSpawnMax), Random.Range(tiempoEntreSpawnMin, tiempoEntreSpawnMax));
    }

    void SpawnInsecto()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, Random.Range(-2.5f, 2.5f), 0f);
        GameObject nuevoInsecto = Instantiate(insectoPrefab, spawnPosition, Quaternion.identity);
        nuevoInsecto.GetComponent<Insecto>().lamparas = lamparas;
    }
}
