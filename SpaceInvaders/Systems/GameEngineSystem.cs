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
            if (KeyboardHelper.isPressed(Keys.P))
            {
                
                if (gameEngine.CurrentGameState == GameState.PLAY)
                {
                    gameEngine.CurrentGameState = GameState.PAUSE;
                }
                else
                {
                    gameEngine.CurrentGameState = GameState.PLAY;
                }
                
                KeyboardHelper.ReleaseKey(Keys.P);
            }
            
            EntityGarbageCollector(gameEngine);
            DisplayPlayerLife(gameEngine, graphics);
            IsGameOver(gameEngine);
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

        private void IsGameOver(Engine gameInstance)
        {
            if(gameInstance.WorldEntityManager.GetNodes<PlayerComposition>().Nodes.Count <= 0)
            {
                gameInstance.CurrentGameState = GameState.GAME_OVER;
            }
        }

        public void Update()
        {
            
        }
    }
}