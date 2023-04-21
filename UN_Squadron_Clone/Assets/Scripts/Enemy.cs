using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private int _health;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _fireRateCounter;
    [SerializeField] private bool _canDropPOW;
    [SerializeField] private VulkanPOWType _type;
    [SerializeField] private GameObject[] _vulkanPOWs;


    private void Start()
    {
        //Cuando se instancie cualquier objeto con este Script va a saber cual es el jugador
        _player = FindObjectOfType<PlayerController>().gameObject;
    }

    private void Update()
    {
        //fireRateCounter va sumando segundos
        _fireRateCounter += Time.deltaTime;

        //Si los segundos transcurridos son mayores a 1 sobre fireRate...
        //1 / fireRate para poder definir cuantas balas por segundo se dispara
        if (_fireRateCounter > 1 / _fireRate)
        {
            FireBullet();
        }
    }
    //Este metodo recibe como parametro el dano que se quiera aplicar al objeto que lo porta
    public void TakeDamage(int damage)
    {
        //el dano estipulado en el parametro se le resta a la vida actual
        _health -= damage;

        if (_health <= 0)
        {
            if (_canDropPOW)
            {
                switch (_type)
                {
                    case VulkanPOWType.Orange:
                        Instantiate(_vulkanPOWs[0], transform.position, Quaternion.identity);
                        break;
                    case VulkanPOWType.Blue:
                        Instantiate(_vulkanPOWs[1], transform.position, Quaternion.identity);
                        break;
                    default:
                        break;
                }
            }
            //Si el enemigo muere porque su vida es 0 o menor...
            //...instancia el objeto explosion
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            //...se destruye el objeto que porta este script para ahorrar memoria
            Destroy(gameObject);
        }
    }

    public void FireBullet()
    {
        //...aimDir es la direccion a donde esta el jugador
        Vector3 aimDir = (_player.transform.position - transform.position).normalized;
        //se crea la variable bullet para tener referencia del objeto recien creado
        var bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        //se accede a EnemyBullet y se setea la direccion a la que se va a mover con el metodo SetDirection
        bullet.GetComponent<EnemyBullet>().SetDirection(aimDir);
        //Se resetea fireRateCounter para aplicar cooldown
        _fireRateCounter = 0;
    }
}
