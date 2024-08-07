using UnityEngine;

namespace Core
{
    public class SideScrollController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [field: SerializeField] public BoxCollider2D Col { get; private set; }

        private void Start()
        {
            Application.targetFrameRate = 60;
        }
        private void Update()
        {
            transform.Translate(Vector3.right * (speed * Time.deltaTime));
        }

        public void PauseScroll()
        {
            speed = 0;
        }
    }
}
