using UnityEngine;

namespace Core
{
    public class Explosion : MonoBehaviour
    {
        public void DestroyEnemy()
        {
            Destroy(gameObject);
        }
    }
}