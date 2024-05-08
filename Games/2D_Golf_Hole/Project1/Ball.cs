using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;


namespace Project1
{


    internal class Ball : DrawableGameComponent
    {
        
        public Vector2 ballPos = new Vector2(280, 600);       //ball is 45 x 45 pixels
        public Rectangle ballRectangle;
        public Microsoft.Xna.Framework.Color[] ballTextureData;
        public Vector2 Velocity = new Vector2(0, 0);

        public float _rotation;
        public float rotationVelocity = 1f;
        public float linearVelocity = 0f;
        public float drag = 0.994f;
        public float acceleration = 1f;

        public Color color = Color.White;
        Texture2D Texture { get; set; }
        Texture2D Texture2 { get; set; }

        readonly Game1 _game;
        KeyboardState prevState = Keyboard.GetState(); // used to track when a button is released

        public Vector2 origin;
        public Vector2 origin2;

        public Vector2 direction = new Vector2(0, 0);

        double checkTime;
        const double coolDown = 5.0;


        public float transparency = 1.0f; //make arrow transparent after ball is hit (space released)
        public Vector2 arrowPos = new Vector2(280, 600);


        public Ball(Game1 game) : base(game) {
            _game = game;
            

        }

        protected override void LoadContent()
        {
            Texture = _game.Content.Load<Texture2D>("ball");
            origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
            //get color data for collision calculation
            ballTextureData = new Color[Texture.Width * Texture.Height];
            Texture.GetData(ballTextureData);

            Texture2 = _game.Content.Load<Texture2D>("aim");
            origin2 = new Vector2(Texture2.Width / 2, Texture2.Height / 2);

        }
    

        public override void Draw(GameTime gameTime)
        {
            //_game.SpriteBatch.Draw(Texture, ballPos, color);
           _game.SpriteBatch.Draw(Texture2, arrowPos, null, Color.White * transparency, _rotation, origin2, 1, SpriteEffects.None, 0f);

           _game.SpriteBatch.Draw(Texture, ballPos, null, color, _rotation, origin, 1, SpriteEffects.None, 0f);

        }

        public override void Update(GameTime gameTime)
        {
            // Get the bounding rectangle of the ball
            // When changed SpriteBatch Draw function to have rotation and origin parameters the ballPos is now in center of sprite rather
            // than left corner so had to offset bounding box by the ball's radius (22.5)
            ballRectangle = new Rectangle((int)ballPos.X-22, (int)ballPos.Y-22, Texture.Width, Texture.Height);

            //If the ball is going very slow change velocity to 0 or it takes too long to stop moving
            //ball Velocity was changing to 0 during calculations like wall checks so added a cooldown
            if ((Velocity.X < 5f && Velocity.X > -5f && gameTime.TotalGameTime.TotalSeconds - checkTime > coolDown)
                ||(Velocity.Y < 5f && Velocity.Y > 5f && gameTime.TotalGameTime.TotalSeconds - checkTime > coolDown))
            {

                    Velocity = Vector2.Zero;
                    transparency = 1f;
            }   
            Velocity *= drag;
            Velocity *= acceleration;
            ballPos += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            arrowPos = ballPos;

            KeyboardState curState = Keyboard.GetState();

            //debugging only
            //if (curState.IsKeyDown(Keys.Right))
            //    ballPos.X += 2;
            //if (curState.IsKeyDown(Keys.Left))
            //    ballPos.X -= 2;
            //if (curState.IsKeyDown(Keys.Up))
            //    ballPos.Y -= 2;
            //if (curState.IsKeyDown(Keys.Down))
            //    ballPos.Y += 2;

            // Rotate ball aim using 'A' and 'D'
            if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _rotation -= MathHelper.ToRadians(rotationVelocity);
            }
            else if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _rotation += MathHelper.ToRadians(rotationVelocity);
            }
                // Move in direction sprite is pointing. Subtracted 90 degrees because our sprite is facing up and default is facing right
                direction = new Vector2((float)Math.Cos(MathHelper.ToRadians(90)-_rotation), -(float)Math.Sin(MathHelper.ToRadians(90) - _rotation));
               
            // When space releases shoot ball in direction aiming

            if (curState.IsKeyUp(Keys.Space) && prevState.IsKeyDown(Keys.Space) && Velocity == Vector2.Zero)
            {

                //change linear velocity based on hit strength
                _game.hitBall.Play();
                Velocity = direction * linearVelocity;
                checkTime = gameTime.TotalGameTime.TotalSeconds;
                transparency = 0.0f;

            }
            prevState = curState;

            base.Update(gameTime);

        }

        public void setLinearVelocity(double elapsedTime)
        {
            double amount = elapsedTime / 4.0;
            linearVelocity = (float)amount;
        }

     

    }
}
