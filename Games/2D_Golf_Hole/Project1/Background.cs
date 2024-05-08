using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Project1
{
    internal class Background : DrawableGameComponent
    {
        public Vector2 bgPos = new Vector2(0,0);
        public Rectangle bgRectangle;
        public Microsoft.Xna.Framework.Color[] bgTextureData;

        Texture2D Texture { get; set; }
        readonly Game1 _game;
        public Background(Game1 game) : base(game)
        {
            _game = game;
        }

        protected override void LoadContent()
        {
            Texture = _game.Content.Load<Texture2D>("bg");

            //get color data for collision calculation
            bgTextureData = new Color[Texture.Width * Texture.Height];
            Texture.GetData(bgTextureData);

            // Get the bounding rectangle
            bgRectangle = new Rectangle((int)bgPos.X, (int)bgPos.Y, Texture.Width, Texture.Height);
        }

        public override void Draw(GameTime gameTime)
        {

            _game.SpriteBatch.Draw(Texture, bgPos, Color.White);

        }
    }
}

