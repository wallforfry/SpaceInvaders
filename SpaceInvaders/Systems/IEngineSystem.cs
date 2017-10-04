using System.Drawing;

namespace SpaceInvaders
{
    public interface IEngineSystem : ISystem
    {
        void Update(Engine gameEngine);
    }
}