using System.Windows.Forms;

namespace SpaceInvaders
{
    public class KeyboardHelper
    {
        public static bool isPressed(Keys key)
        {
            return Engine.keyPressed.Contains(key);
        }
        public static void ReleaseKey(Keys key)
        {
            Engine.keyPressed.Remove(key);
        }

        public static bool isReleased(Keys key)
        {
            return !Engine.keyPressed.Contains(key);
        }
    }
}