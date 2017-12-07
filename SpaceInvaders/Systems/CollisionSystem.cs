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
                    node.Life.IsShoot = false;
                    
                    foreach (var node2 in _collisionNodes.Nodes.ToArray())
                    {
                        if (node.Owner.Id != node2.Owner.Id)
                        {                           
                            if (node2.Life.IsAlive)
                            {
                                node2.Life.IsShoot = false;
                                
                                //Test de l'appartenance au rectangle sur l'axe X
                                if (node2.Position.X >= node.Position.X &&
                                    node2.Position.X <= node.Position.X + node.Render.Image.Width)
                                {                                    
                                    //Test de l'appartenance au rectangle sur l'axe Y
                                    if (node2.Position.Y >= node.Position.Y && node2.Position.Y <=
                                        node.Position.Y + node.Render.Image.Height)
                                    {
                                        //On ignore certains types de collisions
                                        if ((node.TypeComponent.TypeOfObject == TypeOfObject.MISSILE_IA &&
                                             node2.TypeComponent.TypeOfObject == TypeOfObject.AI) ||
                                            (node2.TypeComponent.TypeOfObject == TypeOfObject.MISSILE_IA &&
                                             node.TypeComponent.TypeOfObject == TypeOfObject.AI))
                                        {            
                                            //AI Friendly Fire
                                            break;
                                        }
                                        if (node.TypeComponent.TypeOfObject == TypeOfObject.AI &&
                                            node2.TypeComponent.TypeOfObject == TypeOfObject.AI)
                                        {
                                            //AI to AI Collision
                                            break;
                                        }
                                        else
                                        {                                       
                                            //On test la collision pixel à pixel
                                            TestCollision(node, node2, gameInstance);
                                            break;
                                        }
                                    }
                                }
                            }                                                            
                        }
                        if (node2.Life.IsShoot)
                            node2.Life.Lives--;
                    }
                    if(node.Life.IsShoot)
                        node.Life.Lives--;
                }
            }   
        }


        void TestCollision(CollisionComposition node, CollisionComposition node2, Engine gameInstance)
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
                                        //Collision AI vs Bunker => Fin de partie
                                        if ((node.TypeComponent.TypeOfObject == TypeOfObject.STATIC &&
                                             node2.TypeComponent.TypeOfObject == TypeOfObject.AI) ||
                                            (node2.TypeComponent.TypeOfObject == TypeOfObject.STATIC &&
                                             node.TypeComponent.TypeOfObject == TypeOfObject.AI))
                                        {
                                            gameInstance.CurrentGameState = GameState.GAME_OVER;
                                        }
                                        
                                        //Suppression pixel pour AI et bunker
                                        if(node.TypeComponent.TypeOfObject == TypeOfObject.STATIC || node.TypeComponent.TypeOfObject == TypeOfObject.AI)                                        
                                            DeletePixel(node.Render.Image, x, y, Color.Transparent);
                                        if(node2.TypeComponent.TypeOfObject == TypeOfObject.STATIC || node.TypeComponent.TypeOfObject == TypeOfObject.AI)
                                            DeletePixel(node2.Render.Image, pX2, pY2, Color.Transparent);
                                        
                                        //RemoveLife(node, node2);
                                        node.Life.IsShoot = true;
                                        node2.Life.Lives--;
                                        //Console.WriteLine(node.TypeComponent.TypeOfObject.ToString() + " VS "+ node2.TypeComponent.TypeOfObject.ToString());                                
                                        //return;
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

        //Retire une vie à chaque entité
        void RemoveLife(CollisionComposition node, CollisionComposition node2)
        {
            LifeComponent lifeComponent = node.Life;
            LifeComponent lifeComponent2 = node2.Life;            
            lifeComponent.Lives -= 1;
            lifeComponent2.Lives -= 1;
        }
        
        //Supprimer un pixel
        void DeletePixel(Bitmap image, int X, int Y, Color color)
        {
            image.SetPixel(X, Y, color);
        }        
    }
}