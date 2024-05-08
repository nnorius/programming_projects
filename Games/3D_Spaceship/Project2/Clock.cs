using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace Project2
{
    internal class Clock : DrawableGameComponent
    {
        private SpriteFont timer;
        readonly Game1 game;
        float seconds = 0f;
        float mil_sec = 0f;
        bool start = false;
        public int missed = 0;

        Texture2D _texture;

        public Clock(Game1 game) : base (game){ 
            this.game = game;
        
        }

        protected override void LoadContent()
        {

            timer = game.Content.Load<SpriteFont>("File");
            _texture = new Texture2D(GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.White });
        }

        public override void Draw(GameTime gameTime)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            

            string str = time.ToString(@"mm\:ss\:fff");

            //Add one minute to score for each missed hoop
            int minScore = time.Minutes + missed;

            game._spriteBatch.Draw(_texture, new Rectangle(4, 4, 640, 40), Color.White*0.4f);
            game._spriteBatch.DrawString(timer, str+"   Missed: "+ missed+ "    Score:"
                + minScore + '.'+time.Seconds, new Vector2(10, 10), Color.Gold);



        }


        public void StartTime()
        {
            start = true;
        }

        public void StopTime()
        {
            start = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (start)
            {
                seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

            }
            base.Update(gameTime);

        }


        }
}
