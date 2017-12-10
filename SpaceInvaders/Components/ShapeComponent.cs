using System.Drawing;

namespace SpaceInvaders.Components
{
    public class ShapeComponent : IComponent
    {
        public int Radius { get; set; }

        public Pen Pen
        {
            get { return new Pen(Color.Black); }
            private set { }
        }

        public bool Equals(IComponent other)
        {
            var shapeComponent = other as ShapeComponent;
            return shapeComponent != null && Radius.GetHashCode() == shapeComponent.Radius.GetHashCode();
        }
    }
}