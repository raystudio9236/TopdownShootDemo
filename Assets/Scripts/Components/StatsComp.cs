using System;
using Entitas;

[Serializable]
public enum VarFlag : short
{
    Velocity, // 速度
    AttackSpeed, // 攻击速度
    BulletCount, // 子弹数量
    BulletSpace, // 子弹之间的间隔
    All,
}

public static class VarFlagExtension
{
    public static short ToIdx(this VarFlag flag)
    {
        return (short) flag;
    }
}

public sealed class StatsComp : IComponent
{
    public float[] Vars;
}

public static class StatsCompEx
{
    public static GameEntity SetStat(this GameEntity gameEntity,
        VarFlag varFlag,
        float value)
    {
        gameEntity.statsComp.Vars[varFlag.ToIdx()] = value;
        return gameEntity;
    }
}