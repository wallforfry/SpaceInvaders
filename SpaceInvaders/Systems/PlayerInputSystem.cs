﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public class PlayerInputSystem : IEngineSystem
    {
        public void Update(Engine gameEngine)
        {
            if (KeyboardHelper.isPressed(Keys.Space))
            {
                // create new BalleQuiTombe
                gameEngine.newBall();               
                // release key space (no autofire)
                KeyboardHelper.ReleaseKey(Keys.Space);
            }
                     
            foreach (var entity in gameEngine.getEntity())
            {
                PhysicsComponent physicsComponent = null;
                FireComponent fireComponent = null;
                PositionComponent positionComponent = null;
                RenderComponent renderComponent = null;
                
                physicsComponent = (PhysicsComponent) entity.GetComponent(typeof(PhysicsComponent));
                fireComponent = (FireComponent) entity.GetComponent(typeof(FireComponent));
                positionComponent = (PositionComponent) entity.GetComponent(typeof(PositionComponent));
                renderComponent = (RenderComponent) entity.GetComponent(typeof(RenderComponent));
                         
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
                        /*if (fireComponent.Entity == null)
                        {
                            fireComponent.Entity = gameEngine.newMissile(positionComponent.X + renderComponent.Image.Width/2, positionComponent.Y);
                        }*/
                        
                        LifeComponent missileLifeComponent = ((LifeComponent)
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

            }                              
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}