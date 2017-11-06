using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public class GameEngineSystem : IEngineSystem
    {
        public void Update(Engine gameEngine)
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
        }

        private void EntityGarbageCollector(Engine gameEngine)
        {
            foreach (var entity in gameEngine.getEntity().ToList())
            {
                LifeComponent lifeComponent = (LifeComponent) entity.GetComponent(typeof(LifeComponent));
                if (lifeComponent != null)
                {
                    if (!lifeComponent.IsAlive)
                    {
                        gameEngine.getEntity().Remove(entity);
                    }
                    
                }
            }
        }

        public void Update()
        {
            
        }
    }
}