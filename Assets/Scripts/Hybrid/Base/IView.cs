using Entitas;

namespace Hybrid.Base
{
    public interface IView
    {
        void Link(Contexts contexts, IEntity entity);
    }

    public static class IViewEx
    {
        public static IPhysicsView AsPhysics(this IView view)
        {
            return view as IPhysicsView;
        }
    }
}