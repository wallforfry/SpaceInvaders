using SpaceInvaders.EngineFiles;

namespace SpaceInvaders.Systems
{
    public interface IPhysicsSystem : ISystem
    {
        void Update(Engine gameEngine, double deltaT);
    }
}