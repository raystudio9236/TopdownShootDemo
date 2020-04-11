using Entitas;
using UnityEngine;

[Input]
public sealed class InputComp : IComponent
{
    public Vector2 Dir;
    public Vector2 MousePos;
}