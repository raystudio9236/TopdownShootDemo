using System;
using Components.Base;
using Entitas;
using UnityEngine;

namespace Other
{
    public static class Extension
    {
        public static Vector2 Angle2Vector2D(this float angle)
        {
            var a = (angle + 90) * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(a), Mathf.Sin(a));
        }

        public static float Vector2Angle2D(this Vector2 dir)
        {
            return Vector2.SignedAngle(Vector2.up, dir);
        }

        public static float Vector2Angle2D(this Vector3 dir)
        {
            return Vector2.SignedAngle(Vector2.up, dir);
        }

        public static RectTransform RectTf(this Transform tf)
        {
            return tf as RectTransform;
        }
    }

    public static class ContextsIdExtensions
    {
        public static void SubscribeId(this Contexts contexts)
        {
            foreach (var context in contexts.allContexts)
            {
                if (Array.FindIndex(context.contextInfo.componentTypes,
                    v => v == typeof(IdComp)) >= 0)
                {
                    context.OnEntityCreated += AddId;
                }
            }
        }

        public static void AddId(IContext context, IEntity entity)
        {
            (entity as IIdCompEntity).ReplaceIdComp(entity.creationIndex);
        }
    }

    public static class GameEntityEx
    {
        public static bool IsValid(this GameEntity gameEntity)
        {
            return gameEntity != null
                   && gameEntity.isEnabled
                   && gameEntity.hasIdComp
                   && !gameEntity.isDestroyFlag;
        }
    }
}