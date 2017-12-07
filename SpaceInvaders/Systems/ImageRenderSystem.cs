using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using SpaceInvaders.Nodes;

namespace SpaceInvaders
{
    //Effectue le rendu des entités possèdant une image
    public class ImageRenderSystem : IRenderSystem
    {
        private CompositionNodes<RenderComposition> _renderNodes;
        
        public void Initialize(Engine gameInstance)
        {
            _renderNodes = gameInstance.WorldEntityManager.GetNodes<RenderComposition>();
   
        }
        
        public void Update(Engine gameInstance, Graphics graphics)
        {
            Initialize(gameInstance);
            
            foreach (var node in _renderNodes.Nodes.ToArray())
            {
                if (node.Life.IsAlive)
                {
                    graphics.DrawImage(node.Render.Image, (float) node.Position.X, (float) node.Position.Y);                    
                }
            }                                                           
        }

        public void Update()
        {
            
        }
    }
}