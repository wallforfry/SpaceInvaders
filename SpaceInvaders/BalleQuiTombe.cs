using System.Drawing;

namespace SpaceInvaders
{
    //Classe du projet de base

    /// <summary>
    ///     Dummy class for demonstration
    /// </summary>
    internal class BalleQuiTombe : GameObject
    {
        #region Constructor

        /// <summary>
        ///     Simple constructor
        /// </summary>
        /// <param name="x">start position x</param>
        /// <param name="y">start position y</param>
        public BalleQuiTombe(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        #endregion

        #region Fields

        /// <summary>
        ///     Position
        /// </summary>
        private readonly double x;

        /// <summary>
        ///     Position
        /// </summary>
        private double y;

        /// <summary>
        ///     Radius
        /// </summary>
        private readonly double radius = 10;

        /// <summary>
        ///     Ball speed in pixel/second
        /// </summary>
        private readonly double ballSpeed = 100;

        /// <summary>
        ///     A shared black pen for drawing
        /// </summary>
        private static readonly Pen pen = new Pen(Color.Black);

        private bool alive = true;

        #endregion

        #region Methods

        public override void Update(Game gameInstance, double deltaT)
        {
            y += ballSpeed * deltaT;
            if (y > gameInstance.gameSize.Height)
                alive = false;
        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            var xmin = (float) (x - radius);
            var ymin = (float) (y - radius);
            var width = (float) (2 * radius);
            var height = (float) (2 * radius);
            graphics.DrawEllipse(pen, xmin, ymin, width, height);
        }

        public override bool IsAlive()
        {
            return alive;
        }

        #endregion
    }
}