using System.Collections;
using UnityEngine;

public class BulletTime : MonoBehaviour
{
    public float bulletTimeTime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BulletTime")) StartCoroutine(ActiveBulletTime());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BulletTime"))
        {
            Time.timeScale = 1f;
        }
    }
    public IEnumerator ActiveBulletTime()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(bulletTimeTime);
        Time.timeScale = 1f;
    }
}
