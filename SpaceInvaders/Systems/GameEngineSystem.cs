using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SpaceInvaders.Components;
using SpaceInvaders.EngineFiles;
using SpaceInvaders.Nodes;

namespace SpaceInvaders.Systems
{
    public class GameEngineSystem : IEngineSystem
    {
        public void Initialize(Engine gameInstance)
        {
        }

        //System qui gère le coeur du jeu (État, suppression des entités inutilisé/mortes/ affichage des vies du joueurs
        public void Update(Engine gameEngine, Graphics graphics)
        {
            IsPaused(gameEngine, graphics);
            EntityGarbageCollector(gameEngine);
            DisplayPlayerLife(gameEngine, graphics);
            IsGameOver(gameEngine, graphics);
            IsGameWin(gameEngine, graphics);
            RestartGame(gameEngine);
        }

        public void Update()
        {
        }

        //Supprime les entités mortes
        private static void EntityGarbageCollector(Engine gameEngine)
        {
            foreach (var entity in gameEngine.WorldEntityManager.GetEntities().ToArray())
            {
                var lifeComponent = entity.Value.GetComponent<LifeComponent>();
                if (lifeComponent == null) continue;
                if (!lifeComponent.IsAlive)
                    gameEngine.WorldEntityManager.DestroyEntity(entity.Key);
            }
        }

        //Affiche les vies du joueur
        private static void DisplayPlayerLife(Engine gameInstance, Graphics graphics)
        {
            foreach (var playerComposition in gameInstance.WorldEntityManager.GetNodes<PlayerComposition>().Nodes
                .ToArray())
                graphics.DrawString("Vies : " + playerComposition.Life.Lives,
                    new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel), new SolidBrush(Color.Black),
                    new Point(10, 10));
        }

        //Test si la partie est perdue
        private static void IsGameOver(Engine gameInstance, Graphics graphics)
        {
            if ((gameInstance.WorldEntityManager.GetNodes<PlayerComposition>().Nodes.Count > 0 ||
                 gameInstance.WorldEntityManager.GetNodes<AiComposition>().Nodes.Count <= 0) &&
                gameInstance.CurrentGameState != GameState.GameOver) return;
            
            gameInstance.CurrentGameState = GameState.GameOver;
            const string message = "Game Over :(\n\n\"R\" to restart";
            graphics.DrawString(message, new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel),
                new SolidBrush(Color.Black),
                new Point(gameInstance.GameSize.Width / 2 - message.Length, gameInstance.GameSize.Height / 2));
        }

        //Test si la partie est gagnée
        private static void IsGameWin(Engine gameInstance, Graphics graphics)
        {
            if (gameInstance.WorldEntityManager.GetNodes<AiComposition>().Nodes.Count > 0) return;
            gameInstance.CurrentGameState = GameState.Win;
            const string message = "Win ! :)\n\n\"R\" to restart";
            graphics.DrawString(message, new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel),
                new SolidBrush(Color.Black),
                new Point(gameInstance.GameSize.Width / 2 - message.Length, gameInstance.GameSize.Height / 2));
        }

        //Test si le jeu est en pause
        private static void IsPaused(Engine gameInstance, Graphics graphics)
        {
            if (KeyboardHelper.IsPressed(Keys.P))
            {
                switch (gameInstance.CurrentGameState)
                {
                    case GameState.Play:
                        gameInstance.CurrentGameState = GameState.Pause;
                        break;
                    case GameState.Pause:
                        gameInstance.CurrentGameState = GameState.Play;
                        break;              
                }

                KeyboardHelper.ReleaseKey(Keys.P);
            }

            if (gameInstance.CurrentGameState != GameState.Pause) return;
            const string message = "Pause";
            graphics.DrawString(message, new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel),
                new SolidBrush(Color.Black),
                new Point(gameInstance.GameSize.Width / 2 - message.Length, gameInstance.GameSize.Height / 2));
        }

        //Relance une nouvelle partie
        private static void RestartGame(Engine gameInstance)
        {
            if (gameInstance.CurrentGameState != GameState.GameOver &&
                gameInstance.CurrentGameState != GameState.Win) return;
            if (KeyboardHelper.IsPressed(Keys.R))
                gameInstance.CreateGame();
        }
    }
}