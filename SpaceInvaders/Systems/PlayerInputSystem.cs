using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
            Initialize(gameEngine);
            
            foreach (var node in _playerNodes.Nodes.ToArray())
            {
                if (node.Physic.TypeOfObject == TypeOfObject.CONTROLABLE)
                {
                    if (KeyboardHelper.isPressed(Keys.Right))
                    {
                        if(node.Position.X < gameEngine.GameSize.Width - node.Render.Image.Width)
                            node.Physic.Move.X = node.Physic.SpeedX;
                        else
                            node.Physic.Move.X = 0;                        
                    }
                    else if (KeyboardHelper.isPressed(Keys.Left))
                    {
                        if (node.Position.X > 0)
                            node.Physic.Move.X = -node.Physic.SpeedX;
                        else
                            node.Physic.Move.X = 0;
                    }
                    else
                    {
                        node.Physic.Move.X = 0;
                    }

                    if (KeyboardHelper.isPressed(Keys.Space))
                    {                       
                        if (node.Fire.Entity == null || !node.Fire.Entity.GetComponent<LifeComponent>().IsAlive)
                        {
                            Entity entity = gameEngine.WorldEntityManager.CreateEntity();
                            LifeComponent c1 = entity.CreateComponent<LifeComponent>();
                            c1.Lives = 1;

                            PhysicsComponent c2 = entity.CreateComponent<PhysicsComponent>();
                            c2.Vector = new Vecteur2D(0, -2);
                            c2.Move = new Vecteur2D(0, -2);
                            c2.TypeOfObject = TypeOfObject.MOVABLE;

                            PositionComponent c3 = entity.CreateComponent<PositionComponent>();
                            c3.Position = new Vecteur2D(node.Position.X + node.Render.Image.Width / 2, node.Position.Y);

                            RenderComponent c4 = entity.CreateComponent<RenderComponent>();
                            c4.Image = new Bitmap(SpaceInvaders.Properties.Resources.shoot1);

                            node.Fire.Entity = entity;
                        }

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