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
        public void Update(Engine gameEngine, Graphics graphics)
        {
            
            IsPaused(gameEngine, graphics);
            EntityGarbageCollector(gameEngine);
            DisplayPlayerLife(gameEngine, graphics);
            IsGameOver(gameEngine, graphics);
            RestartGame(gameEngine);
        }

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

        private void IsGameOver(Engine gameInstance, Graphics graphics)
        {
            if(gameInstance.WorldEntityManager.GetNodes<PlayerComposition>().Nodes.Count <= 0)
            {
                gameInstance.CurrentGameState = GameState.GAME_OVER;
                String message = "Game Over :(\n\"R\" to restart";
                graphics.DrawString(message, new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel),
                    new SolidBrush(Color.Black), new Point(gameInstance.GameSize.Width / 2 - message.Length, gameInstance.GameSize.Height / 2));
                gameInstance.WorldEntityManager.ClearGame();
            }
        }

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

        private void RestartGame(Engine gameInstance)
        {
            if (gameInstance.CurrentGameState == GameState.GAME_OVER)
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