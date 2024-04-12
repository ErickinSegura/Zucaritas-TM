using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MamiferosManager : MonoBehaviour
{
    public List<Mamifero> mamiferos;
    public Image imagenMamifero;
    public Text nombreMamifero;

    private List<Mamifero> mamiferosMostrados = new List<Mamifero>();
    private int indiceActual = 0;

    void Start()
    {
        MostrarMamiferosAleatorios();
    }

    public void MostrarMamiferosAleatorios()
    {
        int cantidadMostrar = Random.Range(1, 4);

        for (int i = 0; i < cantidadMostrar; i++)
        {
            Mamifero mamifero = mamiferos[Random.Range(0, mamiferos.Count)];
            mamiferosMostrados.Add(mamifero);
            // Mostrar el mamífero en la interfaz de usuario
        }
    }

    public void MostrarSiguienteMamifero()
    {
        indiceActual = (indiceActual + 1) % mamiferosMostrados.Count;
        MostrarMamifero(indiceActual);
    }

    public void MostrarAnteriorMamifero()
    {
        indiceActual = (indiceActual - 1 + mamiferosMostrados.Count) % mamiferosMostrados.Count;
        MostrarMamifero(indiceActual);
    }

    private void MostrarMamifero(int indice)
    {
        Mamifero mamifero = mamiferosMostrados[indice];
        // Actualizar la imagen y el texto mostrados en la interfaz de usuario
    }
}
