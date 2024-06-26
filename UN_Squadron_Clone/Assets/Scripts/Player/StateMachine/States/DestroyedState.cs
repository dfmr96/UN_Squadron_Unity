using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class DestroyedState : StateBase
    {
        private PlayerStateMachine playerStateMachine;

        private float gameOverTime = 2;
        private float gameOverCounter = 0;
        public DestroyedState(PlayerStateMachine stateMachine)
        {
            playerStateMachine = stateMachine;
        }
        public override void Enter()
        {
            //StopAllCoroutines();
            playerStateMachine.Controller.Anim.SetInteger("PlayerState", 3); //TODO Magic number
            playerStateMachine.Controller.DamagedFlames.SetActive(false); //TODO Extraer sprite y animaciones
            EventBus.instance.PlayerDestroyed();
            AudioManager.instance.playerRecovery.Stop();
            AudioManager.instance.bgmAudio.Stop();
            AudioManager.instance.playerDestroyed.Play();
        }

        public override void Update()
        {
            gameOverCounter += Time.deltaTime;

            if (gameOverCounter > gameOverTime)
            {
                gameOverCounter = 0;
                GameManager.instance.GameOver();
            }
        }

        public override void Exit()
        {
            //throw new NotImplementedException();
        }
        
        
    }
}