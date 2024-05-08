using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Project1
{
    internal class Text : DrawableGameComponent
    {
        private SpriteFont font;
        public int hits = 0;
        KeyboardState prevState = Keyboard.GetState(); // used to track when a button is released


        private SpriteFont font2;
        public float transparency = 0.0f;

        readonly Game1 _game;
        public Text(Game1 game) : base(game)
        {
            _game = game;
        }

        protected override void LoadContent()
        {
            font = _game.Content.Load<SpriteFont>("Hits");
            font2 = _game.Content.Load<SpriteFont>("End");

        }

        public override void Draw(GameTime gameTime)
        {

            _game.SpriteBatch.DrawString(font, "HITS: "+hits, new Vector2(10, 10), Color.Black);
            _game.SpriteBatch.DrawString(font2, "Game Over", new Vector2(300, 300), Color.Black * transparency);

        }


    }
}
