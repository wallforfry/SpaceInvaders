using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms.VisualStyles;
using SpaceInvaders.Nodes;

namespace SpaceInvaders
{
    public class MovablePhysicsSystem : IPhysicsSystem
    {
        public void Update()
        {

        }

        private CompositionNodes<MovableComposition> _movableNodes;

        public void Initialize(Engine gameInstance)
        {
            _movableNodes = gameInstance.WorldEntityManager.GetNodes<MovableComposition>();
        }

        public void Update(Engine gameEngine, double deltaT)
        {                  
            Initialize(gameEngine);
            foreach (var node in _movableNodes.Nodes.ToArray())
            {
               /* if (node.Position.X > 1 &&
                    node.Position.X < gameEngine.GameSize.Width - node.Render.Image.Width -1)
                {*/
                    node.Position.X += node.Physic.Move.X;
                /*}
                if (node.Position.Y >= 0 &&
                    node.Position.Y < gameEngine.GameSize.Height - node.Render.Image.Height)
                {*/
                    node.Position.Y += node.Physic.Move.Y;
                //}
                if (node.Position.Y > gameEngine.GameSize.Height || node.Position.Y < 0)
                    //gameEngine.WorldEntityManager.DestroyEntity(node.Owner.Id);
                    node.Life.Lives = 0;
            }
        }
    }
}