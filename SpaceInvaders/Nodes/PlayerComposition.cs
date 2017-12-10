using SpaceInvaders.Components;

namespace SpaceInvaders.Nodes
{
    public class PlayerComposition : CompositionBase
    {
        public RenderComponent Render { get; set; }
        public PositionComponent Position { get; set; }
        public PhysicsComponent Physic { get; set; }
        public LifeComponent Life { get; set; }
        public FireComponent Fire { get; set; }
        public TypeComponent TypeComponent { get; set; }
    }
}