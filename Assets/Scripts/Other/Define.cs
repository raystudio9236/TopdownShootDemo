using System;
using Hybrid.Base;
using UnityEngine;
using Utils.Pool;

namespace Other
{
    [Serializable]
    public enum ActorTag
    {
        Player,
        Bullet,
        Enemy,
        Coin,
        PlayerShadow,
        None,
    }

    [Serializable]
    public class ActorTagPathDic : SerializableDictionary<ActorTag, string>
    {
    }

    [Serializable]
    public class ViewPrefabPool : PrefabPool<View>
    {
    }

    [Serializable]
    public struct CollisionInfo
    {
        public int SourceId;
        public int OtherId;
    }

    [Serializable]
    public enum PlayerInput
    {
        MainButton,
        SecondaryButton,
    }

    [Serializable]
    public enum PlayerInputType
    {
        Keep,
        Down,
        Up,
    }

    [Serializable]
    public enum PhysicsTag
    {
        Player,
        Bullet,
        Enemy,
        Item,
        Background,
    }

    public static class PhysicsTagValue
    {
        public static readonly int Player =
            LayerMask.NameToLayer("Player");

        public static readonly int Bullet =
            LayerMask.NameToLayer("Bullet");

        public static readonly int Enemy =
            LayerMask.NameToLayer("Enemy");

        public static readonly int Item =
            LayerMask.NameToLayer("Item");

        public static readonly int Background =
            LayerMask.NameToLayer("Background");

        public static int ToValue(this PhysicsTag physicsTag)
        {
            switch (physicsTag)
            {
                case PhysicsTag.Player:
                    return Player;
                case PhysicsTag.Bullet:
                    return Bullet;
                case PhysicsTag.Enemy:
                    return Enemy;
                case PhysicsTag.Item:
                    return Item;
                case PhysicsTag.Background:
                    return Background;
            }

            return 0;
        }
    }
}