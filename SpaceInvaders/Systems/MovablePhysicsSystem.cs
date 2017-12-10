using SpaceInvaders.EngineFiles;
using SpaceInvaders.Nodes;

namespace SpaceInvaders.Systems
{
    //Gère la la physique du jeu (déplacement) des entités
    public class MovablePhysicsSystem : IPhysicsSystem
    {
        private CompositionNodes<MovableComposition> _movableNodes;

        public void Update()
        {
        }

        public void Initialize(Engine gameInstance)
        {
            _movableNodes = gameInstance.WorldEntityManager.GetNodes<MovableComposition>();
        }

        public void Update(Engine gameEngine, double deltaT)
        {
            Initialize(gameEngine);
            foreach (var node in _movableNodes.Nodes.ToArray())
            {
                node.Position.X += node.Physic.Move.X;
                node.Position.Y += node.Physic.Move.Y;

                if (node.Position.Y > gameEngine.GameSize.Height || node.Position.Y < 0)
                    node.Life.Lives = 0;
            }
        }
    }
}