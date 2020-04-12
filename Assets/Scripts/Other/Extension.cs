using UnityEngine;

public static class Extension
{
    public static Vector2 Angle2Vector2D(this float angle)
    {
        var a = (angle + 90) * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(a), Mathf.Sin(a));
    }
}