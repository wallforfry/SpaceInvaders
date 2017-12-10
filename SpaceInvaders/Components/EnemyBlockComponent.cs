namespace SpaceInvaders.Components
{
    public class EnemyBlockComponent : IComponent
    {
        public int FireProbability { get; set; }

        public bool Equals(IComponent other)
        {
            var enemyBlockComponent = other as EnemyBlockComponent;
            return enemyBlockComponent != null && FireProbability.GetHashCode() == enemyBlockComponent.FireProbability.GetHashCode();
        }
    }
}