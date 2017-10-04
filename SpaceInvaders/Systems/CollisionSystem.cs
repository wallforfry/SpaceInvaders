using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace SpaceInvaders
{
    public class CollisionSystem : IRenderSystem
    {
        public void Update()
        {
            
        }

        public void Update(Engine gameInstance, Graphics graphics)
        {
            foreach (var entity in gameInstance.getEntity())
            {
                RenderComponent renderComponent = null;
                PositionComponent positionComponent = null;
                LifeComponent lifeComponent = null;
                PhysicsComponent physicsComponent = null;     

                renderComponent = (RenderComponent) entity.GetComponent(typeof(RenderComponent));
                positionComponent = (PositionComponent) entity.GetComponent(typeof(PositionComponent));
                lifeComponent = (LifeComponent) entity.GetComponent(typeof(LifeComponent));
                physicsComponent = (PhysicsComponent) entity.GetComponent(typeof(PhysicsComponent));             

                if (renderComponent != null && positionComponent != null)
                {
                    if (lifeComponent != null)
                    {
                        if (lifeComponent.IsAlive)
                        {
                            foreach (var entity2 in gameInstance.getEntity())
                            {
                                RenderComponent renderComponent2 = null;
                                PositionComponent positionComponent2 = null;
                                LifeComponent lifeComponent2 = null;
                                PhysicsComponent physicsComponent2 = null;

                                if (!entity.Equals(entity2))
                                {
                                    renderComponent2 = (RenderComponent) entity2.GetComponent(typeof(RenderComponent));
                                    positionComponent2 = (PositionComponent) entity2.GetComponent(typeof(PositionComponent));
                                    lifeComponent2 = (LifeComponent) entity2.GetComponent(typeof(LifeComponent));
                                    physicsComponent2 = (PhysicsComponent) entity2.GetComponent(typeof(PhysicsComponent));
                                    
                                    if (renderComponent2 != null && positionComponent2 != null)
                                    {
                                        if (lifeComponent2 != null)
                                        {
                                            if (lifeComponent2.IsAlive)
                                            {
                                                //TODO: FAIRE ICI LE TEST ENTRE ENTITY & ENTITY2
                                                if (positionComponent2.X >= positionComponent.X &&
                                                    positionComponent2.X <=
                                                    positionComponent.X + renderComponent.Image.Width)
                                                {
                                                    if (positionComponent2.Y >= positionComponent.Y &&
                                                         positionComponent2.Y <=
                                                         positionComponent.Y + renderComponent.Image.Height)
                                                     {


                                                         if (physicsComponent.TypeOfObject !=
                                                             physicsComponent2.TypeOfObject)
                                                         {
                                                           
                                                             //TODO: ICI FAIRE LE TRAITEMENT POUR LA COLLISION PRÉCISE

                                                             for (double i = positionComponent.X;
                                                                 i < positionComponent.X + renderComponent.Image.Width;
                                                                 i++)
                                                             {                                                                 
                                                                 for (double i2 = positionComponent2.X; i2 < positionComponent2.X +
                                                                                 renderComponent2.Image.Width; i2++)
                                                                     {
                                                                         if (i == i2)
                                                                         {
                                                                           //Collision sur X 
                                                                                 
                                                                             for (double j = positionComponent.Y;
                                                                                 j < positionComponent.Y + renderComponent.Image.Height;
                                                                                 j++)
                                                                             {                                                                 
                                                                                 for (double j2 = positionComponent2.Y; j2 < positionComponent2.Y +
                                                                                                                        renderComponent2.Image.Height; j2++)
                                                                                 {
                                                                                     if (j == j2)
                                                                                     {
                                                                                         //Collision sur Y
                                                                                         
                                                                                         if (lifeComponent.IsAlive && lifeComponent2.IsAlive)
                                                                                         {
                                                                                             //TODO: Gérer la collision par couleur de bit
                                                                                             Color color =
                                                                                                 renderComponent.Image
                                                                                                     .GetPixel((int) (i - positionComponent.X), (int) (j - positionComponent.Y));
                                                                                             Color color2 =
                                                                                                 renderComponent2.Image
                                                                                                     .GetPixel((int) (i2 - positionComponent2.X), (int) (j2 - positionComponent2.Y));

                                                                                             if (color == color2)
                                                                                             {
                                                                                                 Color delete = Color.Red;
                                                                                                 
                                                                                                 lifeComponent.Lives -=
                                                                                                     1;
                                                                                                 lifeComponent2.Lives -=
                                                                                                     1;

                                                                                                 DeletePixel(renderComponent.Image, (int)(i - positionComponent.X), (int)( j - positionComponent.Y), delete);
                                                                                                 DeletePixel(renderComponent2.Image, (int)(i2 - positionComponent2.X), (int)( j2 - positionComponent2.Y), delete);
                                                                                             
                                                                                                 debugDisplayLifes(entity, entity2);
                                                                                             }
                                                                                         }
                                                                                     }
                                                                                 }
                                                                             }
                                                                         }
                                                                     }
                                                             }
                                                         }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }        
        }

        void DeleteWhenCollision(Entity e1, Entity e2)
        {
            RenderComponent renderComponent = (RenderComponent) e1.GetComponent(typeof(RenderComponent));
            RenderComponent renderComponent2 = (RenderComponent) e2.GetComponent(typeof(RenderComponent));
            PositionComponent positionComponent = (PositionComponent) e1.GetComponent(typeof(PositionComponent));
            PositionComponent positionComponent2 = (PositionComponent) e2.GetComponent(typeof(PositionComponent));
            
            
            
            

        }
        
        void DeletePixel(Bitmap image, int X, int Y, Color color)
        {
            image.SetPixel(X, Y, color);
        }
        
        void debugDisplayLifes(Entity e1, Entity e2)
        {
            PhysicsComponent physicsComponent = (PhysicsComponent) e1.GetComponent(typeof(PhysicsComponent));
            PhysicsComponent physicsComponent2 = (PhysicsComponent) e2.GetComponent(typeof(PhysicsComponent));
            LifeComponent lifeComponent = (LifeComponent) e1.GetComponent(typeof(LifeComponent));
            LifeComponent lifeComponent2 = (LifeComponent) e2.GetComponent(typeof(LifeComponent));
            
         Debug.WriteLine(physicsComponent.TypeOfObject +" life = " +lifeComponent.Lives);
            lifeComponent.Lives -=1;
            Debug.WriteLine(physicsComponent.TypeOfObject +" life = " +lifeComponent.Lives);
            Debug.WriteLine(physicsComponent2.TypeOfObject +" life = " +lifeComponent2.Lives);
            lifeComponent2.Lives -=1;
            Debug.WriteLine(physicsComponent2.TypeOfObject +" life = " +lifeComponent2.Lives);   
        }
    }
}