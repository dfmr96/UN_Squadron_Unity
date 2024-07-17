using UnityEngine;

namespace UI
{
    public class PortraitTrigger : MonoBehaviour
    {
        [SerializeField] private UIGameplayManager UIGameplayManager;
        public void ChangeAnimation()
        {
            UIGameplayManager.PlayPortraitRecovery();
        }
    }
}
