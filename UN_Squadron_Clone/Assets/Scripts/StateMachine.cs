using System;
using UnityEngine;

namespace Player
{
    public class StateMachine
    {
        [field: SerializeField] public IState CurrentState { get; protected set; }
        [field: SerializeField] public string StateString { get; protected set; }
        public event Action<IState> OnStateChanged;

        public void Initialize(IState state)
        {
            CurrentState = state;
            state.Enter();
            StateString = CurrentState.ToString();
            
            OnStateChanged?.Invoke(state);
        }

        public void ChangeStateTo(IState nextState)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            nextState.Enter();
            StateString = CurrentState.ToString();
            
            OnStateChanged?.Invoke(nextState);
        }

        public void Update()
        {
            if (CurrentState != null)
            {
                CurrentState.Update();
            }
        }
    }

    public interface IState
    {
        public string StateName { get; }
        public void Enter();
        public void Update();
        public void Exit();
    }
}
