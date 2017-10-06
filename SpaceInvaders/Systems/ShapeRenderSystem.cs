using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Security.Policy;

namespace SpaceInvaders
{
    public class ShapeRenderSystem : IRenderSystem
    {
        public void Update()
        {
            
        }
 
        public void Update(Engine gameInstance, Graphics graphics)
        {
            foreach (var entity in gameInstance.getEntity())
            {
                ShapeComponent shapeComponent = null;
                PositionComponent positionComponent = null;                              
                
                shapeComponent = (ShapeComponent) entity.GetComponent(typeof(ShapeComponent));
                positionComponent = (PositionComponent) entity.GetComponent(typeof(PositionComponent));           
                
                if (shapeComponent != null && positionComponent != null)
                {
                    float xmin = (float) (positionComponent.X - shapeComponent.Radius);
                    float ymin = (float) (positionComponent.Y - shapeComponent.Radius);
                    float width = (float) (2 * shapeComponent.Radius);
                    float height = (float) (2 * shapeComponent.Radius);
    
                    graphics.DrawEllipse(shapeComponent.Pen, xmin, ymin, width, height);                    
                }
            }         
        }
    }
}