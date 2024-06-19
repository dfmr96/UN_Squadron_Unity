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
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }
    }
}