using System;

namespace SpaceInvaders
{
    public class LifeComponent : IComponent
    {
        public int Lives { get; set; }        
        public bool IsAlive => Lives > 0;
        
        public bool Equals(IComponent other)
        {
            return Lives.GetHashCode() == ((LifeComponent) other).Lives.GetHashCode();
        }
    }
}