using System;

namespace SpaceInvaders
{
    public class TypeComponent : IComponent
    {
        public TypeOfObject TypeOfObject { get; set; }
        
        public bool Equals(IComponent other)
        {
            return TypeOfObject.GetHashCode() == ((TypeComponent) other).TypeOfObject.GetHashCode();
        }
    }
}