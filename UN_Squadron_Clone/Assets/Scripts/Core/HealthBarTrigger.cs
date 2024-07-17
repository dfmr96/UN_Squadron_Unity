using UI;
using UnityEngine;

namespace Core
{
    public class HealthBarTrigger : MonoBehaviour
    {
        [SerializeField] private UIGameplayManager UIGameplayManager;
        public void HealthBarToNormal()
        {
            UIGameplayManager.HealthBarBackToNormal();
        }
    }
}
