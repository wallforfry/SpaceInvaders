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
                RenderComponent renderComponent = entity.GetComponent<RenderComponent>();
                PositionComponent positionComponent = entity.GetComponent<PositionComponent>();
                LifeComponent lifeComponent = entity.GetComponent<LifeComponent>();
                PhysicsComponent physicsComponent = entity.GetComponent<PhysicsComponent>();             

                //Si possède les bons composants
                if (renderComponent != null && positionComponent != null && lifeComponent != null)
                {
                    //Si e1 est en vie
                    if (lifeComponent.IsAlive)
                    {
                        //On cherche toutes les autres entitées
                        foreach (var entity2 in gameInstance.getEntity())
                        {
                            //Si e1 != e2
                            if (!entity.Equals(entity2))
                            {                                
                                RenderComponent renderComponent2 = entity2.GetComponent<RenderComponent>();
                                PositionComponent positionComponent2 = entity2.GetComponent<PositionComponent>();
                                LifeComponent lifeComponent2 = entity2.GetComponent<LifeComponent>();
                                PhysicsComponent physicsComponent2 = entity2.GetComponent<PhysicsComponent>();
                                 
                                //Si e2 possède les bons composants
                                if (renderComponent2 != null && positionComponent2 != null && lifeComponent2 != null)
                                {                                  
                                    //Si e2 est en vie
                                    if (lifeComponent2.IsAlive)
                                    {                                      
                                        //On test la collision des rectangles
                                        if (positionComponent2.X >= positionComponent.X && positionComponent2.X <= positionComponent.X + renderComponent.Image.Width)
                                        {
                                            if (positionComponent2.Y >= positionComponent.Y && positionComponent2.Y <= positionComponent.Y + renderComponent.Image.Height)
                                             {
                                                 /*if (physicsComponent.TypeOfObject != physicsComponent2.TypeOfObject)
                                                 {//*/                                                    
                                                     //On alors une collision pixel à pixel
                                                     TestCollision(entity, entity2);                                                             
                                                 //}
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


        void TestCollision(Entity e1, Entity e2)
        {
            RenderComponent renderComponent = e1.GetComponent<RenderComponent>();
            RenderComponent renderComponent2 = e2.GetComponent<RenderComponent>();           
            PositionComponent positionComponent = e1.GetComponent<PositionComponent>();
            PositionComponent positionComponent2 = e2.GetComponent<PositionComponent>();

            for (int y = 0; y < renderComponent.Image.Height; y++)           
            {
                for (int x = 0; x < renderComponent.Image.Width; x++)
                {
                    Color color = renderComponent.Image.GetPixel(x, y);
                    
                    //Si pas pixel pas mort
                    if(color.A != 0)
                    {
                        //TODO : Récupérer la position relative de l'autre pixel
                        int pX = (int) (positionComponent.X + x);
                        int pY = (int) (positionComponent.Y + y);

                        if (positionComponent2.X <= pX && pX < positionComponent2.X + renderComponent2.Image.Width)
                        {
                            if (positionComponent2.Y <= pY && pY < positionComponent2.Y + renderComponent2.Image.Height)
                            {
                                //int pX2 = (int) ((x + positionComponent2.X + (positionComponent.X - positionComponent2.X))- positionComponent2.X);
                                //int pY2 = (int) ((y + positionComponent2.Y + (positionComponent.Y - positionComponent2.Y))- positionComponent2.Y);
                                int pX2 = (int) (positionComponent.X - positionComponent2.X + x);
                                int pY2 = (int) (positionComponent.Y - positionComponent2.Y + y);

                                Color color2 = renderComponent2.Image.GetPixel(pX2, pY2);
                                if (color2.A != 0)
                                {
                                    //Color test = Color.FromArgb(0, 255, 255, 255);
                                    Color test = Color.Transparent;
                                    DeletePixel(renderComponent.Image, x, y, test);
                                    DeletePixel(renderComponent2.Image, pX2, pY2, test);
                                    //RemoveLife(e1, e2);
                                }

                            }
                        } 
                    }
                }
            }
        }

        void RemoveLife(Entity e1, Entity e2)
        {
            LifeComponent lifeComponent = e1.GetComponent<LifeComponent>();
            LifeComponent lifeComponent2 = e2.GetComponent<LifeComponent>();            
            lifeComponent.Lives -= 1;
            lifeComponent2.Lives -= 1;
        }
        void DeletePixel(Bitmap image, int X, int Y, Color color)
        {
            image.SetPixel(X, Y, color);
        }
        
        void debugDisplayLifes(Entity e1, Entity e2)
        {
            PhysicsComponent physicsComponent = e1.GetComponent<PhysicsComponent>();
            PhysicsComponent physicsComponent2 = e2.GetComponent<PhysicsComponent>();
            LifeComponent lifeComponent = e1.GetComponent<LifeComponent>();
            LifeComponent lifeComponent2 = e2.GetComponent<LifeComponent>();
            
         Debug.WriteLine(physicsComponent.TypeOfObject +" life = " +lifeComponent.Lives);
            lifeComponent.Lives -=1;
            Debug.WriteLine(physicsComponent.TypeOfObject +" life = " +lifeComponent.Lives);
            Debug.WriteLine(physicsComponent2.TypeOfObject +" life = " +lifeComponent2.Lives);
            lifeComponent2.Lives -=1;
            Debug.WriteLine(physicsComponent2.TypeOfObject +" life = " +lifeComponent2.Lives);   
        }
    }
}