using Entitas;
using Entitas.Unity;
using UnityEngine;

public class View : MonoBehaviour, IView
{
    public void Link(Contexts contexts, IEntity entity)
    {
        gameObject.Link(entity);
    }
}