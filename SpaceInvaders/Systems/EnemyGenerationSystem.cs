using System;
using System.Drawing;
using System.Threading;
using SpaceInvaders.Nodes;

namespace SpaceInvaders
{
    
    //Classe non utilisé mais prévue pour générer plusieurs blocs d'ennemis à la suite
    public class EnemyGenerationSystem : IRenderSystem
    {
        
        private CompositionNodes<AIComposition> _enemyNodes;
        private int enemyApparitionLimit = 57;

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
            
            AIComposition topLeftEnemy = FindTopLeftEnemy(gameInstance);

            if (topLeftEnemy.Position.X  < 10)
            {
              gameInstance.EnemyLine(topLeftEnemy.Position.X, 0, topLeftEnemy.Physic.SpeedX);              
            }
        
        }

        private AIComposition FindTopLeftEnemy(Engine gameInstance)
        {
            AIComposition topLeftEnemy = null;
            double minX = gameInstance.GameSize.Width;
            double minY = gameInstance.GameSize.Height;
            
            foreach (var node in _enemyNodes.Nodes.ToArray())
            {
                if (node.Position.X <= minX && node.Position.Y <= minY)
                {
                    topLeftEnemy = node;
                    minX = node.Position.X;
                    minY = node.Position.Y;
                }    
            }
            if(minX < 5)
                Console.WriteLine(minX+" : "+minY);

            return topLeftEnemy;
        }
    }
}