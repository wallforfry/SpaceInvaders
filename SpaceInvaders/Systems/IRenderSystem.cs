using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace SpaceInvaders
{
    public interface IRenderSystem : ISystem
    {
        void Update(Engine gameInstance, Graphics graphics);
    }
}