using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReptileBehaviour : MonoBehaviour
{

    public ReptileType reptileType = ReptileType.Crocodile;    

    public enum ReptileType
    {
        Crocodile,
        Turtle,
        Snake
    }

    [System.Serializable]
    public class Reptile
    {
        public int ID;
        public string Name;
        public string Image;
    }

    Dictionary<ReptileType, Reptile> reptiles = new Dictionary<ReptileType, Reptile>
    {
        {ReptileType.Crocodile, new Reptile { ID = 1, Name = "Crocodile", Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/bd/Nile_crocodile_head.jpg/220px-Nile_crocodile_head.jpg" } },
        {ReptileType.Turtle, new Reptile { ID = 2, Name = "Turtle", Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR8Rm_QNyyjyDgtVwduMH68WIMhr7GilCP6UkoM9tzuoQ&s" } },
        {ReptileType.Snake, new Reptile { ID = 3, Name = "Snake", Image = "https://www.worldatlas.com/upload/a4/91/91/shutterstock-1708408498.jpg" } }
    };
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Reptile reptile = reptiles[reptileType];
            ReptilesController.Instance.activatePopup(reptile.Image);
            GameObject.Destroy(this.gameObject);
        }
    }



    
}
