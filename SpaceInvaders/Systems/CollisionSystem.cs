using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms.VisualStyles;
using SpaceInvaders.Nodes;

namespace SpaceInvaders
{
    public class CollisionSystem : IRenderSystem
    {
        public void Update()
        {
            
        }
        
        private CompositionNodes<MovableComposition> _renderNodes;
        
        public void Initialize(Engine gameInstance)
        {
            _renderNodes= gameInstance.WorldEntityManager.GetNodes<MovableComposition>();               
        }


        public void Update(Engine gameInstance, Graphics graphics)
        {
            Initialize(gameInstance);
            foreach (var node in _renderNodes.Nodes.ToArray())
            {
                if (node.Life.IsAlive)
                {
                    foreach (var node2 in _renderNodes.Nodes.ToArray())
                    {
                        if (node.Owner.Id != node2.Owner.Id)
                        {
                            if (node2.Life.IsAlive)
                            {
                                if (node2.Position.X >= node.Position.X &&
                                    node2.Position.X <= node.Position.X + node.Render.Image.Width)
                                {
                                    if (node2.Position.Y >= node.Position.Y && node2.Position.Y <=
                                        node.Position.Y + node.Render.Image.Height)
                                    {
                                        Console.WriteLine("Collision");
                                        //TestCollision(node, node2);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            /*
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
                                                 {//*                                                   
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
                }*/            
        }


        void TestCollision(MovableComposition node, MovableComposition node2)
        {
            
            for (int y = 0; y < node.Render.Image.Height; y++)           
            {
                for (int x = 0; x < node.Render.Image.Width; x++)
                {
                    Color color = node.Render.Image.GetPixel(x, y);
                    
                    //Si pas pixel pas mort
                    if(color.A != 0)
                    {
                        //TODO : Récupérer la position relative de l'autre pixel
                        int pX = (int) (node.Position.X + x);
                        int pY = (int) (node.Position.Y + y);

                        if (node2.Position.X <= pX && pX < node2.Position.X + node2.Render.Image.Width)
                        {
                            if (node2.Position.Y <= pY && pY < node2.Position.Y + node2.Render.Image.Height)
                            {
                                //int pX2 = (int) ((x + positionComponent2.X + (positionComponent.X - positionComponent2.X))- positionComponent2.X);
                                //int pY2 = (int) ((y + positionComponent2.Y + (positionComponent.Y - positionComponent2.Y))- positionComponent2.Y);
                                int pX2 = (int) (node.Position.X - node2.Position.X + x);
                                int pY2 = (int) (node.Position.Y - node2.Position.Y + y);

                                Color color2 = node2.Render.Image.GetPixel(pX2, pY2);
                                if (color2.A != 0)
                                {
                                    //Color test = Color.FromArgb(0, 255, 255, 255);
                                    Color test = Color.Transparent;
                                    DeletePixel(node.Render.Image, x, y, test);
                                    DeletePixel(node2.Render.Image, pX2, pY2, test);
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