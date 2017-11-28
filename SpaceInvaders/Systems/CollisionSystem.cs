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
        
        private CompositionNodes<CollisionComposition> _collisionNodes;
        
        public void Initialize(Engine gameInstance)
        {
            _collisionNodes= gameInstance.WorldEntityManager.GetNodes<CollisionComposition>();               
        }


        public void Update(Engine gameInstance, Graphics graphics)
        {
            Initialize(gameInstance);
            foreach (var node in _collisionNodes.Nodes.ToArray())
            {
                if (node.Life.IsAlive)
                {
                    foreach (var node2 in _collisionNodes.Nodes.ToArray())
                    {
                        if (node.Owner.Id != node2.Owner.Id)
                        {
                            // Test qu'un missile IA ne détruit pas celui qui le lance
                            if (!(node.TypeComponent.TypeOfObject == TypeOfObject.MISSILE_IA &&
                                 node2.TypeComponent.TypeOfObject != TypeOfObject.AI) ||
                                !(node2.TypeComponent.TypeOfObject == TypeOfObject.MISSILE_IA &&
                                 node.TypeComponent.TypeOfObject != TypeOfObject.AI))                                                               
                            {                            
                            if (node2.Life.IsAlive)
                                {
                                    if (node2.Position.X >= node.Position.X &&
                                        node2.Position.X <= node.Position.X + node.Render.Image.Width)
                                    {
                                        if (node2.Position.Y >= node.Position.Y && node2.Position.Y <=
                                            node.Position.Y + node.Render.Image.Height)
                                        {
                                            TestCollision(node, node2);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }   
        }


        void TestCollision(CollisionComposition node, CollisionComposition node2)
        {
            
            for (int y = 0; y < node.Render.Image.Height; y++)           
            {
                for (int x = 0; x < node.Render.Image.Width; x++)
                {
                    try
                    {
                        Color color = node.Render.Image.GetPixel(x, y);

                        //Si pas pixel pas mort
                        if (color.A != 0)
                        {
                            //TODO : Récupérer la position relative de l'autre pixel
                            int pX = (int) (node.Position.X + x);
                            int pY = (int) (node.Position.Y + y);

                            if (node2.Position.X <= pX && pX < node2.Position.X + node2.Render.Image.Width)
                            {
                                if (node2.Position.Y <= pY && pY < node2.Position.Y + node2.Render.Image.Height)
                                {
                                    int pX2 = (int) (node.Position.X - node2.Position.X + x);
                                    int pY2 = (int) (node.Position.Y - node2.Position.Y + y);

                                    Color color2 = node2.Render.Image.GetPixel(pX2, pY2);
                                    if (color2.A != 0)
                                    {
                                        if(node.TypeComponent.TypeOfObject == TypeOfObject.STATIC)                                        
                                            DeletePixel(node.Render.Image, x, y, Color.Transparent);
                                        if(node2.TypeComponent.TypeOfObject == TypeOfObject.STATIC)
                                            DeletePixel(node2.Render.Image, pX2, pY2, Color.Transparent);
                                        
                                        RemoveLife(node, node2);
                                        return;
                                    }

                                }
                            }
                        }
                    }
                    catch (System.ArgumentException)
                    {
                        Console.WriteLine("Pixel en dehors");
                    }
                }
            }
        }

        void RemoveLife(CollisionComposition node, CollisionComposition node2)
        {
            LifeComponent lifeComponent = node.Life;
            LifeComponent lifeComponent2 = node2.Life;            
            lifeComponent.Lives -= 1;
            lifeComponent2.Lives -= 1;
        }
        
        void DeletePixel(Bitmap image, int X, int Y, Color color)
        {
            image.SetPixel(X, Y, color);
        }        
    }
}