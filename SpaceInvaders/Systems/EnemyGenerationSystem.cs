using System;
using System.Drawing;
using SpaceInvaders.Nodes;

namespace SpaceInvaders
{
    public class EnemyGenerationSystem : IRenderSystem
    {
        
        private CompositionNodes<AIComposition> _enemyNodes;
        private int enemyApparitionLimit = 40;

        public void Update()
        {
            
        }

        public void Initialize(Engine gameInstance)
        {
            _enemyNodes = gameInstance.WorldEntityManager.GetNodes<AIComposition>();
        }

        public void Update(Engine gameInstance, Graphics graphics)
        {
            Initialize(gameInstance);
            
            bool addBlock = false;
            int minPositionX = gameInstance.GameSize.Width;
            double positionY = 0;
            double speedX = 0.0;
            
            foreach (var node in _enemyNodes.Nodes.ToArray())
            {                                
                if (node.Position.Y > enemyApparitionLimit && node.Position.X < 1)
                {
                    addBlock = true;
                    minPositionX = (int) node.Position.X;
                    speedX = node.Physic.SpeedX;
                    positionY = node.Position.Y;
                    break;                    
                }    
            }

            if (addBlock)
            {
                //Add line
                gameInstance.EnemyLine(minPositionX,positionY, speedX);
                return;
            }
        }
    }
}