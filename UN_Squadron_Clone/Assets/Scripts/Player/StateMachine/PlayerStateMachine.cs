using System;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PlayerStateMachine : StateMachine
    {
        [field: SerializeField] public HealthyState HealthyState { get; private set; }
        [field: SerializeField] public DangerState DangerState { get; private set; }
        [field: SerializeField] public CriticalState CriticalState { get; private set; }
        [field: SerializeField] public DestroyedState DestroyedState { get; private set; }
        [field: SerializeField] public CharacterController Controller { get; private set; }
        
        public PlayerStateMachine(CharacterController controller)
        {
            Controller = controller;
            HealthyState = new HealthyState();
            DangerState = new DangerState();
            CriticalState = new CriticalState();
            DestroyedState = new DestroyedState();
            Initialize(HealthyState);
        }
    }
}