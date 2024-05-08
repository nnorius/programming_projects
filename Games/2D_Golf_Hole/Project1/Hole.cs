using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class Hole : DrawableGameComponent
    {

        public Vector2 holePos = new Vector2(990, 400);
        public Rectangle holeRectangle;
        public Microsoft.Xna.Framework.Color[] holeTextureData;

        Texture2D Texture { get; set; }
        readonly Game1 _game;
        public Hole(Game1 game) : base(game)
        {
            _game = game;
        }

        protected override void LoadContent()
        {
            Texture = _game.Content.Load<Texture2D>("hole");

            //get color data for collision calculation
            holeTextureData = new Color[Texture.Width * Texture.Height];
            Texture.GetData(holeTextureData);

            // Get the bounding rectangle
            holeRectangle = new Rectangle((int)holePos.X, (int)holePos.Y, Texture.Width, Texture.Height);
        }

        public override void Draw(GameTime gameTime)
        {

            _game.SpriteBatch.Draw(Texture, holePos, Color.White);

        }
    }
}
