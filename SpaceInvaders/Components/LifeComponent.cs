namespace SpaceInvaders.Components
{
    public class LifeComponent : IComponent
    {
        public int Lives { get; set; }
        public bool IsAlive => Lives > 0;
        public bool IsShoot { get; set; }

        public bool Equals(IComponent other)
        {
            var lifeComponent = other as LifeComponent;
            return lifeComponent != null && Lives.GetHashCode() == lifeComponent.Lives.GetHashCode();
        }
    }
}