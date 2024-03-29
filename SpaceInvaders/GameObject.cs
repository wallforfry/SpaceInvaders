﻿using System.Drawing;

namespace SpaceInvaders
{
    /// <summary>
    ///     This is the generic abstact base class for any entity in the game
    /// </summary>
    public abstract class GameObject
    {
        /// <summary>
        ///     Update the state of a game objet
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="deltaT">time ellapsed in seconds since last call to Update</param>
        public abstract void Update(Game gameInstance, double deltaT);

        /// <summary>
        ///     Render the game object
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="graphics">graphic object where to perform rendering</param>
        public abstract void Draw(Game gameInstance, Graphics graphics);

        /// <summary>
        ///     Determines if object is alive. If false, the object will be removed automatically.
        /// </summary>
        /// <returns>Am I alive ?</returns>
        public abstract bool IsAlive();
    }
}