namespace SpaceInvaders
{
    public class EnemyBlockComponent : IComponent
    {
        public int FireProbability { get; set; }
        
        public bool Equals(IComponent other)
        {
            return FireProbability.GetHashCode() == ((EnemyBlockComponent) other).FireProbability.GetHashCode();
        }
    }
}