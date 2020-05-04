using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Components.Input
{
    [Input]
    [Unique]
    public sealed class InputComp : IComponent
    {
        public Vector2 Dir;
        public Vector2 MousePos;
        public bool MainButton; // 主要按钮
        public bool MainButtonDown; // 主要按钮按下
        public bool SecondaryButton; // 次要按钮
        public bool SecondaryButtonDown; // 次要按钮按下
    }
}