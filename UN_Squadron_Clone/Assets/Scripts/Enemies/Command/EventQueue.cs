using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Command
{
    public class EventQueue : MonoBehaviour
    {
        private List<ICommand> currentCommands = new();
        public static EventQueue Instance { get; private set; }
        private static EventQueue _instance;

        public static EventQueue GetInstance()
        {
            return _instance;
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void EnqueueCommand(ICommand command)
        {
            currentCommands.Add(command);
        }
    
        private void LateUpdate()
        {
            if (currentCommands.Count == 0)
                return;

            foreach (var command in currentCommands)
            {
                command.Execute();
            }

            currentCommands.Clear();
        }
    
    }
}