using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using SpaceInvaders.Nodes;

namespace SpaceInvaders
{
    public class ImageRenderSystem : IRenderSystem
    {
        private CompositionNodes<RenderComposition> _renderNodes;
        
        public void Initialize(Engine gameInstance)
        {
            _renderNodes = gameInstance.WorldEntityManager.GetNodes<RenderComposition>();
        }
        
        
        public void Update(Engine gameInstance, Graphics graphics)
        {
            /*foreach (var entity in gameInstance.getEntity())
            {
                RenderComponent renderComponent = null;
                PositionComponent positionComponent = null;
                LifeComponent lifeComponent = null;     
                
                renderComponent = entity.GetComponent<RenderComponent>();
                positionComponent = entity.GetComponent<PositionComponent>();
                lifeComponent = entity.GetComponent<LifeComponent>();                           
                
                if (renderComponent != null && positionComponent != null)
                {
                    if (lifeComponent != null)
                    {
                        if (lifeComponent.IsAlive)
                        {
                            graphics.DrawImage(renderComponent.Image, (float) positionComponent.X,
                                (float) positionComponent.Y);
                        }
                    }
                    else
                    {                        
                        graphics.DrawImage(renderComponent.Image, (float)positionComponent.X, (float)positionComponent.Y, renderComponent.Image.Width, renderComponent.Image.Height);
                    }                                        
                }
            }*/
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