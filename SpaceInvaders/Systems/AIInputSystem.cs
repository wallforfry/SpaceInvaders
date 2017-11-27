using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public class AIInputSystem : IEngineSystem
    {
        public void Update()
        {
            
        }

        public void Update(Engine gameEngine)
        {
            int limitX = gameEngine.GameSize.Width;
            int limitY = 400;
            
            foreach (var entity in gameEngine.getEntity())
            {
                PhysicsComponent physicsComponent = null;
                PositionComponent positionComponent = null;
                EnemyBlockComponent enemyBlockComponent = null;
                RenderComponent renderComponent = null;

                physicsComponent = entity.GetComponent<PhysicsComponent>();
                positionComponent = entity.GetComponent<PositionComponent>();
                enemyBlockComponent = entity.GetComponent<EnemyBlockComponent>();
                renderComponent = entity.GetComponent<RenderComponent>();                             

                if (physicsComponent != null && positionComponent != null)
                {
                    if (physicsComponent.TypeOfObject == TypeOfObject.AI)
                    {

                        if (enemyBlockComponent != null && renderComponent != null)
                        {
                            double x = enemyBlockComponent.Position.X;
                            //Debug.WriteLine(x);
                            //double x = (positionComponent.X - (50 * (enemyBlockComponent.PositionInLine - 1)) - (renderComponent.Image.Width * (enemyBlockComponent.PositionInLine - 1)));
                            /*if (positionComponent.X >= gameEngine.GameSize.Width - (renderComponent.Image.Width *
                                                       (enemyBlockComponent.NumberOfEnemy -
                                                        enemyBlockComponent.PositionInLine)))
                            {*/  
                            if (x <= 0)
                            {
                                physicsComponent.SpeedX = -physicsComponent.SpeedX;
                                positionComponent.Y += physicsComponent.SpeedY;
                            }
                            //else if (positionComponent.X <= renderComponent.Image.Width)
                            else if (x + enemyBlockComponent.Size.X >= limitX)
                            {
                                physicsComponent.SpeedX = -physicsComponent.SpeedX;
                                positionComponent.Y += physicsComponent.SpeedY;
                            }
                        }

                        else
                        {
                            if (positionComponent.X >= limitX - 50)
                            {
                                physicsComponent.SpeedX = -1;
                                positionComponent.Y += physicsComponent.SpeedY;
                            }
                            else if (positionComponent.X <= 10)
                            {
                                physicsComponent.SpeedX = 1;
                                positionComponent.Y += physicsComponent.SpeedY;
                            }
                        }                       
                    }
                }
            }        
        }
    }
}