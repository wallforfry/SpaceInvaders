using System.Windows.Forms;

namespace SpaceInvaders.EngineFiles
{
    public class KeyboardHelper
    {
        public static bool IsPressed(Keys key)
        {
            return Engine.KeyPressed.Contains(key);
        }

        public static void ReleaseKey(Keys key)
        {
            Engine.KeyPressed.Remove(key);
        }

        public static bool IsReleased(Keys key)
        {
            return !Engine.KeyPressed.Contains(key);
        }
    }
}