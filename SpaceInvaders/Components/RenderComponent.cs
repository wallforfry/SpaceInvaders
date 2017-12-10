using System.Drawing;

namespace SpaceInvaders.Components
{
    public class RenderComponent : IComponent
    {
        public Bitmap Image { get; set; }

        public int NumberOfPixel
        {
            get
            {
                var count = 0;
                for (var i = 0; i < Image.Width; i++)
                for (var j = 0; j < Image.Height; j++)
                    if (Image.GetPixel(i, j) == Color.FromArgb(0, 0, 0))
                        count++;
                return count;
            }
        }

        public bool Equals(IComponent other)
        {
            var renderComponent = other as RenderComponent;
            return renderComponent != null && Image.GetHashCode() == renderComponent.Image.GetHashCode();
        }
    }
}