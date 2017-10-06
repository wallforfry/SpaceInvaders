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

        public int NumberOfPixel
        {
            get
            {
                int count = 0;
                for(int i = 0; i < Image.Width; i++)
                {
                    for (int j = 0; j < Image.Height; j++)
                    {
                        if (Image.GetPixel(i, j) == Color.FromArgb(0, 0, 0))
                        {
                            count++;
                        }
                    }
                }
                return count;
            }
        }
    }
}