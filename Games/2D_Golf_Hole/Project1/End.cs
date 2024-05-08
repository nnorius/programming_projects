using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class End : DrawableGameComponent
    {

            private SpriteFont font;
            public float transparency = 0.0f;

            readonly Game1 _game;
            public End(Game1 game) : base(game)
            {
                _game = game;
            }

            protected override void LoadContent()
            {
                font = _game.Content.Load<SpriteFont>("End");
            }

            public override void Draw(GameTime gameTime)
            {

                //_game.SpriteBatch.Draw(Texture, new Vector2(0, 0), Color.White);
                _game.SpriteBatch.DrawString(font, "Game Over", new Vector2(300, 300), Color.Black * transparency);
            }

            public override void Update(GameTime gameTime)
            {


                base.Update(gameTime);
            }
        }
}
