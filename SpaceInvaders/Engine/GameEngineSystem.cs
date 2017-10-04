using System.Diagnostics;
using System.Drawing;
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
        }

        public void Update()
        {
            
        }
    }
}