using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SpaceInvaders.Components;
using SpaceInvaders.Nodes;
using SpaceInvaders.Properties;
using SpaceInvaders.Systems;

namespace SpaceInvaders.EngineFiles
{
    public class Engine
    {
        public static HashSet<Keys> KeyPressed = new HashSet<Keys>();
        private readonly List<ISystem> _systemsList = new List<ISystem>();

        //Constructeur
        public Engine(Size gameSize)
        {
            GameSize = gameSize;
            WorldEntityManager = new EntityManager();


            //Création des systèmes

            var renderSystem = new ImageRenderSystem();
            renderSystem.Initialize(this);
            _systemsList.Add(renderSystem);

            var movableSystem = new MovablePhysicsSystem();
            movableSystem.Initialize(this);
            _systemsList.Add(movableSystem);

            var playerSystem = new PlayerInputSystem();
            playerSystem.Initialize(this);
            _systemsList.Add(playerSystem);

            var aiSystem = new AiInputSystem();
            aiSystem.Initialize(this);
            _systemsList.Add(aiSystem);

            var collisionSystem = new CollisionSystem();
            collisionSystem.Initialize(this);
            _systemsList.Add(collisionSystem);

            var gameSystem = new GameEngineSystem();
            _systemsList.Add(gameSystem);

            var enemyGenerationSystem = new EnemyGenerationSystem();
            enemyGenerationSystem.Initialize(this);
            //systemsList.Add(enemyGenerationSystem);

            //Peuplement du jeu
            CreateGame();
        }

        public Size GameSize { get; set; }

        public GameState CurrentGameState { get; set; }

        public EntityManager WorldEntityManager { get; set; }

        /*public void newBall()
        {
            ShapeComponent c1 = new ShapeComponent();
            c1.Radius = 10;
            
            PositionComponent c2 = new PositionComponent();
            c2.Position = new Vecteur2D(GameSize.Width / 2, 0);
            
            PhysicsComponent c3 = new PhysicsComponent();
            //c3.SpeedY = 100;
            c3.Vector = new Vecteur2D(0, 1);
            c3.TypeOfObject = TypeOfObject.MOVABLE;
            
            LifeComponent c4 = new LifeComponent();
            c4.Lives = 1;
            
            Entity test = new Entity(c1, c2, c3);            
            entityList.Add(test);       
            
        }
        */

        //Création d'un player
        public void SpaceShip()
        {
            var entity = WorldEntityManager.CreateEntity();

            var renderComponent = entity.CreateComponent<RenderComponent>();
            renderComponent.Image = new Bitmap(Resources.ship3);

            var positionComponent = entity.CreateComponent<PositionComponent>();
            positionComponent.Position = new Vecteur2D(300.0, 600.0);

            var lifeComponent = entity.CreateComponent<LifeComponent>();
            lifeComponent.Lives = 10;

            var physicsComponent = entity.CreateComponent<PhysicsComponent>();
            physicsComponent.Vector = new Vecteur2D(1, 1);
            physicsComponent.Move = new Vecteur2D();

            var typeComponent = entity.CreateComponent<TypeComponent>();
            typeComponent.TypeOfObject = TypeOfObject.Controlable;

            var fireComponent = entity.CreateComponent<FireComponent>();            
        }

        //Création d'une ligne d'ennemis
        public void EnemyLine(double xShift, double yShift, double speedX)
        {
            const int numberOfEnemy = 5;

            var rdm = new Random();
            var ship = rdm.Next(7);
            Bitmap image;

            switch (ship)
            {
                default:
                    image = new Bitmap(Resources.ship1);
                    break;
                case 1:
                    image = new Bitmap(Resources.ship2);
                    break;
                case 2:
                    image = new Bitmap(Resources.ship3);
                    break;
                case 3:
                    image = new Bitmap(Resources.ship4);
                    break;
                case 4:
                    image = new Bitmap(Resources.ship5);
                    break;
                case 5:
                    image = new Bitmap(Resources.ship6);
                    break;
                case 6:
                    image = new Bitmap(Resources.ship7);
                    break;
                case 7:
                    image = new Bitmap(Resources.ship8);
                    break;
            }

            for (var i = 1; i <= numberOfEnemy; i++)
                Enemy(xShift + 400 / numberOfEnemy * i, yShift, speedX, new Bitmap(image));
        }

        //Création d'un ennemi
        public void Enemy(double xShift, double yShift, double speedX, Bitmap image)
        {
            var enemy = WorldEntityManager.CreateEntity();

            var renderComponent = enemy.CreateComponent<RenderComponent>();
            renderComponent.Image = image;

            var positionComponent = enemy.CreateComponent<PositionComponent>();
            positionComponent.Position = new Vecteur2D(xShift, yShift);

            var lifeComponent = enemy.CreateComponent<LifeComponent>();
            lifeComponent.Lives = image.Height * image.Width / 10;
            lifeComponent.Lives = 2;

            var physicsComponent = enemy.CreateComponent<PhysicsComponent>();
            physicsComponent.Vector = new Vecteur2D(speedX, 30);
            physicsComponent.Move = new Vecteur2D();

            var typeComponent = enemy.CreateComponent<TypeComponent>();
            typeComponent.TypeOfObject = TypeOfObject.Ai;

            var enemyBlockComponent = enemy.CreateComponent<EnemyBlockComponent>();
            enemyBlockComponent.FireProbability = 8;

            var fireComponent = enemy.CreateComponent<FireComponent>();
        }

        //Création d'un bunker
        public void Bunker(double x)
        {
            var bunker = WorldEntityManager.CreateEntity();

            var renderComponent = bunker.CreateComponent<RenderComponent>();
            renderComponent.Image = new Bitmap(Resources.bunker2);

            var positionComponent = bunker.CreateComponent<PositionComponent>();
            positionComponent.Position = new Vecteur2D(x, 550.0);

            var physicsComponent = bunker.CreateComponent<PhysicsComponent>();
            physicsComponent.Vector = new Vecteur2D();
            physicsComponent.Move = new Vecteur2D();

            var typeComponent = bunker.CreateComponent<TypeComponent>();
            typeComponent.TypeOfObject = TypeOfObject.Static;

            var lifeComponent = bunker.CreateComponent<LifeComponent>();
            lifeComponent.Lives = renderComponent.NumberOfPixel;
        }


        //Création d'un missile ennemi
        public Entity NewAiMissile(AiComposition node)
        {
            var x = node.Position.X + node.Render.Image.Width / 2;
            var y = node.Position.Y + node.Render.Image.Height;
            const double speedY = 2;

            var entity = WorldEntityManager.CreateEntity();
            var lifeComponent = entity.CreateComponent<LifeComponent>();
            lifeComponent.Lives = 1;

            var physicsComponent = entity.CreateComponent<PhysicsComponent>();
            physicsComponent.Vector = new Vecteur2D(0, speedY);
            physicsComponent.Move = new Vecteur2D(0, speedY);

            var typeComponent = entity.CreateComponent<TypeComponent>();
            typeComponent.TypeOfObject = TypeOfObject.MissileIa;

            var positionComponent = entity.CreateComponent<PositionComponent>();
            positionComponent.Position = new Vecteur2D(x, y);

            var renderComponent = entity.CreateComponent<RenderComponent>();
            renderComponent.Image = new Bitmap(Resources.shoot1);

            return entity;
        }

        //Création d'un missile du joueur
        public Entity NewPlayerMissile(PlayerComposition node)
        {
            var x = node.Position.X + node.Render.Image.Width / 2;
            var y = node.Position.Y;
            const double speedY = -3;

            var entity = WorldEntityManager.CreateEntity();
            var c1 = entity.CreateComponent<LifeComponent>();
            c1.Lives = 1;

            var physicsComponent = entity.CreateComponent<PhysicsComponent>();
            physicsComponent.Vector = new Vecteur2D(0, speedY);
            physicsComponent.Move = new Vecteur2D(0, speedY);

            var typeComponent = entity.CreateComponent<TypeComponent>();
            typeComponent.TypeOfObject = TypeOfObject.Missile;

            var positionComponent = entity.CreateComponent<PositionComponent>();
            positionComponent.Position = new Vecteur2D(x, y);

            var renderComponent = entity.CreateComponent<RenderComponent>();
            renderComponent.Image = new Bitmap(Resources.shoot1);

            return entity;
        }

        //Traitement des systems de rendu et ceux de moteurs influant sur l'affichage
        public void Draw(Graphics graphics)
        {
            if (CurrentGameState == GameState.Play)
                foreach (var system in _systemsList)
                {
                    var renderSystem = system as IRenderSystem;
                    renderSystem?.Update(this, graphics);
                }

            foreach (var system in _systemsList)
            {
                var engineSystem = system as IEngineSystem;
                engineSystem?.Update(this, graphics);
            }
        }

        //Traitement des systems de physique du jeu
        public void Update(double deltaT)
        {
            foreach (var system in _systemsList)
                if (CurrentGameState == GameState.Play)
                {
                    var physicsSystem = system as IPhysicsSystem;
                    physicsSystem?.Update(this, deltaT);
                }
        }


        //Création d'une partie selon ce modèle
        public void CreateGame()
        {
            WorldEntityManager.ClearGame();

            /////SpaceShip////
            SpaceShip();

            //////Enemy///////
            EnemyLine(0, 0, 0.4);
            EnemyLine(0, 40, 0.4);
            EnemyLine(0, 80, 0.4);
            //EnemyLine(0,120, 0.5);

            //////Bunker//////

            Bunker(GameSize.Width / 2 - 300);
            Bunker(GameSize.Width / 2 - 50);
            Bunker(GameSize.Width / 2 + 200);

            CurrentGameState = GameState.Play;
        }

        
    }
}