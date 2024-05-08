using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;


namespace Project1
{
    internal class Bar :DrawableGameComponent
    {
        public Rectangle frame;
        public Rectangle background;
        public Rectangle fill;
        public int curHeight = 0;

        const int maxFill = -150; //for rectangle to fill upward uses a negative Y value
        Texture2D Texture { get; set; }
        Texture2D Texture2 { get; set; }

        readonly Game1 _game;
        public Bar(Game1 game) : base(game)
        {
            _game = game;
        }


        protected override void LoadContent()
        {
            Texture = new Texture2D(GraphicsDevice, 1, 1);
            Texture.SetData(new[] { Color.White });

            frame = new Rectangle(80, 96, 50, 158);
            background = new Rectangle(84, 100, 42, 150);
            fill = new Rectangle(84+42, 100+150, -42, -150);
            fill.Height = 0;

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {

            _game.SpriteBatch.Draw(Texture, frame, Color.Black);
            _game.SpriteBatch.Draw(Texture, background, Color.CornflowerBlue);
            _game.SpriteBatch.Draw(Texture, fill, Color.OrangeRed);


        }

        public override void Update(GameTime gameTime)
        {
            fill.Height = curHeight;

            base.Update(gameTime);
        }

        public void setHeight(double timeElapsed)
        {
            double time = timeElapsed / 4000.0;
            time = time * 150;
            curHeight = -(int)time;
        }
    }
}
