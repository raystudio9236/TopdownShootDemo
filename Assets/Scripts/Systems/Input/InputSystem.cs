using Entitas;
using UnityEngine;

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
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        ),
            Input.mousePosition,
            Input.GetMouseButton(0),
            Input.GetMouseButtonDown(0),
            Input.GetMouseButton(1),
            Input.GetMouseButtonDown(1));
    }
}