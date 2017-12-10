using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SpaceInvaders.EngineFiles;

namespace SpaceInvaders
{
    public partial class GameForm : Form
    {
        #region constructor

        /// <summary>
        ///     Create form, create game
        /// </summary>
        public GameForm()
        {
            InitializeComponent();
            game = Game.CreateGame(ClientSize);
            watch.Start();
            WorldClock.Start();
        }

        #endregion

        private void GameForm_Load(object sender, EventArgs e)
        {
        }

        #region fields

        /// <summary>
        ///     Instance of the game
        /// </summary>
        private readonly Game game;

        public static Graphics graphics;

        #region time management

        /// <summary>
        ///     Game watch
        /// </summary>
        private readonly Stopwatch watch = new Stopwatch();

        /// <summary>
        ///     Last update time
        /// </summary>
        private long lastTime;

        #endregion

        #endregion

        #region events

        /// <summary>
        ///     Paint event of the form, see msdn for help => paint game with double buffering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            var bg = BufferedGraphicsManager.Current.Allocate(e.Graphics, e.ClipRectangle);
            var g = bg.Graphics;
            g.Clear(Color.Red);

            graphics = g;

            game.Draw(g);

            bg.Render();
            bg.Dispose();
        }

        /// <summary>
        ///     Tick event => update game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorldClock_Tick(object sender, EventArgs e)
        {
            // lets do 5 ms update to avoid quantum effects
            var maxDelta = 5;

            // get time with millisecond precision
            var nt = watch.ElapsedMilliseconds;
            // compute ellapsed time since last call to update
            double deltaT = nt - lastTime;

            for (; deltaT >= maxDelta; deltaT -= maxDelta)
                game.Update(maxDelta / 1000.0);

            game.Update(deltaT / 1000.0);

            // remember the time of this update
            lastTime = nt;

            Invalidate();
        }

        /// <summary>
        ///     Key down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            Engine.KeyPressed.Add(e.KeyCode);
        }

        /// <summary>
        ///     Key up event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            Engine.KeyPressed.Remove(e.KeyCode);
        }

        #endregion
    }
}