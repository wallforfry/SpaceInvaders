namespace SpaceInvaders
{
    public class EnemyBlockComponent : IComponent
    {
        public int NumberOfEnemy { get; set; }
        public int PositionInLine { get; set; }
        public Vecteur2D Size { get; set; }
        public Vecteur2D Position { get; set; }
        public int FireProbability { get; set; }
        
        public bool Equals(IComponent other)
        {
            return NumberOfEnemy.GetHashCode() == ((EnemyBlockComponent) other).NumberOfEnemy.GetHashCode() &&
                   PositionInLine.GetHashCode() == ((EnemyBlockComponent) other).PositionInLine.GetHashCode() ;
        }
    }
}