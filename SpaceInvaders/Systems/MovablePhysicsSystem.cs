using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms.VisualStyles;

namespace SpaceInvaders
{
    public class MovablePhysicsSystem : IPhysicsSystem
    {
        public void Update()
        {
            
        }

        public void Update(Engine gameEngine, double deltaT)
        {
            foreach (var entity in gameEngine.getEntity())
            {
                RenderComponent renderComponent = null;
                PositionComponent positionComponent = null;
                PhysicsComponent physicsComponent = null;
                ShapeComponent shapeComponent = null;
                LifeComponent lifeComponent = null;
                EnemyBlockComponent enemyBlockComponent = null;
                
                
                renderComponent = (RenderComponent) entity.GetComponent(typeof(RenderComponent));
                positionComponent = (PositionComponent) entity.GetComponent(typeof(PositionComponent));
                physicsComponent = (PhysicsComponent) entity.GetComponent(typeof(PhysicsComponent));
                shapeComponent = (ShapeComponent) entity.GetComponent(typeof(ShapeComponent));
                lifeComponent = (LifeComponent) entity.GetComponent(typeof(LifeComponent));
                enemyBlockComponent = (EnemyBlockComponent) entity.GetComponent(typeof(EnemyBlockComponent));


                if (renderComponent != null && positionComponent != null && physicsComponent != null && enemyBlockComponent != null)
                {                 
                    if (enemyBlockComponent.Position.X >= 0 &&
                        enemyBlockComponent.Position.X + enemyBlockComponent.Size.X <= gameEngine.GameSize.Width)
                    {
                        positionComponent.X += physicsComponent.SpeedX;
                        enemyBlockComponent.Position.X += physicsComponent.SpeedX;            
                    }
                    if (enemyBlockComponent.Position.Y >= 0 &&
                        enemyBlockComponent.Position.Y < gameEngine.GameSize.Height - renderComponent.Image.Height)
                    {                      
                        //positionComponent.Y += physicsComponent.SpeedY;
                        //enemyBlockComponent.Position.Y += physicsComponent.SpeedY;                        
                    }
                    if (lifeComponent != null)
                    {
                        if (positionComponent.Y < 0)
                        {
                            lifeComponent.Lives = 0;
                        }
                    }
                    
                }
                else if (renderComponent != null && positionComponent != null && physicsComponent != null)
                {                 
                    if (positionComponent.X >= 0 &&
                        positionComponent.X < gameEngine.GameSize.Width - renderComponent.Image.Width)
                    {
                        positionComponent.X += physicsComponent.SpeedX;                                                                       
                    }
                    if (positionComponent.Y >= 0 &&
                        positionComponent.Y < gameEngine.GameSize.Height - renderComponent.Image.Height)
                    {                      
                        positionComponent.Y += physicsComponent.SpeedY;
                    }
                    if (lifeComponent != null)
                    {
                        if (positionComponent.Y < 0)
                        {
                            lifeComponent.Lives = 0;
                        }
                    }
                    
                }
                else if (shapeComponent != null && positionComponent != null && physicsComponent != null)
                {
                    if (positionComponent.X >= 0 &&
                        positionComponent.X < gameEngine.GameSize.Width - shapeComponent.Radius * 2)
                    {
                        positionComponent.X +=
                            physicsComponent.SpeedX;
                    }
                    if (positionComponent.Y >= 0 &&
                        positionComponent.Y < gameEngine.GameSize.Height - shapeComponent.Radius * 2)
                    {
                        positionComponent.Y +=
                            physicsComponent.SpeedY;
                    }
                }
            }
        }
    }
}