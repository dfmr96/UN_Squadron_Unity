using System;

namespace Player
{
    public class HealthyState : StateBase
    {
        private PlayerStateMachine playerStateMachine;
        public HealthyState(PlayerStateMachine stateMachine)
        {
            playerStateMachine = stateMachine;
        }
        
        public override void Enter()
        {
        }

        public override void Update()
        {
        }

        public override void Exit()
        {
        }
    }
}