using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SpaceInvaders.Nodes;

namespace SpaceInvaders
{
    public class Engine
    {
        private List<Entity> entityList = new List<Entity>();
        private List<ISystem> systemsList = new List<ISystem>();

        public Size GameSize { get; set; }      
        public static HashSet<Keys> keyPressed = new HashSet<Keys>();
        
        public GameState CurrentGameState { get; set; }
        
        public EntityManager WorldEntityManager { get; set; }

        //Constructeur
        public Engine(Size gameSize)
        {
            GameSize = gameSize;             
            WorldEntityManager = new EntityManager();
            
            
            //Création des systèmes
            
            ImageRenderSystem renderSystem = new ImageRenderSystem();
            renderSystem.Initialize(this);
            systemsList.Add(renderSystem);
            
            MovablePhysicsSystem movableSystem = new MovablePhysicsSystem();
            movableSystem.Initialize(this);
            systemsList.Add(movableSystem);
            
            PlayerInputSystem playerSystem = new PlayerInputSystem();
            playerSystem.Initialize(this);
            systemsList.Add(playerSystem);
            
            AIInputSystem aiSystem = new AIInputSystem();
            aiSystem.Initialize(this);
            systemsList.Add(aiSystem);
            
            CollisionSystem collisionSystem = new CollisionSystem();
            collisionSystem.Initialize(this);
            systemsList.Add(collisionSystem);
            
            GameEngineSystem gameSystem = new GameEngineSystem();
            systemsList.Add(gameSystem);
            
            EnemyGenerationSystem enemyGenerationSystem = new EnemyGenerationSystem();
            enemyGenerationSystem.Initialize(this);
            //systemsList.Add(enemyGenerationSystem);
            
            //Peuplement du jeu
            CreateGame();
        }

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
            Entity entity = WorldEntityManager.CreateEntity();             

            RenderComponent c1 = entity.CreateComponent<RenderComponent>();
            c1.Image = new Bitmap(SpaceInvaders.Properties.Resources.ship3);

            PositionComponent c2 = entity.CreateComponent<PositionComponent>();
            c2.Position = new Vecteur2D(300.0,600.0);

            LifeComponent c3 = entity.CreateComponent<LifeComponent>();
            c3.Lives = 10;

            PhysicsComponent c7 = entity.CreateComponent<PhysicsComponent>();
            c7.Vector = new Vecteur2D(1,1);
            c7.Move = new Vecteur2D();
            
            TypeComponent c5 = entity.CreateComponent<TypeComponent>();
            c5.TypeOfObject = TypeOfObject.CONTROLABLE;      

            FireComponent c9 = entity.CreateComponent<FireComponent>();  
        }

        //Création d'une ligne d'ennemis
        public void EnemyLine(double xShift, double yShift, double speedX)
        {
            int numberOfEnemy = 5;
            
            Random rdm = new Random();
            int ship = rdm.Next(7);
            Bitmap image;
            
            switch (ship)
            {
                default:               
                    image = new Bitmap(SpaceInvaders.Properties.Resources.ship1);
                    break;                    
                case 1:                    
                    image = new Bitmap(SpaceInvaders.Properties.Resources.ship2);
                    break;
                case 2:                    
                    image = new Bitmap(SpaceInvaders.Properties.Resources.ship3);
                    break;
                case 3:
                    image = new Bitmap(SpaceInvaders.Properties.Resources.ship4);
                    break;
                case 4:
                    image = new Bitmap(SpaceInvaders.Properties.Resources.ship5);
                    break;
                case 5:
                    image = new Bitmap(SpaceInvaders.Properties.Resources.ship6);
                    break;
                case 6:
                    image = new Bitmap(SpaceInvaders.Properties.Resources.ship7);
                    break;
                case 7:
                    image = new Bitmap(SpaceInvaders.Properties.Resources.ship8);
                    break;
            }
            
            for (int i = 1; i <= numberOfEnemy; i++)
            {                
                Enemy(xShift + (400/numberOfEnemy) * i, yShift, speedX, new Bitmap(image));
            }
        }
        
        //Création d'un ennemi
        public void Enemy(double xShift, double yShift, double speedX, Bitmap image)
        {
            Entity enemy = WorldEntityManager.CreateEntity();                                                
            
            RenderComponent c4 = enemy.CreateComponent<RenderComponent>();
            c4.Image = image;

            PositionComponent c5 = enemy.CreateComponent<PositionComponent>();
            c5.Position = new Vecteur2D(xShift,yShift);

            LifeComponent c6 = enemy.CreateComponent<LifeComponent>();
            c6.Lives = image.Height * image.Width / 10;
            c6.Lives = 2;
            
            PhysicsComponent c8 = enemy.CreateComponent<PhysicsComponent>();
            c8.Vector = new Vecteur2D(speedX, 30);
            c8.Move = new Vecteur2D();
            
            TypeComponent c7 = enemy.CreateComponent<TypeComponent>();
            c7.TypeOfObject = TypeOfObject.AI;      
                      
            EnemyBlockComponent c9 = enemy.CreateComponent<EnemyBlockComponent>();
            c9.FireProbability = 8;

            FireComponent c10 = enemy.CreateComponent<FireComponent>();
        }
        
        //Création d'un bunker
        public void Bunker(double x)
        {
            Entity bunker = WorldEntityManager.CreateEntity();

            RenderComponent c1 = bunker.CreateComponent<RenderComponent>();
            c1.Image = new Bitmap(SpaceInvaders.Properties.Resources.bunker2);

            PositionComponent c2 = bunker.CreateComponent<PositionComponent>();
            c2.Position = new Vecteur2D(x, 550.0);

            PhysicsComponent c3 = bunker.CreateComponent<PhysicsComponent>();
            c3.Vector = new Vecteur2D(0,0);
            c3.Move = new Vecteur2D();

            TypeComponent c5 = bunker.CreateComponent<TypeComponent>();
            c5.TypeOfObject = TypeOfObject.STATIC;            

            LifeComponent c4 = bunker.CreateComponent<LifeComponent>();
            c4.Lives = c1.NumberOfPixel;        
        }   
        
        
        //Création d'un missile ennemi
        public Entity newAIMissile(AIComposition node)
         {
            double x = node.Position.X + node.Render.Image.Width / 2;
            double y = node.Position.Y + node.Render.Image.Height;
            double speedY = 2;
            
            Entity entity = WorldEntityManager.CreateEntity();
            LifeComponent c1 = entity.CreateComponent<LifeComponent>();
            c1.Lives = 1;            
            
            PhysicsComponent c2 = entity.CreateComponent<PhysicsComponent>();
            c2.Vector = new Vecteur2D(0, speedY);
            c2.Move = new Vecteur2D(0, speedY);
             
            TypeComponent c5 = entity.CreateComponent<TypeComponent>();      
            c5.TypeOfObject = TypeOfObject.MISSILE_IA;
             
            PositionComponent c3 = entity.CreateComponent<PositionComponent>();
            c3.Position = new Vecteur2D(x,y);

            RenderComponent c4 = entity.CreateComponent<RenderComponent>();
            c4.Image = new Bitmap(SpaceInvaders.Properties.Resources.shoot1);

            return entity;
        }
        
        //Création d'un missile du joueur
        public Entity newPlayerMissile(PlayerComposition node)
        {
            double x = node.Position.X + node.Render.Image.Width / 2;
            double y = node.Position.Y;
            double speedY = -3;
            
            Entity entity = WorldEntityManager.CreateEntity();
            LifeComponent c1 = entity.CreateComponent<LifeComponent>();
            c1.Lives = 1;            
            
            PhysicsComponent c2 = entity.CreateComponent<PhysicsComponent>();
            c2.Vector = new Vecteur2D(0, speedY);
            c2.Move = new Vecteur2D(0, speedY);
            
            TypeComponent c5 = entity.CreateComponent<TypeComponent>();
            c5.TypeOfObject = TypeOfObject.MISSILE;
            
            PositionComponent c3 = entity.CreateComponent<PositionComponent>();
            c3.Position = new Vecteur2D(x,y);

            RenderComponent c4 = entity.CreateComponent<RenderComponent>();
            c4.Image = new Bitmap(SpaceInvaders.Properties.Resources.shoot1);

            return entity;
        }

        //Traitement des systems de rendu et ceux de moteurs influant sur l'affichage
        public void Draw(Graphics graphics)
        {  
            if(CurrentGameState == GameState.PLAY)            
            {
                foreach (var system in systemsList)
                {
                    if (system as IRenderSystem != null)
                    {
                        ((IRenderSystem) system).Update(this, graphics);
                    }  
                }
            }

            foreach (var system in systemsList)
            {                                       
                if (system as IEngineSystem != null)
                {
                    ((IEngineSystem) system).Update(this, graphics);
                }         
            }

        }

        //Traitement des systems de physique du jeu
        public void Update(double deltaT)
        {
            
                foreach (var system in systemsList)
                {
                    if (CurrentGameState == GameState.PLAY)
                    {
                        if (system as IPhysicsSystem != null)
                        {
                            ((IPhysicsSystem) system).Update(this, deltaT);
                        }         
                    }
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
            EnemyLine(0,40, 0.4);
            EnemyLine(0,80, 0.4);
            //EnemyLine(0,120, 0.5);
            
            //////Bunker//////
            
            Bunker(GameSize.Width / 2 - 300);
            Bunker(GameSize.Width / 2 - 50);
            Bunker(GameSize.Width / 2 + 200);                                 
            
            CurrentGameState = GameState.PLAY;
        }

        public List<Entity> getEntity()
        {
            return entityList;
        }
    }
}