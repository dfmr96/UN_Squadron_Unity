using UnityEngine;

public class EnemyEnabler : MonoBehaviour
{
    [SerializeField] BoxCollider2D _cameraBounds;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            Behaviour[] comps = collision.gameObject.GetComponents<Behaviour>();
            foreach (Behaviour comp in comps)
            {
                //Debug.Log(comp);
                comp.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyBullet>() != null || collision.GetComponent<Enemy>() != null)
        {
            Destroy(collision.gameObject);
        }
    }
}
