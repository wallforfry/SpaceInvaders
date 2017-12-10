using System;
using System.Drawing;
using SpaceInvaders.EngineFiles;
using SpaceInvaders.Nodes;

namespace SpaceInvaders.Systems
{
    public class CollisionSystem : IRenderSystem
    {
        private CompositionNodes<CollisionComposition> _collisionNodes;

        public void Update()
        {
        }

        public void Initialize(Engine gameInstance)
        {
            _collisionNodes = gameInstance.WorldEntityManager.GetNodes<CollisionComposition>();
        }


        public void Update(Engine gameInstance, Graphics graphics)
        {
            Initialize(gameInstance);
            foreach (var node in _collisionNodes.Nodes.ToArray())
                if (node.Life.IsAlive)
                {
                    node.Life.IsShoot = false;

                    foreach (var node2 in _collisionNodes.Nodes.ToArray())
                    {
                        if (node.Owner.Id != node2.Owner.Id)
                            if (node2.Life.IsAlive)
                            {
                                node2.Life.IsShoot = false;

                                //Test de l'appartenance au rectangle sur l'axe X
                                if (node2.Position.X >= node.Position.X &&
                                    node2.Position.X <= node.Position.X + node.Render.Image.Width)
                                    if (node2.Position.Y >= node.Position.Y && node2.Position.Y <=
                                        node.Position.Y + node.Render.Image.Height)
                                    {
                                        //On ignore certains types de collisions
                                        if (node.TypeComponent.TypeOfObject == TypeOfObject.MissileIa &&
                                            node2.TypeComponent.TypeOfObject == TypeOfObject.Ai ||
                                            node2.TypeComponent.TypeOfObject == TypeOfObject.MissileIa &&
                                            node.TypeComponent.TypeOfObject == TypeOfObject.Ai)
                                            break;
                                        if (node.TypeComponent.TypeOfObject == TypeOfObject.Ai &&
                                            node2.TypeComponent.TypeOfObject == TypeOfObject.Ai)
                                        {
                                            //AI to AI Collision
                                            break;
                                        }
                                        //On test la collision pixel à pixel
                                        TestCollision(node, node2, gameInstance);
                                        break;
                                    }
                            }
                        if (node2.Life.IsShoot)
                            node2.Life.Lives--;
                    }
                    if (node.Life.IsShoot)
                        node.Life.Lives--;
                }
        }


        private static void TestCollision(CollisionComposition node, CollisionComposition node2, Engine gameInstance)
        {
            for (var y = 0; y < node.Render.Image.Height; y++)
            for (var x = 0; x < node.Render.Image.Width; x++)
                try
                {
                    var color = node.Render.Image.GetPixel(x, y);

                    //Si pas pixel pas mort
                    if (color.A == 0) continue;
                    
                    var pX = (int) (node.Position.X + x);
                    var pY = (int) (node.Position.Y + y);

                    if (!(node2.Position.X <= pX) || !(pX < node2.Position.X + node2.Render.Image.Width)) continue;

                    if (!(node2.Position.Y <= pY) || !(pY < node2.Position.Y + node2.Render.Image.Height)) continue;
                    
                    var pX2 = (int) (node.Position.X - node2.Position.X + x);
                    var pY2 = (int) (node.Position.Y - node2.Position.Y + y);

                    var color2 = node2.Render.Image.GetPixel(pX2, pY2);
                    if (color2.A == 0) continue;
                            
                    //Collision AI vs Bunker => Fin de partie
                    if (node.TypeComponent.TypeOfObject == TypeOfObject.Static &&
                        node2.TypeComponent.TypeOfObject == TypeOfObject.Ai ||
                        node2.TypeComponent.TypeOfObject == TypeOfObject.Static &&
                        node.TypeComponent.TypeOfObject == TypeOfObject.Ai)
                        gameInstance.CurrentGameState = GameState.GameOver;

                    //Suppression pixel pour AI et bunker
                    if (node.TypeComponent.TypeOfObject == TypeOfObject.Static ||
                        node.TypeComponent.TypeOfObject == TypeOfObject.Ai)
                        DeletePixel(node.Render.Image, x, y, Color.Transparent);
                    if (node2.TypeComponent.TypeOfObject == TypeOfObject.Static ||
                        node.TypeComponent.TypeOfObject == TypeOfObject.Ai)
                        DeletePixel(node2.Render.Image, pX2, pY2, Color.Transparent);

                    node.Life.IsShoot = true;
                    node2.Life.Lives--;
                    //Console.WriteLine(node.TypeComponent.TypeOfObject.ToString() + " VS "+ node2.TypeComponent.TypeOfObject.ToString());                                
                    //return;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Pixel en dehors");
                }
        }

        //Supprimer un pixel
        private static void DeletePixel(Bitmap image, int x, int y, Color color)
        {
            image.SetPixel(x, y, color);
        }
    }
}