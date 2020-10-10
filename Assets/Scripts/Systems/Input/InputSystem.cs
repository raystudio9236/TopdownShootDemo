using Entitas;
using UnityEngine;

namespace Systems.Input
{
    public class InputSystem : IExecuteSystem
    {
        private readonly Contexts _contexts;

        public InputSystem(Contexts contexts)
        {
            _contexts = contexts;
        }

        public void Execute()
        {
            var playerInputEntity = _contexts.input.CreateEntity();
            playerInputEntity.AddInputComp(new Vector2(
                    UnityEngine.Input.GetAxis("Horizontal"),
                    UnityEngine.Input.GetAxis("Vertical")
                ),
                UnityEngine.Input.mousePosition,
                UnityEngine.Input.GetMouseButton(0),
                UnityEngine.Input.GetMouseButtonDown(0),
                UnityEngine.Input.GetMouseButton(1),
                UnityEngine.Input.GetMouseButtonDown(1));
        }
    }
}