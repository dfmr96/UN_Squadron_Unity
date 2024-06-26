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
        [field: SerializeField] public PlayerController Controller { get; private set; }
        
        public PlayerStateMachine(PlayerController controller)
        {
            Controller = controller;
            HealthyState = new HealthyState(this);
            DangerState = new DangerState(this);
            CriticalState = new CriticalState(this);
            DestroyedState = new DestroyedState(this);
            Initialize(HealthyState);
        }
    }
}