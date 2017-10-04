using System.Dynamic;

namespace SpaceInvaders
{
    public class PositionComponent : IComponent
    {
        public Vecteur2D Position { get; set; }

        public double X
        {
            get { return Position.X; }
            set { Position.X = value; }
        }

        public double Y
        {
            get { return Position.Y; }
            set { Position.Y = value; }
        }

        public bool Equals(IComponent other)
        {
            return Position.GetHashCode() == ((PositionComponent) other).Position.GetHashCode();
        }
    }
            
}