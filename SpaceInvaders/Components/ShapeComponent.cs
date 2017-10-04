using System.Drawing;

namespace SpaceInvaders
{
    public class ShapeComponent : IComponent
    {        
        public int Radius { get; set; }        
        public Pen Pen
        {
            get { return (new Pen(Color.Black)); }
            private set { }
        }

        public bool Equals(IComponent other)
        {
            return Radius.GetHashCode() == ((ShapeComponent) other).Radius.GetHashCode();
        }
    }
}