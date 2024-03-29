﻿using System.Windows.Forms;
using SpaceInvaders.Components;
using SpaceInvaders.EngineFiles;
using SpaceInvaders.Nodes;

namespace SpaceInvaders.Systems
{
    //Gère les entrés clavier du joueur
    public class PlayerInputSystem : IPhysicsSystem
    {
        private CompositionNodes<PlayerComposition> _playerNodes;

        public void Initialize(Engine gameInstance)
        {
            _playerNodes = gameInstance.WorldEntityManager.GetNodes<PlayerComposition>();
        }

        public void Update(Engine gameEngine, double deltaT)
        {
            Initialize(gameEngine);

            foreach (var node in _playerNodes.Nodes.ToArray())
                if (node.TypeComponent.TypeOfObject == TypeOfObject.Controlable)
                {
                    if (KeyboardHelper.IsPressed(Keys.Right))
                    {
                        node.Physic.Move.X = node.Position.X < gameEngine.GameSize.Width - node.Render.Image.Width ? node.Physic.SpeedX : 0;
                    }
                    else if (KeyboardHelper.IsPressed(Keys.Left))
                    {
                        if (node.Position.X > 0)
                            node.Physic.Move.X = -node.Physic.SpeedX;
                        else
                            node.Physic.Move.X = 0;
                    }

                    //Les lignes suivantes permettent d'ajouter au joueur un déplacement vertical (pour le debug c'est plus simple :) )
                    /*
                    else if (KeyboardHelper.isPressed(Keys.Down))
                    {
                        if(node.Position.Y < GameEngine.GameSize.Height - node.Render.Image.Height)
                            node.Physic.Move.Y = node.Physic.SpeedY;
                        else
                            node.Physic.Move.Y = 0;                        
                    }
                    else if (KeyboardHelper.isPressed(Keys.Up))
                    {
                        if (node.Position.Y > 0)
                            node.Physic.Move.Y = -node.Physic.SpeedY;
                        else
                            node.Physic.Move.Y = 0;
                    }//*/
                    else
                    {
                        node.Physic.Move.X = 0;
                        node.Physic.Move.Y = 0;
                    }

                    if (!KeyboardHelper.IsPressed(Keys.Space)) continue;
                    if (node.Fire.Entity == null || !node.Fire.Entity.GetComponent<LifeComponent>().IsAlive)
                        node.Fire.Entity = gameEngine.NewPlayerMissile(node);
                }
        }

        public void Update()
        {
        }
    }
}