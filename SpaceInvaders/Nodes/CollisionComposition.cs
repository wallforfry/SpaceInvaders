namespace SpaceInvaders.Nodes
{
    public class CollisionComposition : CompositionBase
    {
        public RenderComponent Render { get; set; }
        public PositionComponent Position { get; set; }   
        public LifeComponent Life { get; set; }
        public TypeComponent TypeComponent { get; set; }
    }
}