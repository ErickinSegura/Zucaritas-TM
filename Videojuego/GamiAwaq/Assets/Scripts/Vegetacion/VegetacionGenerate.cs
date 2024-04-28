using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class VegetacionGenerate : MonoBehaviour
{
    public GameObject prefab;
    public Tilemap tilemap;
    public int numberOfObjects = 10;
    public Sprite[] variants;


    public VegetacionBehaviour.VegeType[] availableTypes; // Array de tipos de reptiles disponibles

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
            VegetacionBehaviour.VegeType randomType = availableTypes[Random.Range(0, availableTypes.Length)];

            // Instanciar el prefab correspondiente al tipo aleatorio
            GameObject VegePrefab = Instantiate(prefab, spawnPosition, Quaternion.identity);
            VegetacionBehaviour vegeBehaviour = VegePrefab.GetComponent<VegetacionBehaviour>();
            SpriteRenderer spriteRenderer = VegePrefab.GetComponent<SpriteRenderer>(); // Obtener el SpriteRenderer del reptil

            vegeBehaviour.vegeType = randomType;

            if (vegeBehaviour != null && spriteRenderer != null)
            {
                // Establecer el tipo aleatorio al comportamiento del reptil
                vegeBehaviour.vegeType = randomType;

                // Asignar el sprite correspondiente al tipo de reptil
                Sprite selectedSprite = null;


                switch (randomType)
                {
                    // Flores
                    case VegetacionBehaviour.VegeType.Rosa:
                    case VegetacionBehaviour.VegeType.Crisantemo:
                    case VegetacionBehaviour.VegeType.Clavel:
                    case VegetacionBehaviour.VegeType.Lirio:
                    case VegetacionBehaviour.VegeType.Bromelia:
                    case VegetacionBehaviour.VegeType.Orquidea:
                    case VegetacionBehaviour.VegeType.Magnolia:
                        selectedSprite = variants[0];
                        break;

                    // Otros tipos de vegetación
                    case VegetacionBehaviour.VegeType.Frailejon:
                    case VegetacionBehaviour.VegeType.Helecho:
                    case VegetacionBehaviour.VegeType.Drosera:
                        selectedSprite = variants[1];
                        break;

                }
                spriteRenderer.sprite = selectedSprite; // Asignar el sprite seleccionado al SpriteRenderer del reptil 
            }
            

            availablePositions.RemoveAt(randomIndex); // Eliminar la posición utilizada para evitar la superposición de objetos

        }
        
    
        
    }
}

