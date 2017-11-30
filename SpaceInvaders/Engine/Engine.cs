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

        public Engine(Size gameSize)
        {
            GameSize = gameSize;             
            WorldEntityManager = new EntityManager();            
            
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
            systemsList.Add(enemyGenerationSystem);
            
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

        public void SpaceShip()
        {
            Entity entity = WorldEntityManager.CreateEntity();             

            RenderComponent c1 = entity.CreateComponent<RenderComponent>();
            c1.Image = new Bitmap(SpaceInvaders.Properties.Resources.ship3);

            PositionComponent c2 = entity.CreateComponent<PositionComponent>();
            c2.Position = new Vecteur2D(300.0,550.0);

            LifeComponent c3 = entity.CreateComponent<LifeComponent>();
            c3.Lives = 2;

            PhysicsComponent c7 = entity.CreateComponent<PhysicsComponent>();
            c7.Vector = new Vecteur2D(1,0);
            c7.Move = new Vecteur2D();
            
            TypeComponent c5 = entity.CreateComponent<TypeComponent>();
            c5.TypeOfObject = TypeOfObject.CONTROLABLE;      

            FireComponent c9 = entity.CreateComponent<FireComponent>();  
        }

        public void EnemyLine(double xShift, double yShift, double speedX)
        {
            for (int i = 0; i < 5; i++)
            {
                Enemy(xShift+i*50, yShift, speedX);
            }
        }
        
        public void Enemy(double xShift, double yShift, double speedX)
        {
            Entity enemy = WorldEntityManager.CreateEntity();             

            RenderComponent c4 = enemy.CreateComponent<RenderComponent>();
            c4.Image = new Bitmap(SpaceInvaders.Properties.Resources.ship5);

            PositionComponent c5 = enemy.CreateComponent<PositionComponent>();
            c5.Position = new Vecteur2D(xShift,yShift);

            LifeComponent c6 = enemy.CreateComponent<LifeComponent>();
            c6.Lives = 1;
            
            PhysicsComponent c8 = enemy.CreateComponent<PhysicsComponent>();
            c8.Vector = new Vecteur2D(speedX, c4.Image.Height);
            //c8.Vector = new Vecteur2D(0, c4.Image.Height);
            c8.Move = new Vecteur2D();
            
            TypeComponent c7 = enemy.CreateComponent<TypeComponent>();
            c7.TypeOfObject = TypeOfObject.AI;      
                      
            EnemyBlockComponent c9 = enemy.CreateComponent<EnemyBlockComponent>();
            c9.FireProbability = 8;

            FireComponent c10 = enemy.CreateComponent<FireComponent>();
        }
        
        public void Bunker(double x)
        {
            Entity bunker = WorldEntityManager.CreateEntity();

            RenderComponent c1 = bunker.CreateComponent<RenderComponent>();
            c1.Image = new Bitmap(SpaceInvaders.Properties.Resources.bunker2);

            PositionComponent c2 = bunker.CreateComponent<PositionComponent>();
            c2.Position = new Vecteur2D(x, 480.0);

            PhysicsComponent c3 = bunker.CreateComponent<PhysicsComponent>();
            c3.Vector = new Vecteur2D(0,0);
            c3.Move = new Vecteur2D();

            TypeComponent c5 = bunker.CreateComponent<TypeComponent>();
            c5.TypeOfObject = TypeOfObject.STATIC;            

            LifeComponent c4 = bunker.CreateComponent<LifeComponent>();
            c4.Lives = c1.NumberOfPixel;        
        }   
        
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
            c5.SourceType = node.TypeComponent.TypeOfObject;
             
            PositionComponent c3 = entity.CreateComponent<PositionComponent>();
            c3.Position = new Vecteur2D(x,y);

            RenderComponent c4 = entity.CreateComponent<RenderComponent>();
            c4.Image = new Bitmap(SpaceInvaders.Properties.Resources.shoot1);

            return entity;
        }
        
        public Entity newPlayerMissile(PlayerComposition node)
        {
            double x = node.Position.X + node.Render.Image.Width / 2;
            double y = node.Position.Y;
            double speedY = -2;
            
            Entity entity = WorldEntityManager.CreateEntity();
            LifeComponent c1 = entity.CreateComponent<LifeComponent>();
            c1.Lives = 1;            
            
            PhysicsComponent c2 = entity.CreateComponent<PhysicsComponent>();
            c2.Vector = new Vecteur2D(0, speedY);
            c2.Move = new Vecteur2D(0, speedY);
            
            TypeComponent c5 = entity.CreateComponent<TypeComponent>();
            c5.TypeOfObject = TypeOfObject.MISSILE;
            c5.SourceType = node.TypeComponent.TypeOfObject;
            
            PositionComponent c3 = entity.CreateComponent<PositionComponent>();
            c3.Position = new Vecteur2D(x,y);

            RenderComponent c4 = entity.CreateComponent<RenderComponent>();
            c4.Image = new Bitmap(SpaceInvaders.Properties.Resources.shoot1);

            return entity;
        }

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
        
        public void CreateGame()
        {                                 
            
            /////SpaceShip////
            SpaceShip();                     
            
            //////Enemy///////
            EnemyLine(200, 30, 0.5);           
            EnemyLine(200,60, 0.5);
            
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