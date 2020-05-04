using Entitas;

namespace Systems.Input
{
    public class InputCleanupSystem : ICleanupSystem
    {
        private readonly Contexts _contexts;

        public InputCleanupSystem(Contexts contexts)
        {
            _contexts = contexts;
        }

        public void Cleanup()
        {
            _contexts.input.DestroyAllEntities();
        }
    }
}
