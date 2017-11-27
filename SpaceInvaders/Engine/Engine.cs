using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

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
            //newBall();  
            
            /*newSpaceship();
          
            newBunker(gameSize.Width / 2 - 300);
            newBunker(gameSize.Width / 2 - 50);
            newBunker(gameSize.Width / 2 + 200);
           

            int basePositionX = 200;
            int number = 2;
            Bitmap image = SpaceInvaders.Properties.Resources.ship7;
            int positionY = 400;
            
            EnemyBlockComponent c5 = new EnemyBlockComponent();           
            c5.NumberOfEnemy = number;
            //c5.PositionInLine = i;
            c5.Position = new Vecteur2D(basePositionX, positionY);
            //c5.Size = new Vecteur2D((number-1) * 50 + (number-1) * c1.Image.Width, c1.Image.Height);
            c5.Size = new Vecteur2D(200, image.Height);
            
            Entity block = new Entity(c5);
            entityList.Add(block);
            
            newBlockEnemy(2, SpaceInvaders.Properties.Resources.ship7, 400, c5);
            newBlockEnemy(3, SpaceInvaders.Properties.Resources.ship6, 450, c5);          
            //newBlockEnemy(5);       
            
            */
            
            WorldEntityManager = new EntityManager();
            
            //////SpaceShip///////
            Entity entity = WorldEntityManager.CreateEntity();             

            RenderComponent c1 = entity.CreateComponent<RenderComponent>();
            c1.Image = new Bitmap(SpaceInvaders.Properties.Resources.ship3);

            PositionComponent c2 = entity.CreateComponent<PositionComponent>();
            c2.Position = new Vecteur2D(300.0,550.0);

            LifeComponent c3 = entity.CreateComponent<LifeComponent>();
            c3.Lives = 2;

            PhysicsComponent c7 = entity.CreateComponent<PhysicsComponent>();
            c7.Vector = new Vecteur2D();
            c7.TypeOfObject = TypeOfObject.CONTROLABLE;
            
            //////Enemy///////
            /*Entity enemy = WorldEntityManager.CreateEntity();             
            
            enemy.CreateComponent<RenderComponent>();
            enemy.CreateComponent<PositionComponent>();
            enemy.CreateComponent<LifeComponent>();

            RenderComponent c4 = enemy.GetComponent<RenderComponent>();
            c4.Image = new Bitmap(SpaceInvaders.Properties.Resources.ship5);

            PositionComponent c5 = enemy.GetComponent<PositionComponent>();
            c5.Position = new Vecteur2D(300.0,100.0);

            LifeComponent c6 = enemy.GetComponent<LifeComponent>();
            c6.Lives = 2;
            */
            
            
            


            ImageRenderSystem renderSystem = new ImageRenderSystem();
            renderSystem.Initialize(this);
            systemsList.Add(renderSystem);
            
            MovablePhysicsSystem movableSystem = new MovablePhysicsSystem();
            movableSystem.Initialize(this);
            systemsList.Add(movableSystem);
            
            PlayerInputSystem playerSystem = new PlayerInputSystem();
            playerSystem.Initialize(this);
            systemsList.Add(playerSystem);
            
            

            /*systemsList.Add(new GameEngineSystem());
            systemsList.Add(new ImageRenderSystem());
            systemsList.Add(new ShapeRenderSystem());
            systemsList.Add(new PlayerInputSystem());
            systemsList.Add(new MovablePhysicsSystem());
            systemsList.Add(new AIInputSystem());
            systemsList.Add(new CollisionSystem());*/
            
            CurrentGameState = GameState.PLAY;
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

        public Entity newMissile(double x, double y, int lives)
        {
            LifeComponent c1 = new LifeComponent();    
            c1.Lives = lives;
            
            PhysicsComponent c2 = new PhysicsComponent();
            c2.Vector = new Vecteur2D(0, -2);
            c2.TypeOfObject = TypeOfObject.MOVABLE;
           
            PositionComponent c3 = new PositionComponent();
            c3.Position = new Vecteur2D(x, y);
            
            RenderComponent c4 = new RenderComponent();
            c4.Image = new Bitmap(SpaceInvaders.Properties.Resources.shoot1);
            
            Entity missile = new Entity(c1, c2, c3, c4);
            entityList.Add(missile);

            return missile;
        }

        public void newSpaceship()
        {
            RenderComponent c1 = new RenderComponent();
            c1.Image = new Bitmap(SpaceInvaders.Properties.Resources.ship3);
            
            PositionComponent c2 = new PositionComponent();
            c2.Position = new Vecteur2D(300.0,550.0);
            
            PhysicsComponent c3 = new PhysicsComponent();
            c3.Vector = new Vecteur2D();
            c3.TypeOfObject = TypeOfObject.CONTROLABLE;

            FireComponent c4 = new FireComponent();
            c4.Entity = newMissile(c2.X + c1.Image.Width/2, c2.Y, 0);

            Entity test = new Entity(c1, c2, c3, c4);
            //Entity test = new Entity(c1, c2, c3);
            entityList.Add(test);
            
        }     
        
        public void newBunker(double x)
        {
            RenderComponent c1 = new RenderComponent();
            c1.Image = new Bitmap(SpaceInvaders.Properties.Resources.bunker2);
            
            PositionComponent c2 = new PositionComponent();
            c2.Position = new Vecteur2D(x, 480.0);
            
            PhysicsComponent c3 = new PhysicsComponent();
            c3.Vector = new Vecteur2D(0,0);
            c3.TypeOfObject = TypeOfObject.STATIC;
            
            LifeComponent c4 = new LifeComponent();
            c4.Lives = c1.NumberOfPixel;                       
            
            Entity test = new Entity(c1, c2, c3, c4);
            entityList.Add(test);
        }   

        public void newBlockEnemy(int number, Bitmap image, int positionY, EnemyBlockComponent c5)
        {
            int basePositionX = 200;                       
            
            for (int i = 1; i <= number; i++)
            {
                RenderComponent c1 = new RenderComponent();
                c1.Image = new Bitmap(image);

                PositionComponent c2 = new PositionComponent();
                c2.Position = new Vecteur2D(basePositionX + (i-1) * 50, positionY);

                PhysicsComponent c3 = new PhysicsComponent();
                c3.Vector = new Vecteur2D(0.5, 10);
                c3.TypeOfObject = TypeOfObject.AI;
                
                LifeComponent c4 = new LifeComponent();
                c4.Lives = 3;                                

                Entity entity = new Entity(c1, c2, c3, c4, c5);
                //Entity entity = new Entity(c1, c2, c3, c4);
                entityList.Add(entity);
            }
        }

        public Entity newEnemy(double x, double y)
        {
            LifeComponent c1 = new LifeComponent();    
            c1.Lives = 1;
            
            PhysicsComponent c2 = new PhysicsComponent();
            c2.Vector = new Vecteur2D(0, -0.5);
            c2.TypeOfObject = TypeOfObject.MOVABLE;
           
            PositionComponent c3 = new PositionComponent();
            c3.Position = new Vecteur2D(x, y);
            
            RenderComponent c4 = new RenderComponent();
            c4.Image = new Bitmap(SpaceInvaders.Properties.Resources.ship8);
            
            Entity missile = new Entity(c1, c2, c3, c4);
            entityList.Add(missile);

            return missile;
        }
        
        public void newBlockEnemy2(int i)
        {
            RenderComponent c1 = new RenderComponent();
            c1.Image = new Bitmap(SpaceInvaders.Properties.Resources.ship8);
            
            PositionComponent c2 = new PositionComponent();
            c2.Position = new Vecteur2D(200+i*50, 150);
            
            PhysicsComponent c3 = new PhysicsComponent();
            c3.Vector = new Vecteur2D(1,0);
            c3.TypeOfObject = TypeOfObject.AI;
            
            Entity entity = new Entity(c1, c2, c3);            
            entityList.Add(entity);
        }*/
        
        public void Draw(Graphics graphics)
        {          
            if (CurrentGameState == GameState.PLAY)
            {
                foreach (var system in systemsList)
                {
                    if (system as IRenderSystem != null)
                    {
                        ((IRenderSystem) system).Update(this, graphics);
                    }
                }
            }
            else
            {
                graphics.DrawString("Pause",new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel), new SolidBrush(Color.Black), new Point(GameSize.Width/2-20, GameSize.Height/2));            
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
                    if (system as IEngineSystem != null)
                    {
                        ((IEngineSystem) system).Update(this);
                    }
                }
            
        }
        
        void CreateGame()
        {
                         
        }

        public List<Entity> getEntity()
        {
            return entityList;
        }
    }
}