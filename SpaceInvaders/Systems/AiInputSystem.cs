using System;
using SpaceInvaders.Components;
using SpaceInvaders.EngineFiles;
using SpaceInvaders.Nodes;

namespace SpaceInvaders.Systems
{
    public class AiInputSystem : IPhysicsSystem
    {
        private CompositionNodes<AiComposition> _aiNodes;

        public void Update()
        {
        }

        //Récupération des noeuds
        public void Initialize(Engine gameInstance)
        {
            _aiNodes = gameInstance.WorldEntityManager.GetNodes<AiComposition>();
        }

        //Traitement
        public void Update(Engine gameEngine, double deltaT)
        {
            Initialize(gameEngine);

            var limitX = gameEngine.GameSize.Width;

            foreach (var node in _aiNodes.Nodes.ToArray())
            {
                //On test que le composant est du bon type au cas ou
                if (node.TypeComponent.TypeOfObject == TypeOfObject.Ai)
                {
                    //On effectue un déplacement linéaire et on augmente la fréquence des tirs et la vitesse horizontale à chaque allez retour
                    if (node.Position.X > limitX - node.Render.Image.Width)
                    {
                        foreach (var other in _aiNodes.Nodes.ToArray())
                        {
                            other.Physic.SpeedX = -other.Physic.SpeedX;
                            other.Physic.Move.X = other.Physic.SpeedX;
                            other.Position.Y += other.Physic.SpeedY;
                            IncreaseFireRate(other);
                            IncreaseSpeed(other);
                        }
                        break;
                    }

                    if (node.Position.X < 0)
                    {
                        foreach (var other in _aiNodes.Nodes.ToArray())
                        {
                            other.Physic.SpeedX = -other.Physic.SpeedX;
                            other.Physic.Move.X = other.Physic.SpeedX;
                            other.Position.Y += other.Physic.SpeedY;
                            IncreaseFireRate(other);
                            IncreaseSpeed(other);
                        }
                        break;
                    }

                    node.Physic.Move.X = node.Physic.SpeedX;
                }

                // On tir avec une probabilité définie qui augmente à chaque changement de direction
                // Un seul tir à la fois par ennemi
                var rdm = new Random();
                if (rdm.Next(1000) >= node.Enemy.FireProbability) continue;
                
                if (node.Fire.Entity == null || !node.Fire.Entity.GetComponent<LifeComponent>().IsAlive)
                    node.Fire.Entity = gameEngine.NewAiMissile(node);
            }
        }

        private static void IncreaseSpeed(AiComposition node)
        {
            node.Physic.SpeedX *= 1.05;
        }

        private static void IncreaseFireRate(AiComposition node)
        {
            node.Enemy.FireProbability += 1;
        }
    }
}