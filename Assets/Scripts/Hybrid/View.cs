using Entitas;
using Entitas.Unity;
using UnityEngine;

public class View : MonoBehaviour, IView, IDestroyFlagListener
{
    protected GameEntity _selfEntity => 
        gameObject.GetEntityLink().entity as GameEntity;
    
    public void Link(Contexts contexts, IEntity entity)
    {
        gameObject.Link(entity);
        
        var gameEntity = (GameEntity) entity;
        gameEntity.AddDestroyFlagListener(this);

        transform.position = gameEntity.posComp.Value;
        transform.rotation = Quaternion.Euler(0, 
            0, 
            gameEntity.rotComp.Angle);

        OnLinkEntityHandler();
    }

    public void OnDestroyFlag(GameEntity entity)
    {
        gameObject.Unlink();
        
        OnDestroyEntityHandler();
    }

    protected virtual void OnLinkEntityHandler()
    {
    }

    protected virtual void OnDestroyEntityHandler()
    {
    }
}