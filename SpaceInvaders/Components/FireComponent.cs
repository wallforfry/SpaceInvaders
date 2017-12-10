using SpaceInvaders.EngineFiles;

namespace SpaceInvaders.Components
{
    public class FireComponent : IComponent
    {
        public Entity Entity { get; set; }

        public bool Equals(IComponent other)
        {
            var fireComponent = other as FireComponent;
            return fireComponent != null && Entity.GetHashCode() == fireComponent.Entity.GetHashCode();
        }
    }
}