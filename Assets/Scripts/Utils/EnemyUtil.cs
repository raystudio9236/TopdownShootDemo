using UnityEngine;

public static class EnemyUtil
{
    public static void CreateCoin(GameEntity entity)
    {
        int count = Random.Range(3, 5);
        var pos = entity.posComp.Value;

        for (var i = 0; i < count; i++)
        {
            var x = Random.Range(-1f, 1f);
            var y = Random.Range(-1f, 1f);

            EntityUtil.CreateCoinEntity(GameManager.Contexts,
                pos + new Vector2(x, y),
                0f);
        }
    }
}