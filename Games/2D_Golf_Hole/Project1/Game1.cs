using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Drawing;
using System.Runtime.Intrinsics.X86;

namespace Project1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch SpriteBatch {  get; private set; }
        public KeyboardState prevState = Keyboard.GetState();
        Ball ball1;
        Background bg;
        Obstacle obst;
        Hole hole;
        Downhill down;
        Text text;
        Walls walls;
        CollisionComponent cc;
        Bar bar;

        public SoundEffect hitBall;
        public SoundEffect hitWall;
        public SoundEffect inHole;

        const double coolDown = .1;

        //Used to track time space is held down 
        double startTime = 0;
        double maxTime = 4000;
        double totalTime = 0;
        bool started = false;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            // Change the resolution to 720p
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            bg = new Background(this);
            down = new Downhill(this);
            obst = new Obstacle(this);
            hole = new Hole(this);
            ball1 = new Ball(this);
            text = new Text(this);
            walls = new Walls(this);
            bar = new Bar(this);
            cc = new CollisionComponent(this, ball1, walls, hole, obst, down, text);

            Components.Add(bg);
            Components.Add(down);
            Components.Add(obst);
            Components.Add(hole);
            Components.Add(ball1);
            Components.Add(text);
            Components.Add(walls);
            Components.Add(bar);
            Components.Add(cc);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            hitBall = Content.Load<SoundEffect>("hitting_ball");
            hitWall = Content.Load<SoundEffect>("ball_wall");
            inHole = Content.Load<SoundEffect>("ball_hole");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState curState = Keyboard.GetState();
            if (curState.IsKeyDown(Keys.Space)){
                if (!started)
                {
                    startTime = gameTime.TotalGameTime.TotalMilliseconds;
                }

                started = true;
                totalTime = gameTime.TotalGameTime.TotalMilliseconds - startTime;
                if (totalTime > maxTime) totalTime = maxTime;
                if (ball1.Velocity == Vector2.Zero)
                {
                    bar.setHeight(totalTime);
                    ball1.setLinearVelocity(totalTime);
                }

            }            
            
            // add a hit to the score each time the space button is released (space is used to hit the ball)
            if (curState.IsKeyUp(Keys.Space) && prevState.IsKeyDown(Keys.Space))
            {                
                bar.setHeight(0);

                //The arrow pointing in direction of aim disappears after the ball is hit (space)
                if(ball1.Velocity == Vector2.Zero)
                {
                    text.hits++;
                    started = false;
                    totalTime = 0;
                    startTime = 0;
                }

                
                
            }

            prevState = curState;



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

            SpriteBatch.Begin();
            base.Draw(gameTime);
            SpriteBatch.End();
        }


       


    }
}
