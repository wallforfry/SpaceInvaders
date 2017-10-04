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

                physicsComponent = (PhysicsComponent) entity.GetComponent(typeof(PhysicsComponent));
                positionComponent = (PositionComponent) entity.GetComponent(typeof(PositionComponent));
                enemyBlockComponent = (EnemyBlockComponent) entity.GetComponent(typeof(EnemyBlockComponent));
                renderComponent = (RenderComponent) entity.GetComponent(typeof(RenderComponent));                             

                if (physicsComponent != null && positionComponent != null)
                {
                    if (physicsComponent.TypeOfObject == TypeOfObject.AI)
                    {

                        if (enemyBlockComponent != null && renderComponent != null)
                        {
                            if (positionComponent.X >= gameEngine.GameSize.Width - (renderComponent.Image.Width *
                                                       (enemyBlockComponent.NumberOfEnemy -
                                                        enemyBlockComponent.PositionInLine)))
                            {
                                physicsComponent.SpeedX = -1;
                                //positionComponent.Y += 10;
                            }
                            else if (positionComponent.X <= renderComponent.Image.Width)
                            {
                                physicsComponent.SpeedX = 1;
                                //positionComponent.Y += 10;
                            }
                        }

                        else
                        {
                            if (positionComponent.X >= limitX - 50)
                            {
                                physicsComponent.SpeedX = -1;
                                positionComponent.Y += 10;
                            }
                            else if (positionComponent.X <= 10)
                            {
                                physicsComponent.SpeedX = 1;
                                positionComponent.Y += 10;
                            }
                        }                       
                    }
                }
            }        
        }
    }
}