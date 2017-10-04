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
        
        
        public Engine(Size gameSize)
        {
            GameSize = gameSize;          
            //newBall();  
            
            newSpaceship();
          
            newBunker(gameSize.Width / 2 - 300);
            newBunker(gameSize.Width / 2 - 50);
            newBunker(gameSize.Width / 2 + 200);

          
            newBlockEnemy(5);
            //newBlockEnemy(5);       
            

            systemsList.Add(new GameEngineSystem());
            systemsList.Add(new ImageRenderSystem());
            systemsList.Add(new ShapeRenderSystem());
            systemsList.Add(new PlayerInputSystem());
            systemsList.Add(new MovablePhysicsSystem());
            systemsList.Add(new AIInputSystem());
            systemsList.Add(new CollisionSystem());
            
            CurrentGameState = GameState.PLAY;
        }

        public void newBall()
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

        public Entity newMissile(double x, double y)
        {
            LifeComponent c1 = new LifeComponent();    
            c1.Lives = 0;
            
            PhysicsComponent c2 = new PhysicsComponent();
            c2.Vector = new Vecteur2D(0, -0.5);
            c2.TypeOfObject = TypeOfObject.MOVABLE;
           
            PositionComponent c3 = new PositionComponent();
            c3.Position = new Vecteur2D(x, y);
            
            RenderComponent c4 = new RenderComponent();
            c4.Image = SpaceInvaders.Properties.Resources.shoot1;
            
            Entity missile = new Entity(c1, c2, c3, c4);
            entityList.Add(missile);

            return missile;
        }

        public void newSpaceship()
        {
            RenderComponent c1 = new RenderComponent();
            c1.Image = SpaceInvaders.Properties.Resources.ship3;
            
            PositionComponent c2 = new PositionComponent();
            c2.Position = new Vecteur2D(300.0,550.0);
            
            PhysicsComponent c3 = new PhysicsComponent();
            c3.Vector = new Vecteur2D();
            c3.TypeOfObject = TypeOfObject.CONTROLABLE;

            FireComponent c4 = new FireComponent();
            c4.Entity = newMissile(c2.X + c1.Image.Width/2, c2.Y);

            Entity test = new Entity(c1, c2, c3, c4);
            //Entity test = new Entity(c1, c2, c3);
            entityList.Add(test);
            
        }     
        
        public void newBunker(double x)
        {
            RenderComponent c1 = new RenderComponent();
            c1.Image = SpaceInvaders.Properties.Resources.bunker2;
            
            PositionComponent c2 = new PositionComponent();
            c2.Position = new Vecteur2D(x, 480.0);
            
            PhysicsComponent c3 = new PhysicsComponent();
            c3.Vector = new Vecteur2D(0,0);
            c3.TypeOfObject = TypeOfObject.STATIC;
            
            LifeComponent c4 = new LifeComponent();
            c4.Lives = 10;
            
            Entity test = new Entity(c1, c2, c3, c4);
            entityList.Add(test);
        }   

        public void newBlockEnemy(int number)
        {
            for (int i = 1; i < number; i++)
            {
                RenderComponent c1 = new RenderComponent();
                c1.Image = SpaceInvaders.Properties.Resources.ship7;

                PositionComponent c2 = new PositionComponent();
                c2.Position = new Vecteur2D(200 + i * 50, 200);

                PhysicsComponent c3 = new PhysicsComponent();
                c3.Vector = new Vecteur2D(1, 0);
                c3.TypeOfObject = TypeOfObject.AI;
                
                LifeComponent c4 = new LifeComponent();
                c4.Lives = 1;

                /*EnemyBlockComponent c4 = new EnemyBlockComponent();
                c4.NumberOfEnemy = number;
                c4.PositionInLine = i;

                Entity entity = new Entity(c1, c2, c3, c4);*/
                Entity entity = new Entity(c1, c2, c3, c4);
                entityList.Add(entity);
            }
        }
        
        public void newBlockEnemy2(int i)
        {
            RenderComponent c1 = new RenderComponent();
            c1.Image = SpaceInvaders.Properties.Resources.ship8;
            
            PositionComponent c2 = new PositionComponent();
            c2.Position = new Vecteur2D(200+i*50, 150);
            
            PhysicsComponent c3 = new PhysicsComponent();
            c3.Vector = new Vecteur2D(1,0);
            c3.TypeOfObject = TypeOfObject.AI;
            
            Entity entity = new Entity(c1, c2, c3);            
            entityList.Add(entity);
        }
        
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