using UnityEngine;

namespace Weapons
{
    public class MegaCrushRayLogic : MonoBehaviour
    {
        [SerializeField] Vector3 angle;

        private void Start()
        {
            Destroy(gameObject, 2f);
        }
        private void Update()
        {
            transform.Translate(angle.normalized * 50 * Time.deltaTime);
        }
    }
}
