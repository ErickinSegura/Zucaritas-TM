using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Generate : MonoBehaviour
{
    public GameObject prefab;
    public Tilemap tilemap;
    public int numberOfObjects = 10;

    // Start is called before the first frame update
    void Start()
    {
        BoundsInt bounds = tilemap.cellBounds; // Obtener los l�mites del Tilemap

        // Calcular el �rea total del Tilemap
        int totalTiles = bounds.size.x * bounds.size.y;

        // Si el �rea total es menor que el n�mero de objetos, reducir el n�mero de objetos
        if (totalTiles < numberOfObjects)
        {
            numberOfObjects = totalTiles;
        }

        // Crear una lista de posiciones de celda dentro del Tilemap
        List<Vector3Int> availablePositions = new List<Vector3Int>();

        // Agregar todas las posiciones de celda dentro del Tilemap a la lista
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cellPos = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(cellPos))
                {
                    availablePositions.Add(cellPos);
                }
            }
        }

        // Si hay menos posiciones disponibles que el n�mero de objetos, reducir el n�mero de objetos
        if (availablePositions.Count < numberOfObjects)
        {
            numberOfObjects = availablePositions.Count;
        }

        // Instanciar los objetos en posiciones aleatorias dentro del Tilemap
        for (int i = 0; i < numberOfObjects; i++)
        {
            int randomIndex = Random.Range(0, availablePositions.Count);
            Vector3 spawnPosition = tilemap.GetCellCenterWorld(availablePositions[randomIndex]);
            Instantiate(prefab, spawnPosition, Quaternion.identity);
            availablePositions.RemoveAt(randomIndex); // Eliminar la posici�n utilizada para evitar la superposici�n de objetos
        }
    }
}
