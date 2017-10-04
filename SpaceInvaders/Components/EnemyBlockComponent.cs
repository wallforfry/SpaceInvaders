namespace SpaceInvaders
{
    public class EnemyBlockComponent : IComponent
    {
        public int NumberOfEnemy { get; set; }
        public int PositionInLine { get; set; }
        
        public bool Equals(IComponent other)
        {
            return NumberOfEnemy.GetHashCode() == ((EnemyBlockComponent) other).NumberOfEnemy.GetHashCode() &&
                   PositionInLine.GetHashCode() == ((EnemyBlockComponent) other).PositionInLine.GetHashCode();
        }
    }
}