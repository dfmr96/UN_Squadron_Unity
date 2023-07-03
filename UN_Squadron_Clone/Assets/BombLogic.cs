using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BombLogic : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] int impulseForce;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClip;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce((Vector2.right + Vector2.down) * impulseForce, ForceMode2D.Impulse);
        PlaySound(audioClip[0]);
    }
    private void Update()
    {
//        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            DestroyBomb();
        }
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void DestroyBomb()
    {
        animator.SetBool("isDestroyed", true);
        rb.Sleep();
        GetComponent<Collider2D>().enabled = false;
        PlaySound(audioClip[1]);
        Destroy(gameObject,0.5f);
    }
}
