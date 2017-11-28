namespace SpaceInvaders
{
    public class PhysicsComponent : IComponent
    {                
        public Vecteur2D Vector { get; set; }       
        public double SpeedX { get { return Vector.X; } set { Vector.X = value; }}
        public double SpeedY { get { return Vector.Y; } set { Vector.Y = value; }}
        public Vecteur2D Move { get; set; }
        
        public bool Equals(IComponent other)
        {
            return Vector.GetHashCode() == ((PhysicsComponent) other).Vector.GetHashCode();
        }
    }
}