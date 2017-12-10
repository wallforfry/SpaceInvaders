using System;
using System.Drawing;
using SpaceInvaders.EngineFiles;
using SpaceInvaders.Nodes;

namespace SpaceInvaders.Systems
{
    //Classe non utilisé mais prévue pour générer plusieurs blocs d'ennemis à la suite
    public class EnemyGenerationSystem : IRenderSystem
    {
        private CompositionNodes<AiComposition> _enemyNodes;

        public void Update()
        {
        }

        public void Initialize(Engine gameInstance)
        {
            _enemyNodes = gameInstance.WorldEntityManager.GetNodes<AiComposition>();
        }

        public void Update(Engine gameInstance, Graphics graphics)
        {
            Initialize(gameInstance);

            var topLeftEnemy = FindTopLeftEnemy(gameInstance);

            if (topLeftEnemy.Position.X < 10)
                gameInstance.EnemyLine(topLeftEnemy.Position.X, 0, topLeftEnemy.Physic.SpeedX);
        }

        private AiComposition FindTopLeftEnemy(Engine gameInstance)
        {
            AiComposition topLeftEnemy = null;
            double minX = gameInstance.GameSize.Width;
            double minY = gameInstance.GameSize.Height;

            foreach (var node in _enemyNodes.Nodes.ToArray())
                if (node.Position.X <= minX && node.Position.Y <= minY)
                {
                    topLeftEnemy = node;
                    minX = node.Position.X;
                    minY = node.Position.Y;
                }
            if (minX < 5)
                Console.WriteLine(minX + " : " + minY);

            return topLeftEnemy;
        }
    }
}