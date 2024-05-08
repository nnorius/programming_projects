using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class CollisionComponent : GameComponent
    {
        Ball ball1;
        //Background bg;
        Obstacle obst;
        Hole hole;
        Downhill down;
        //Arrow arrow;
        End end;
        Walls walls;
        Text text;
        readonly Game1 _game;

        double collisionTime;
        const double coolDown = .1;
        bool gameover = false;
        public CollisionComponent(Game1 game, Ball ball1, Walls walls, Hole hole, Obstacle obst, Downhill down, Text text) : base(game)
        {
            _game = game;
            this.ball1 = ball1;
            this.walls = walls;
            this.hole = hole;
            this.obst = obst;
            this.down = down;
            this.text = text;
        }

        public override void Update(GameTime gameTime)
        {
            // Check collision
            // Cooldown logic based on tutorial by Addison Schmidt
            if ((Intersect(ball1.ballRectangle, ball1.ballTextureData, obst.obstRectangle, obst.obstTextureData) && gameTime.TotalGameTime.TotalSeconds - collisionTime > coolDown)
               || (Intersect(ball1.ballRectangle, ball1.ballTextureData, obst.cornerRectangle, obst.cornerTextureData) && gameTime.TotalGameTime.TotalSeconds - collisionTime > coolDown))

            {
                collisionTime = gameTime.TotalGameTime.TotalSeconds;
                ball1.Velocity = -ball1.Velocity;
                _game.hitWall.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);

            }

            // WSt = new Rectangle(195, 130, lineWeight, 570);
            if (ball1.ballPos.X < (195 + 22))
            {
                ball1.Velocity = Vector2.Reflect(ball1.Velocity, Vector2.UnitX);
                _game.hitWall.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
            }
            // ESt = new Rectangle(408, 325, lineWeight, 375);
            else if (ball1.ballPos.X > (408 - 22) && ball1.ballPos.X < (700) && ball1.ballPos.Y > 325 - 10)
            {
                ball1.Velocity = Vector2.Reflect(ball1.Velocity, Vector2.UnitX);
                _game.hitWall.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
            }
            //SSt = new Rectangle(195, 695, 216, lineWeight);
            else if (ball1.ballPos.X < (410) && ball1.ballPos.Y > 700 - 22)
            {
                ball1.Velocity = Vector2.Reflect(ball1.Velocity, Vector2.UnitY);
                _game.hitWall.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
            }

            //NMid = new Rectangle(195, 127, 510, lineWeight);
            else if (ball1.ballPos.X < (712) && ball1.ballPos.Y < 130 + 22)
            {
                ball1.Velocity = Vector2.Reflect(ball1.Velocity, Vector2.UnitY);
                _game.hitWall.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
            }
            //SMid = new Rectangle(408, 322, 298, lineWeight);
            else if (ball1.ballPos.X > (400) && ball1.ballPos.X < (715) && ball1.ballPos.Y > 325 - 22)
            {
                ball1.Velocity = Vector2.Reflect(ball1.Velocity, Vector2.UnitY);
                _game.hitWall.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
            }
            //NSq = new Rectangle(1225, 43, lineWeight, 565);
            else if (ball1.ballPos.Y < 45 + 22)
            {
                ball1.Velocity = Vector2.Reflect(ball1.Velocity, Vector2.UnitY);
                _game.hitWall.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
            }
            //ESq = new Rectangle(704, 43, 525, lineWeight);
            else if (ball1.ballPos.X > (1229 - 22))
            {
                ball1.Velocity = Vector2.Reflect(ball1.Velocity, Vector2.UnitX);
                _game.hitWall.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
            }
            //SSq = new Rectangle(704, 604, 525, lineWeight);
            else if (ball1.ballPos.X > (450) && ball1.ballPos.Y > 604 - 22)
            {
                ball1.Velocity = Vector2.Reflect(ball1.Velocity, Vector2.UnitY);
                _game.hitWall.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
            }
            //WUSq = new Rectangle(704, 43, lineWeight, 85);  //Upper half West wall
            else if (ball1.ballPos.X < (706 + 22) && ball1.ballPos.Y < 135)
            {
                ball1.Velocity = Vector2.Reflect(ball1.Velocity, Vector2.UnitX);
                _game.hitWall.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
            }
            //WLSq = new Rectangle(704, 322, lineWeight, 283);  //Lower half West wall
            else if (ball1.ballPos.X > (420) && ball1.ballPos.X < (706 + 22) && ball1.ballPos.Y > 315)
            {
                ball1.Velocity = Vector2.Reflect(ball1.Velocity, Vector2.UnitX);
                _game.hitWall.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
            }


            //if gone downhill change acceleration to uphill
            if (Intersect(ball1.ballRectangle, ball1.ballTextureData, down.downRectangle, down.downTextureData) && gameTime.TotalGameTime.TotalSeconds - collisionTime > coolDown)
            {
                down.onSlope = true;
                if (!down.reverse) //reverse tracks if have already gone down slope. If not is downhill
                {
                    ball1.acceleration = 1.03f;

                }
                else //if reverse is true is now uphill
                {
                    ball1.acceleration = .98f;
                }

            }
            else //if not on slope check if was on slope and update associated variables
            {
                if(down.onSlope == true)
                {
                    down.reverse = !down.reverse;
                    down.onSlope = false;
                    ball1.acceleration = 1f;

                }
            }


            //Game over
            if (hole.holeRectangle.Contains(ball1.ballPos) && gameover == false)
            {
                ball1.Velocity = new Vector2(0, 0);
                ball1.ballPos = new Vector2(990 + 30, 400 + 30);
                _game.inHole.Play();
                text.transparency = 1f;
                gameover = true;
            }


            base.Update(gameTime);
        }


        //taken from tutorial at this link:
        //https://www.austincc.edu/cchrist1/GAME1343/PerPixelCollision/PerPixelCollision.htm
        static bool Intersect(Microsoft.Xna.Framework.Rectangle rectangleA, Microsoft.Xna.Framework.Color[] dataA,
            Microsoft.Xna.Framework.Rectangle rectangleB, Microsoft.Xna.Framework.Color[] dataB)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(rectangleA.Top, rectangleB.Top);
            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
            int left = Math.Max(rectangleA.Left, rectangleB.Left);
            int right = Math.Min(rectangleA.Right, rectangleB.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Microsoft.Xna.Framework.Color colorA = dataA[(x - rectangleA.Left) + (y - rectangleA.Top) * rectangleA.Width];
                    Microsoft.Xna.Framework.Color colorB = dataB[(x - rectangleB.Left) + (y - rectangleB.Top) * rectangleB.Width];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        // then an intersection has been found
                        return true;
                    }
                }
            }

            // No intersection found

            return false;
        }


    }
}
