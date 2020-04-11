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
            var obj = SpawnObj(entity);
            entity.AddViewComp(obj);
            entity.RemoveCreateGameObjCmdComp();
        }
    }

    private GameObject SpawnObj(GameEntity gameEntity)
    {
        var path = gameEntity.createGameObjCmdComp.Path;
        var prefab = Resources.Load<GameObject>(path);
        var obj = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        var view = obj.GetComponent<View>();
        view.Link(_contexts, gameEntity);
        return obj;
    }
}
