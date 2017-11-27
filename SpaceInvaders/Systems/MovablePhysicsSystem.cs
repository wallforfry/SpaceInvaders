using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms.VisualStyles;
using SpaceInvaders.Nodes;

namespace SpaceInvaders
{
    public class MovablePhysicsSystem : IPhysicsSystem
    {
        public void Update()
        {

        }

        private CompositionNodes<MovableComposition> _movableNodes;

        public void Initialize(Engine gameInstance)
        {
            _movableNodes = gameInstance.WorldEntityManager.GetNodes<MovableComposition>();
        }

        public void Update(Engine gameEngine, double deltaT)
        {
           
/*
                
                if (renderComponent != null && positionComponent != null && physicsComponent != null && enemyBlockComponent != null)
                {                 
                    if (enemyBlockComponent.Position.X >= 0 &&
                        enemyBlockComponent.Position.X + getEnemyBoxWidth(gameEngine) <= gameEngine.GameSize.Width)
                        //enemyBlockComponent.Position.X + enemyBlockComponent.Size.X <= gameEngine.GameSize.Width)
                    {
                        positionComponent.X += physicsComponent.SpeedX;
                        //enemyBlockComponent.Position.X += physicsComponent.SpeedX;
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
                else if (enemyBlockComponent != null && renderComponent == null && positionComponent == null && physicsComponent == null)
                {                                  
                    if (enemyBlockComponent.Position.X >= 0 &&
                        enemyBlockComponent.Position.X + getEnemyBoxWidth(gameEngine) <= gameEngine.GameSize.Width)
                    {
                        enemyBlockComponent.Position.X += 1; //physicsComponent.SpeedX;
                    }
                    /*if (enemyBlockComponent.Position.Y >= 0 &&
                        enemyBlockComponent.Position.Y < gameEngine.GameSize.Height - renderComponent.Image.Height)
                    {                      
                        //positionComponent.Y += physicsComponent.SpeedY;
                        //enemyBlockComponent.Position.Y += physicsComponent.SpeedY;                        
                    }*
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

        double getEnemyBoxWidth(Engine gameEngine)
        {                
                
            RenderComponent renderComponent = null;
            PositionComponent positionComponent = null;
            PhysicsComponent physicsComponent = null;
            ShapeComponent shapeComponent = null;
            LifeComponent lifeComponent = null;
            EnemyBlockComponent enemyBlockComponent = null;

            double maxSize = 0;
            
            
            foreach (var entity in gameEngine.getEntity())
            {                               
                renderComponent = entity.GetComponent<RenderComponent>();
                positionComponent = entity.GetComponent<PositionComponent>();
                physicsComponent = entity.GetComponent<PhysicsComponent>();
                shapeComponent = entity.GetComponent<ShapeComponent>();
                lifeComponent = entity.GetComponent<LifeComponent>();
                enemyBlockComponent = entity.GetComponent<EnemyBlockComponent>();


                if (renderComponent != null && positionComponent != null && physicsComponent != null &&
                    enemyBlockComponent != null)
                {
                    if (enemyBlockComponent.Size.X > maxSize)
                    {
                        maxSize = enemyBlockComponent.Size.X;
                    }
                }

            }
            return maxSize;
        }*/
         
            Initialize(gameEngine);
            foreach (var node in _movableNodes.Nodes.ToArray())
            {
               /* if (node.Position.X > 1 &&
                    node.Position.X < gameEngine.GameSize.Width - node.Render.Image.Width -1)
                {*/
                    node.Position.X += node.Physic.Move.X;
                /*}
                if (node.Position.Y >= 0 &&
                    node.Position.Y < gameEngine.GameSize.Height - node.Render.Image.Height)
                {*/
                    node.Position.Y += node.Physic.Move.Y;
                //}
            }
        }
    }
}