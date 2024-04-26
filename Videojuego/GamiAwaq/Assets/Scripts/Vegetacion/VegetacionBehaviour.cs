using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetacionBehaviour : MonoBehaviour
{
    static public VegetacionBehaviour Instance;

    public ReptileType reptileType = ReptileType.CaimanAguja;

    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private Rigidbody2D _rigidbody;
    private Vector2 _targetDirection;
    private float _changeDirectionCooldown;

    public enum ReptileType
    {
        CaimanAguja,
        CaimanLlanero,
        SerpienteSabanera,
        SerpienteTerciopelo,
        SerpienteSanAndres,
        TortugaCienegaCol,
        TortugaMorrocoy,
        CamaleonCundimamarca,
        AnolisCalima,
        LagartijaBogota
    }

    [System.Serializable]
    public class Reptile
    {
        public int ID;
        public string Name;
        public string Image;
    }


    public Dictionary<ReptileType, Reptile> reptiles = new Dictionary<ReptileType, Reptile>
    {
        {ReptileType.CaimanAguja, new Reptile { ID = 1, Name = "Caimán Aguja", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep8.png?raw=true" } },
        {ReptileType.CaimanLlanero, new Reptile { ID = 2, Name = "Caimán Llanero", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep4.png?raw=true" } },
        {ReptileType.SerpienteSabanera, new Reptile { ID = 3, Name = "Serpiente Sabanera", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep7.png?raw=true" } },
        {ReptileType.SerpienteTerciopelo, new Reptile { ID = 4, Name = "Serpiente Terciopelo", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep3.png?raw=true" } },
        {ReptileType.SerpienteSanAndres, new Reptile { ID = 5, Name = "Serpiente San Andrés", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep5.png?raw=true" } },
        {ReptileType.TortugaCienegaCol, new Reptile { ID = 6, Name = "Tortuga Ciénega Col", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep9.png?raw=true" } },
        {ReptileType.TortugaMorrocoy, new Reptile { ID = 7, Name = "Tortuga Morrocoy", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep10.png?raw=true" } },
        {ReptileType.CamaleonCundimamarca, new Reptile { ID = 8, Name = "Camaleón Cundimamarca", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep2.png?raw=true" } },
        {ReptileType.AnolisCalima, new Reptile { ID = 9, Name = "Anolis Calima", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep6.png?raw=true" } },
        {ReptileType.LagartijaBogota, new Reptile { ID = 10, Name = "Lagartija Bogotá", Image = "https://github.com/ErickinSegura/Zucaritas-TM/blob/main/Videojuego/Assets/rep1.png?raw=true" } }
    };

    private void Awake()
    {
        Instance = this;
        _rigidbody = GetComponent<Rigidbody2D>();
        _changeDirectionCooldown = Random.Range(1f, 5f);
    }

    private void FixedUpdate()
    {
        HandleRandomDirectionChange();
    }

    private void HandleRandomDirectionChange()
    {
        _changeDirectionCooldown -= Time.deltaTime;

        if (_changeDirectionCooldown <= 0)
        {
            float angleChange = Random.Range(-180f, 180f);
            Vector2 randomDirection = Quaternion.AngleAxis(angleChange, Vector3.forward) * Vector2.up;
            _rigidbody.AddForce(randomDirection.normalized * _speed, ForceMode2D.Impulse);

            _changeDirectionCooldown = Random.Range(1f, 5f);
        }
    }

    private void SetVelocity()
    {
        _rigidbody.velocity = _targetDirection * _speed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Reptile reptile = reptiles[reptileType];
            ReptilesController.Instance.activatePopup(reptile.Image);
            Dropdown.Instance.ChangeDropdownOptions(reptile.Name);
            GameObject.Destroy(this.gameObject);
            PlayerPrefs.SetString("reptile", reptile.Name);
        }
    }

}
