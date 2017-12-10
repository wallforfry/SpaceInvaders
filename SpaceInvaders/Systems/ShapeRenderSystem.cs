using System.Drawing;
using SpaceInvaders.EngineFiles;

namespace SpaceInvaders.Systems
{
    //System de rendu des entités sans images mais avec une shape. Non converti au fonctionnement avec les noeuds mais 
    //conservé si on souhaite ajouter une entité de ce genre

    public class ShapeRenderSystem : IRenderSystem
    {
        public void Update()
        {
        }

        public void Initialize(Engine gameInstance)
        {
        }

        public void Update(Engine gameInstance, Graphics graphics)
        {
            /*foreach (var entity in gameInstance.getEntity())
            {
                ShapeComponent shapeComponent = null;
                PositionComponent positionComponent = null;                              
                
                shapeComponent = entity.GetComponent<ShapeComponent>();
                positionComponent = entity.GetComponent<PositionComponent>();           
                
                if (shapeComponent != null && positionComponent != null)
                {
                    float xmin = (float) (positionComponent.X - shapeComponent.Radius);
                    float ymin = (float) (positionComponent.Y - shapeComponent.Radius);
                    float width = (float) (2 * shapeComponent.Radius);
                    float height = (float) (2 * shapeComponent.Radius);
    
                    graphics.DrawEllipse(shapeComponent.Pen, xmin, ymin, width, height);                    
                }
            }      */
        }
    }
}