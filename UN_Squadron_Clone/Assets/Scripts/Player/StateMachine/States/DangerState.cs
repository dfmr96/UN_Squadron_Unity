using System;
using UnityEngine;

namespace Player
{
    public class DangerState : StateBase
    
    {
        private PlayerStateMachine playerStateMachine;
        private float invulnerabilityTime;
        private float invulnerabilityCounter = 0;
        private bool isInvulnerable = true;
        private float recoveryTime;
        private float recoveryCounter = 0;
        
        public DangerState(PlayerStateMachine stateMachine)
        {
            playerStateMachine = stateMachine;
        }
        public override void Enter()
        {
            invulnerabilityTime = playerStateMachine.Controller.InvulnerabilityTime;
            recoveryTime = playerStateMachine.Controller.RecoveryTime;
            playerStateMachine.Controller.Anim.SetInteger("PlayerState", 1);
            playerStateMachine.Controller.DamagedFlames.SetActive(true);
            AudioManager.instance.playerDamaged.Play();
            AudioManager.instance.playerRecovery.Play();
            isInvulnerable = true;
            
            Debug.Log("Danger State enter");
        }

        public override void Update()
        {
            InvulnerabilityTimer();

            RecoverTimer();
        }

        private void RecoverTimer()
        {
            recoveryCounter += Time.deltaTime;

            if (recoveryCounter >= recoveryTime)
            {
                Debug.Log("Recovered");
                recoveryCounter = 0;
                playerStateMachine.ChangeStateTo(playerStateMachine.HealthyState);
            }
        }

        private void InvulnerabilityTimer()
        {
            invulnerabilityCounter += Time.deltaTime;

            if (invulnerabilityCounter >= invulnerabilityTime)
            {
                invulnerabilityCounter = 0;
                isInvulnerable = false;
            }
        }

        public override void Exit()
        {
            PlayerController controller = playerStateMachine.Controller;
            
            EventBus.instance.PlayerRecovered();
            controller.DamagedFlames.SetActive(false);
            controller.Anim.SetInteger("PlayerState", 0);
            controller.Rb.velocity = Vector3.zero;
            
            Debug.Log("Danger State exit");
        }
    }
}