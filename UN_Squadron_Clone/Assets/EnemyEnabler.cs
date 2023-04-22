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
                Debug.Log(comp);
                comp.enabled = true;
            }
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
