using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RastrosBehaviour : MonoBehaviour
{
    static public RastrosBehaviour Instance;

    public RestrosType reptileType = RestrosType.HuellaDeTortuga;


    private Rigidbody2D _rigidbody;
    private Vector2 _targetDirection;
    private float _changeDirectionCooldown;

    public enum RestrosType
    {
        HuellaDeTortuga,
        HuellaDeCocodrilo,
        RastroDeSerpiente,
        HuellaDeLagartija,
        HuellaDeCamaleon,
        HuellaDeOcelote,
        HuellaDeZorro,
        HuellaDeArmadillo,
        HuellaDeMapache,
        HuellaDeCoati
    }

    [System.Serializable]
    public class Reptile
    {
        public int ID;
        public string Name;
        public string Image;
    }


    public Dictionary<RestrosType, Reptile> reptiles = new Dictionary<RestrosType, Reptile>
    {
        {RestrosType.HuellaDeTortuga, new Reptile{ID = 0, Name = "Huella de Tortuga", Image = "Tortuga"}},
        {RestrosType.HuellaDeCocodrilo, new Reptile{ID = 1, Name = "Huella de Cocodrilo", Image = "Cocodrilo"}},
        {RestrosType.RastroDeSerpiente, new Reptile{ID = 2, Name = "Rastro de Serpiente", Image = "Serpiente"}},
        {RestrosType.HuellaDeLagartija, new Reptile{ID = 3, Name = "Huella de Lagartija", Image = "Lagartija"}},
        {RestrosType.HuellaDeCamaleon, new Reptile{ID = 4, Name = "Huella de Camaleon", Image = "Camaleon"}},
        {RestrosType.HuellaDeOcelote, new Reptile{ID = 5, Name = "Huella de Ocelote", Image = "Ocelote"}},
        {RestrosType.HuellaDeZorro, new Reptile{ID = 6, Name = "Huella de Zorro", Image = "Zorro"}},
        {RestrosType.HuellaDeArmadillo, new Reptile{ID = 7, Name = "Huella de Armadillo", Image = "Armadillo"}},
        {RestrosType.HuellaDeMapache, new Reptile{ID = 8, Name = "Huella de Mapache", Image = "Mapache"}},
        {RestrosType.HuellaDeCoati, new Reptile{ID = 9, Name = "Huella de Coati", Image = "Coati"}}
    };

    private void Awake()
    {
        Instance = this;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Reptile reptile = reptiles[reptileType];
            RastrosController.Instance.activatePopup(reptile.Image);
            DropdownRastros.Instance.ChangeDropdownOptions(reptile.Name);
            GameObject.Destroy(this.gameObject);
            PlayerPrefs.SetString("reptile", reptile.Name);
        }
    }
}
