using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Input]
[Unique]
public sealed class InputComp : IComponent
{
    public Vector2 Dir;
    public Vector2 MousePos;
    public bool MainButton; // 主要按钮
    public bool SecondaryButton; // 次要按钮
}