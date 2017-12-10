using SpaceInvaders.EngineFiles;

namespace SpaceInvaders.Systems
{
    public interface ISystem
    {
        void Update();
        void Initialize(Engine gameInstance);
    }
}