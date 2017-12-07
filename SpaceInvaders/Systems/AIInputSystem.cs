using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using SpaceInvaders.Nodes;

namespace SpaceInvaders
{
    public class AIInputSystem : IPhysicsSystem
    {
        public void Update()
        {
            
        }

        private CompositionNodes<AIComposition> _aiNodes;
        
        //Récupération des noeuds
        public void Initialize(Engine gameInstance)
        {
            _aiNodes= gameInstance.WorldEntityManager.GetNodes<AIComposition>();               
        }

        //Traitement
        public void Update(Engine gameEngine, double deltaT)
        {
            Initialize(gameEngine);
            
            int limitX = gameEngine.GameSize.Width;
            int limitY = 400;
            
            foreach (var node in _aiNodes.Nodes.ToArray())
            {
                //On test que le composant est du bon type au cas ou
                if (node.TypeComponent.TypeOfObject == TypeOfObject.AI)
                {
                    //On effectue un déplacement linéaire et on augmente la fréquence des tirs et la vitesse horizontale à chaque allez retour
                    if (node.Position.X > limitX - node.Render.Image.Width)
                    {
                        foreach (var other in _aiNodes.Nodes.ToArray())
                        {
                            other.Physic.SpeedX = -other.Physic.SpeedX;
                            other.Physic.Move.X = other.Physic.SpeedX;
                            other.Position.Y += other.Physic.SpeedY;
                            IncreaseFireRate(other);
                            IncreaseSpeed(other);
                        }                            
                        break;
                    }
                    
                    if (node.Position.X < 0)
                    {
                        foreach (var other in _aiNodes.Nodes.ToArray())
                        {
                            other.Physic.SpeedX = -other.Physic.SpeedX;
                            other.Physic.Move.X = other.Physic.SpeedX;
                            other.Position.Y += other.Physic.SpeedY;
                            IncreaseFireRate(other);
                            IncreaseSpeed(other);
                        }
                        break;
                    }
                    
                    node.Physic.Move.X = node.Physic.SpeedX;                              
                }

                // On tir avec une probabilité définie qui augmente à chaque changement de direction
                // Un seul tir à la fois par ennemi
                Random rdm = new Random();
                if (rdm.Next(1000) < node.Enemy.FireProbability)
                {
                    if (node.Fire.Entity == null || !node.Fire.Entity.GetComponent<LifeComponent>().IsAlive)
                    {
                        node.Fire.Entity = gameEngine.newAIMissile(node);
                    }
                }

            }        
        }

        private void IncreaseSpeed(AIComposition node)
        {
            node.Physic.SpeedX *= 1.05;
        }

        private void IncreaseFireRate(AIComposition node)
        {
            node.Enemy.FireProbability += 1;
        }
    }
}