using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace SpaceInvaders
{
    public class ImageRenderSystem : IRenderSystem
    {
        public void Update(Engine gameInstance, Graphics graphics)
        {
            foreach (var entity in gameInstance.getEntity())
            {
                RenderComponent renderComponent = null;
                PositionComponent positionComponent = null;
                LifeComponent lifeComponent = null;     
                
                renderComponent = (RenderComponent) entity.GetComponent(typeof(RenderComponent));
                positionComponent = (PositionComponent) entity.GetComponent(typeof(PositionComponent));
                lifeComponent = (LifeComponent) entity.GetComponent(typeof(LifeComponent));                           
                
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
            }        
        }

        public void Update()
        {
            
        }
    }
}