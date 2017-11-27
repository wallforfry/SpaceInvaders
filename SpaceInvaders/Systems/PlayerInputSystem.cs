using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using SpaceInvaders.Nodes;

namespace SpaceInvaders
{
    public class PlayerInputSystem : IEngineSystem
    {
        private CompositionNodes<PlayerComposition> _playerNodes;
        
        public void Initialize(Engine gameInstance)
        {
            _playerNodes = gameInstance.WorldEntityManager.GetNodes<PlayerComposition>();
        }
        
        public void Update(Engine gameEngine)
        {
            /*
            if (KeyboardHelper.isPressed(Keys.Space))
            {
                // create new BalleQuiTombe
                ///////gameEngine.newBall();               
                // release key space (no autofire)
                KeyboardHelper.ReleaseKey(Keys.Space);
            }
                     
            foreach (var entity in gameEngine.getEntity().ToList())
            {
                
                PhysicsComponent physicsComponent = entity.GetComponent<PhysicsComponent>();
                FireComponent fireComponent = entity.GetComponent<FireComponent>();
                PositionComponent positionComponent = entity.GetComponent<PositionComponent>();
                RenderComponent renderComponent = entity.GetComponent<RenderComponent>();
                         
                if (physicsComponent != null)
                {
                    if (physicsComponent.TypeOfObject == TypeOfObject.CONTROLABLE)
                    {
                        if (KeyboardHelper.isPressed(Keys.Right))
                        {
                            physicsComponent.SpeedX = 1;
                        }
                        else if (KeyboardHelper.isPressed(Keys.Left))
                        {
                            physicsComponent.SpeedX = -1;
                        }
                        else
                        {
                            physicsComponent.SpeedX = 0;
                        }
                        //KeyboardHelper.ReleaseKey(Keys.Left);
                        //KeyboardHelper.ReleaseKey(Keys.Right);
                    }
                }




                if (fireComponent != null && positionComponent != null && renderComponent != null)
                {
                    if (KeyboardHelper.isPressed(Keys.Enter))
                    {
                        
                        //TODO: Si on fait un newMissile ici, la collection est modifiée du coup le foreach plante. Il faut essayer d'inverser le test d'appui sur Entrer et le foreach ou un truc du genre
                        if (!fireComponent.Entity.GetComponent<LifeComponent>().IsAlive)
                        {
                            //////fireComponent.Entity =
                                //////gameEngine.newMissile(positionComponent.X + renderComponent.Image.Width / 2,
                                //////       positionComponent.Y, 1);
                            /*fireComponent.Entity =
                                gameEngine.newEnemy(positionComponent.X,
                                    positionComponent.Y);*
                        }
                        
                        /*LifeComponent missileLifeComponent = ((LifeComponent)
                                fireComponent.Entity.GetComponents()[typeof(LifeComponent)]);
                        
                        if (!missileLifeComponent.IsAlive)
                        {
                            PositionComponent missilePositionComponent =
                            ((PositionComponent) fireComponent.Entity.GetComponents()[typeof(PositionComponent)]
                            );

                            missileLifeComponent.Lives = 1;
                            //missilePositionComponent.Position = new Vecteur2D(positionComponent.Position);
                            missilePositionComponent.X = positionComponent.X + renderComponent.Image.Width / 2;
                            missilePositionComponent.Y = positionComponent.Y;
                        }

                        KeyboardHelper.ReleaseKey(Keys.Enter);
                    }
                }

            }*/
            foreach (var node in _playerNodes.Nodes.ToArray())
            {
                if (node.Physic.TypeOfObject == TypeOfObject.CONTROLABLE)
                {
                    if (KeyboardHelper.isPressed(Keys.Right))
                    {
                        node.Physic.SpeedX = 1;
                    }
                    else if (KeyboardHelper.isPressed(Keys.Left))
                    {
                        node.Physic.SpeedX = -1;
                    }
                    else
                    {
                        node.Physic.SpeedX = 0;
                    }
                    //KeyboardHelper.ReleaseKey(Keys.Left);
                    //KeyboardHelper.ReleaseKey(Keys.Right);
                }
            }


        }

        public void Update()
        {
            
        }
    }
}