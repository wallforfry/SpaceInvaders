using System.Drawing;
using SpaceInvaders.EngineFiles;

namespace SpaceInvaders.Systems
{
    public interface IRenderSystem : ISystem
    {
        void Update(Engine gameInstance, Graphics graphics);
    }
}