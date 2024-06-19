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
           // throw new NotImplementedException();
        }

        public override void Update()
        {
            //throw new NotImplementedException();
        }

        public override void Exit()
        {
            //throw new NotImplementedException();
        }
    }
}