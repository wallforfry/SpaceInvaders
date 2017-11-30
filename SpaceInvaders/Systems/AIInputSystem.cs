using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using SpaceInvaders.Nodes;

namespace SpaceInvaders
{
    public class AIInputSystem : IPhysicsSystem
    {
        public void Update()
        {
            
        }

        private CompositionNodes<AIComposition> _aiNodes;
        
        public void Initialize(Engine gameInstance)
        {
            _aiNodes= gameInstance.WorldEntityManager.GetNodes<AIComposition>();               
        }

        public void Update(Engine gameEngine, double deltaT)
        {
            Initialize(gameEngine);
            
            int limitX = gameEngine.GameSize.Width;
            int limitY = 400;
            
            foreach (var node in _aiNodes.Nodes.ToArray())
            {
                if (node.TypeComponent.TypeOfObject == TypeOfObject.AI)
                {
                    if (node.Position.X > limitX - node.Render.Image.Width)
                    {
                        foreach (var other in _aiNodes.Nodes.ToArray())
                        {
                            other.Physic.SpeedX = -other.Physic.SpeedX;
                            other.Physic.Move.X = other.Physic.SpeedX;
                            other.Position.Y += other.Physic.SpeedY;
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
                        }
                        break;
                    }
                    
                    node.Physic.Move.X = node.Physic.SpeedX;                              
                }

                Random rdm = new Random();
                if (rdm.Next(1000) < node.Enemy.FireProbability)
                {
                    if (node.Fire.Entity == null || !node.Fire.Entity.GetComponent<LifeComponent>().IsAlive)
                    {
                        node.Fire.Entity = gameEngine.newAIMissile(node);
                    }
                }

            }        
        }
    }
}