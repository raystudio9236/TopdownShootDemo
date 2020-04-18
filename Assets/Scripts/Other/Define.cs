using System;

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