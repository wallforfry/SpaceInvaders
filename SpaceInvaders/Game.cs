using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SpaceInvaders.EngineFiles;

namespace SpaceInvaders
{
    public class Game
    {
        public static Engine GameEngine;

        #region GameObjects management

        /// <summary>
        ///     Set of all game objects currently in the game
        /// </summary>
        public HashSet<GameObject> gameObjects = new HashSet<GameObject>();

        /// <summary>
        ///     Set of new game objects scheduled for addition to the game
        /// </summary>
        private readonly HashSet<GameObject> pendingNewGameObjects = new HashSet<GameObject>();

        /// <summary>
        ///     Schedule a new object for addition in the game.
        ///     The new object will be added at the beginning of the next update loop
        /// </summary>
        /// <param name="gameObject">object to add</param>
        public void AddNewGameObject(GameObject gameObject)
        {
            pendingNewGameObjects.Add(gameObject);
        }

        #endregion

        #region game technical elements

        /// <summary>
        ///     Size of the game area
        /// </summary>
        public Size gameSize;

        /// <summary>
        ///     State of the keyboard
        /// </summary>
        public HashSet<Keys> keyPressed = new HashSet<Keys>();

        #endregion

        #region static fields (helpers)

        /// <summary>
        ///     Singleton for easy access
        /// </summary>
        public static Game game { get; private set; }

        /// <summary>
        ///     A shared black brush
        /// </summary>
        private static Brush blackBrush = new SolidBrush(Color.Black);

        /// <summary>
        ///     A shared simple font
        /// </summary>
        private static Font defaultFont = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel);

        #endregion

        #region constructors

        /// <summary>
        ///     Singleton constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        /// <returns></returns>
        public static Game CreateGame(Size gameSize)
        {
            if (game == null)
                game = new Game(gameSize);
            GameEngine = new Engine(gameSize);

            return game;
        }

        /// <summary>
        ///     Private constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        private Game(Size gameSize)
        {
            this.gameSize = gameSize;
        }

        #endregion

        #region methods

        /// <summary>
        ///     Force a given key to be ignored in following updates until the user
        ///     explicitily retype it or the system autofires it again.
        /// </summary>
        /// <param name="key">key to ignore</param>
        public void ReleaseKey(Keys key)
        {
            keyPressed.Remove(key);
        }


        /// <summary>
        ///     Draw the whole game
        /// </summary>
        /// <param name="g">Graphics to draw in</param>
        public void Draw(Graphics g)
        {
            /*foreach (GameObject gameObject in gameObjects)
                gameObject.Draw(this, g);*/
            GameEngine.Draw(g);
        }

        /// <summary>
        ///     Update game
        /// </summary>
        public void Update(double deltaT)
        {
            GameEngine.Update(deltaT);
        }

        #endregion
    }
}