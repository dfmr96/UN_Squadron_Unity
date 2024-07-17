using System.Collections;
using Enemies;
using Enemies.Core;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Weapons
{
    public class MegaCrushLogic : MonoBehaviour
    {
        public float speed;
        public int damage;
        public GameObject[] rayPrefabs;
        public int rayBursts;
        public Collider2D cameraBounds;
        public Light2D megaLight;
        public Color whiteLight;
        public Color blueLight;
        public AudioSource audiosource;
        public AudioClip[] audioClip;

        private void Start()
        {
            megaLight = GetComponent<Light2D>();
            audiosource = GetComponent<AudioSource>();
            audiosource.PlayOneShot(audioClip[0]);
        }

        private void Update()
        {
            transform.Translate((Vector3.up + Vector3.right).normalized * (speed * Time.deltaTime));
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("CameraBounds"))
            {
                speed = 0;
                cameraBounds = collision;
                StartCoroutine(CastRays());
            }
        }

        private IEnumerator CastRays()
        {
            megaLight.enabled = true;
            audiosource.PlayOneShot(audioClip[1]);
            for (int j = 0; j <= rayBursts; j++)
            {
                Vector3 cameraBoundsCorner = new Vector3(cameraBounds.bounds.max.x, cameraBounds.bounds.max.y, 0);
                for (int i = 0; i < rayPrefabs.Length; i++)
                {
                    Instantiate(rayPrefabs[i], cameraBoundsCorner, Quaternion.identity);
                    DealDamage();
                }
                megaLight.color = blueLight;
                yield return new WaitForSeconds(0.2f);
                megaLight.color = whiteLight;
                yield return new WaitForSeconds(0.2f);
            }
            Destroy(gameObject);
        }


        private void DealDamage()
        {
            Vector2 pointA = new Vector2(cameraBounds.bounds.min.x, cameraBounds.bounds.max.y);
            Vector2 pointB = new Vector2(cameraBounds.bounds.max.x, cameraBounds.bounds.min.y);
            Collider2D[] enemiesInside = Physics2D.OverlapAreaAll(pointA, pointB);

            foreach (Collider2D collider in enemiesInside)
            {
                if (collider.GetComponent<Enemy>() != null)
                {
                    collider.GetComponent<Enemy>().TakeDamage(damage);
                }

                if (collider.GetComponent<Boss>() != null)
                {
                    collider.GetComponent<Boss>().TakeDamage(damage);
                }

                if (collider.GetComponent<MiniMissile>() != null)
                {
                    collider.GetComponent<MiniMissile>().DestroyMissiles();
                }
            }
        }
    }
}