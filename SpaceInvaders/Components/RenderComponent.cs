using System.Drawing;
using System.Dynamic;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public class RenderComponent : IComponent
    {
        public Bitmap Image { get; set; }
        
        public bool Equals(IComponent other)
        {
            return Image.GetHashCode() == ((RenderComponent) other).Image.GetHashCode();
        }
    }
}