using System;
using Utils;

[Serializable]
public enum ActorTag
{
    Player,
    Bullet,
    Enemy,
    Coin,
    PlayerShadow,
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