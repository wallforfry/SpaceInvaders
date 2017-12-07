using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SpaceInvaders.Nodes;

namespace SpaceInvaders
{
    public class GameEngineSystem : IEngineSystem
    {
        public void Initialize(Engine gameInstance)
        {
            
        }
        
        //System qui gère le coeur du jeu (État, suppression des entités inutilisé/mortes/ affichage des vies du joueurs
        public void Update(Engine gameEngine, Graphics graphics)
        {
            
            IsPaused(gameEngine, graphics);
            EntityGarbageCollector(gameEngine);
            DisplayPlayerLife(gameEngine, graphics);
            IsGameOver(gameEngine, graphics);
            IsGameWin(gameEngine, graphics);
            RestartGame(gameEngine);
        }

        //Supprime les entités mortes
        private void EntityGarbageCollector(Engine gameEngine)
        {
            foreach (var entity in gameEngine.WorldEntityManager.GetEntities().ToArray())
            {
                LifeComponent lifeComponent = entity.Value.GetComponent<LifeComponent>();
                if (lifeComponent != null)
                {
                    if (!lifeComponent.IsAlive)
                    {
                        gameEngine.WorldEntityManager.DestroyEntity(entity.Key);
                    }
                    
                }
            }
        }
        
        //Affiche les vies du joueur
        private void DisplayPlayerLife(Engine gameInstance, Graphics graphics)
        {
            foreach (var playerComposition in gameInstance.WorldEntityManager.GetNodes<PlayerComposition>().Nodes
                .ToArray())
            {
                graphics.DrawString("Vies : "+playerComposition.Life.Lives.ToString(),
                    new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel), new SolidBrush(Color.Black),
                    new Point(10, 10));
            }
        }

        //Test si la partie est perdue
        private void IsGameOver(Engine gameInstance, Graphics graphics)
        {           
            if((gameInstance.WorldEntityManager.GetNodes<PlayerComposition>().Nodes.Count <= 0 && gameInstance.WorldEntityManager.GetNodes<AIComposition>().Nodes.Count > 0) || gameInstance.CurrentGameState == GameState.GAME_OVER)
            {
                gameInstance.CurrentGameState = GameState.GAME_OVER;
                String message = "Game Over :(\n\n\"R\" to restart";
                graphics.DrawString(message, new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel),
                    new SolidBrush(Color.Black), new Point(gameInstance.GameSize.Width / 2 - message.Length, gameInstance.GameSize.Height / 2));
            }
        }

        //Test si la partie est gagnée
        private void IsGameWin(Engine gameInstance, Graphics graphics)
        {
            if (gameInstance.WorldEntityManager.GetNodes<AIComposition>().Nodes.Count <= 0)
            {
                gameInstance.CurrentGameState = GameState.WIN;
                String message = "Win ! :)\n\n\"R\" to restart";
                graphics.DrawString(message, new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel),
                    new SolidBrush(Color.Black), new Point(gameInstance.GameSize.Width / 2 - message.Length, gameInstance.GameSize.Height / 2));  
            }
        }

        //Test si le jeu est en pause
        private void IsPaused(Engine gameInstance, Graphics graphics)
        {
            if (KeyboardHelper.isPressed(Keys.P))
            {
                
                if (gameInstance.CurrentGameState == GameState.PLAY)
                {
                    gameInstance.CurrentGameState = GameState.PAUSE;
                    
                }
                else if(gameInstance.CurrentGameState == GameState.PAUSE)
                {
                    gameInstance.CurrentGameState = GameState.PLAY;
                }
                
                KeyboardHelper.ReleaseKey(Keys.P);
            }

            if (gameInstance.CurrentGameState == GameState.PAUSE)
            {                
                String message = "Pause";
                graphics.DrawString(message,new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel), 
                    new SolidBrush(Color.Black), new Point(gameInstance.GameSize.Width/2 - message.Length, gameInstance.GameSize.Height/2)); 
            }
        }

        //Relance une nouvelle partie
        private void RestartGame(Engine gameInstance)
        {
            if (gameInstance.CurrentGameState == GameState.GAME_OVER || gameInstance.CurrentGameState == GameState.WIN)
            {
                if (KeyboardHelper.isPressed(Keys.R))
                {                  
                        gameInstance.CreateGame();
                }
            }
        }

        public void Update()
        {
            
        }
    }
}