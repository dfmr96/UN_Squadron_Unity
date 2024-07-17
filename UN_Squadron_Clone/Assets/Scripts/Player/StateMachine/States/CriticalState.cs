using System;

namespace Player
{
    public class CriticalState : StateBase
    {
        private PlayerStateMachine playerStateMachine;
        public CriticalState(PlayerStateMachine stateMachine)
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