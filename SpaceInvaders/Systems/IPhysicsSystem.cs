using System.Data;

namespace SpaceInvaders
{
    public interface IPhysicsSystem : ISystem
    {
        void Update(Engine gameEngine, double deltaT);
        
    }
}