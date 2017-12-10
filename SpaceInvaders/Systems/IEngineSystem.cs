using System.Drawing;
using SpaceInvaders.EngineFiles;

namespace SpaceInvaders.Systems
{
    public interface IEngineSystem : ISystem
    {
        void Update(Engine gameEngine, Graphics graphics);
    }
}