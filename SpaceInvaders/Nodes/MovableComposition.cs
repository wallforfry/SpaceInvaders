namespace SpaceInvaders.Nodes
{
    public class MovableComposition : CompositionBase
    {
        public RenderComponent Render { get; set; }
        public PositionComponent Position { get; set; }       
        public PhysicsComponent Physic { get; set; }
        public LifeComponent Life { get; set; }
    }
}