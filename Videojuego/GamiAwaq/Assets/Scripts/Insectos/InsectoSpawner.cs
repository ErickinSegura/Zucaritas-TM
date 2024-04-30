using UnityEngine;

public class InsectoSpawner : MonoBehaviour
{
    public GameObject insectoPrefab;
    public float tiempoEntreSpawnMin = 3f;
    public float tiempoEntreSpawnMax = 5f;
    public Transform[] lamparas;

    public Insecto.InsectoType[] availableTypes;

    void Start()
    {
        InvokeRepeating("SpawnInsecto", Random.Range(tiempoEntreSpawnMin, tiempoEntreSpawnMax), Random.Range(tiempoEntreSpawnMin, tiempoEntreSpawnMax));
    }

    void SpawnInsecto()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, Random.Range(-2.5f, 2.5f), 0f);
        GameObject nuevoInsecto = Instantiate(insectoPrefab, spawnPosition, Quaternion.identity);
        nuevoInsecto.GetComponent<Insecto>().lamparas = lamparas;

        Insecto.InsectoType randomType = availableTypes[Random.Range(0, availableTypes.Length)];


        Insecto insecto = insectoPrefab.GetComponent<Insecto>();

        insecto.insectosType = randomType;
    }
}
