
using Entitas;

public enum VarFlag : short
{
    Velocity, // 速度  
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
