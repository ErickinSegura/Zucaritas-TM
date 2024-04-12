using UnityEngine;

[System.Serializable]
public class Mamifero
{
    public string nombre;
    public Sprite imagen;
    public Mamifero(string nombre, Sprite imagen)
    {
        this.nombre = nombre;
        this.imagen = imagen;
    }

    public string ObtenerNombre()
    {
        return nombre;
    }

    public Sprite ObtenerImagen()
    {
        return imagen;
    }


}
