namespace SpaceInvaders
{
    public class FireComponent : IComponent
    {
        public Entity Entity{ get; set; }
        
        public bool Equals(IComponent other)
        {
            return Entity.GetHashCode() == ((FireComponent) other).Entity.GetHashCode();
        }
    }
}