using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AddViewSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    
    public AddViewSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }
    
    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.CreateGameObjCmdComp);
    }
    
    protected override bool Filter(GameEntity entity)
    {
        return true;
    }
    
    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var view = SpawnObj(entity);
            entity.AddViewComp(view);
            entity.RemoveCreateGameObjCmdComp();
        }
    }

    private View SpawnObj(GameEntity gameEntity)
    {
        var path = gameEntity.createGameObjCmdComp.Path;
        var prefab = Resources.Load<GameObject>(path);
        View view = null;
        if (path == "Bullet")
        {
            view = PoolManager.Instance.BulletPrefabPool.Spawn();
        }
        else
        {
            var obj = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            view = obj.GetComponent<View>();
        }

        view.Link(_contexts, gameEntity);
        return view;
    }
}
