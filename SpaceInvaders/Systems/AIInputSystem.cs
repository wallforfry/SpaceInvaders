using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using SpaceInvaders.Nodes;

namespace SpaceInvaders
{
    public class AIInputSystem : IEngineSystem
    {
        public void Update()
        {
            
        }

        private CompositionNodes<AIComposition> _aiNodes;
        
        public void Initialize(Engine gameInstance)
        {
            _aiNodes= gameInstance.WorldEntityManager.GetNodes<AIComposition>();               
        }

        public void Update(Engine gameEngine)
        {
            Initialize(gameEngine);
            
            int limitX = gameEngine.GameSize.Width;
            int limitY = 400;
            
            foreach (var node in _aiNodes.Nodes.ToArray())
            {
                if (node.Physic.TypeOfObject == TypeOfObject.AI)
                {
                    if (node.Position.X > limitX - node.Render.Image.Width - 20)
                    {
                        foreach (var other in _aiNodes.Nodes.ToArray())
                        {
                            other.Physic.SpeedX = -other.Physic.SpeedX;
                            other.Physic.Move.X = other.Physic.SpeedX;
                        }                            
                        break;
                    }
                    
                    if (node.Position.X < 10)
                    {
                        foreach (var other in _aiNodes.Nodes.ToArray())
                        {
                            other.Physic.SpeedX = -other.Physic.SpeedX;
                            other.Physic.Move.X = other.Physic.SpeedX;
                        }
                        break;
                    }
                    
                    node.Physic.Move.X = node.Physic.SpeedX;                              
                }
                
            }        
        }
    }
}