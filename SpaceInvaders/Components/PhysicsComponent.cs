namespace SpaceInvaders
{
    public class PhysicsComponent : IComponent
    {
        public TypeOfObject TypeOfObject { get; set; }
        public Vecteur2D Vector { get; set; }       
        public double SpeedX { get { return Vector.X; } set { Vector.X = value; }}
        public double SpeedY { get { return Vector.Y; } set { Vector.Y = value; }}
        
        public bool Equals(IComponent other)
        {
            return Vector.GetHashCode() == ((PhysicsComponent) other).Vector.GetHashCode() &&
                   TypeOfObject.GetHashCode() == ((PhysicsComponent) other).TypeOfObject.GetHashCode();
        }
    }
}