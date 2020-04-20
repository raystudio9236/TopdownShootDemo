using System;

[Serializable]
public enum ActorTag
{
    Player,
    Bullet,
    Enemy,
    Coin,
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