using SpaceInvaders.EngineFiles;

namespace SpaceInvaders.Components
{
    public class TypeComponent : IComponent
    {
        public TypeOfObject TypeOfObject { get; set; }

        public bool Equals(IComponent other)
        {
            var typeComponent = other as TypeComponent;
            return typeComponent != null && TypeOfObject.GetHashCode() == typeComponent.TypeOfObject.GetHashCode();
        }
    }
}