using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateRastros : MonoBehaviour
{
    public GameObject prefab;
    public Tilemap tilemap;
    public int numberOfObjects = 10;
    public Sprite[] variants;


    public RastrosBehaviour.RestrosType[] availableTypes; // Array de tipos de reptiles disponibles

    // Start is called before the first frame update
    public void Start()
    {

        BoundsInt bounds = tilemap.cellBounds; // Obtener los límites del Tilemap
        // Calcular el área total del Tilemap
        int totalTiles = bounds.size.x * bounds.size.y;

        // Si el área total es menor que el número de objetos, reducir el número de objetos
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

        // Si hay menos posiciones disponibles que el número de objetos, reducir el número de objetos
        if (availablePositions.Count < numberOfObjects)
        {
            numberOfObjects = availablePositions.Count;
        }

        // Instanciar los objetos en posiciones aleatorias dentro del Tilemap
        for (int i = 0; i < numberOfObjects; i++)
        {
            int randomIndex = Random.Range(0, availablePositions.Count);
            Vector3 spawnPosition = tilemap.GetCellCenterWorld(availablePositions[randomIndex]);

            // Seleccionar un tipo aleatorio de reptil
            RastrosBehaviour.RestrosType randomType = availableTypes[Random.Range(0, availableTypes.Length)];

            // Instanciar el prefab correspondiente al tipo aleatorio
            GameObject reptilePrefab = Instantiate(prefab, spawnPosition, Quaternion.identity);
            
            availablePositions.RemoveAt(randomIndex); // Eliminar la posición utilizada para evitar la superposición de objetos
        }
    }
}
