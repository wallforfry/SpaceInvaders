namespace SpaceInvaders.Nodes
{
    // Les sous-types ajoutent des champs héritant de IComponent
    public class RenderComposition : CompositionBase
    {
        public RenderComponent Render { get; set; }
        public PositionComponent Position { get; set; }
        public LifeComponent Life { get; set; }
    }
}