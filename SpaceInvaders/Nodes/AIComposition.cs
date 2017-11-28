namespace SpaceInvaders.Nodes
{
    public class AIComposition : CompositionBase
    {
        public RenderComponent Render { get; set; }
        public PositionComponent Position { get; set; }       
        public PhysicsComponent Physic { get; set; }
        public LifeComponent Life { get; set; }
        public FireComponent Fire { get; set; }
        public EnemyBlockComponent Enemy { get; set; }
        public TypeComponent TypeComponent { get; set; }
    }
}