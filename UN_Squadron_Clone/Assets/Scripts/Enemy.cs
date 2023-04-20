using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] int health;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireRate;
    [SerializeField] float fireRateCounter;


    private void Start()
    {
        //Cuando se instancie cualquier objeto con este Script va a saber cual es el jugador
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    private void Update()
    {
        //fireRateCounter va sumando segundos
        fireRateCounter += Time.deltaTime;

        //Si los segundos transcurridos son mayores a 1 sobre fireRate...
        //1 / fireRate para poder definir cuantas balas por segundo se dispara
        if (fireRateCounter > 1 / fireRate)
        {
            FireBullet();
        }
    }
    //Este metodo recibe como parametro el dano que se quiera aplicar al objeto que lo porta
    public void TakeDamage(int damage)
    {
        //el dano estipulado en el parametro se le resta a la vida actual
        health -= damage;

        if (health <= 0)
        {
            //Si el enemigo muere porque su vida es 0 o menor...
            //...instancia el objeto explosion
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            //...se destruye el objeto que porta este script para ahorrar memoria
            Destroy(gameObject);
        }
    }

    public void FireBullet()
    {
        //...aimDir es la direccion a donde esta el jugador
        Vector3 aimDir = (player.transform.position - transform.position).normalized;
        //se crea la variable bullet para tener referencia del objeto recien creado
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        //se accede a EnemyBullet y se setea la direccion a la que se va a mover con el metodo SetDirection
        bullet.GetComponent<EnemyBullet>().SetDirection(aimDir);
        //Se resetea fireRateCounter para aplicar cooldown
        fireRateCounter = 0;
    }
}
