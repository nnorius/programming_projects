using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Project1
{
    internal class Downhill : DrawableGameComponent
    {

        public Vector2 downPos = new Vector2(540, 126);
        public Rectangle downRectangle;
        public Microsoft.Xna.Framework.Color[] downTextureData;
        public bool reverse = false; // if gone downhill reverse acceleration to uphill next time
        public bool onSlope = false; //need to know if currently on slope or not 
        Texture2D Texture { get; set; }
        readonly Game1 _game;
        public Downhill(Game1 game) : base(game)
        {
            _game = game;
        }

        protected override void LoadContent()
        {
            Texture = _game.Content.Load<Texture2D>("hill");

            //get color data for collision calculation
            downTextureData = new Color[Texture.Width * Texture.Height];
            Texture.GetData(downTextureData);

            // Get the bounding rectangle
            downRectangle = new Rectangle((int)downPos.X, (int)downPos.Y, Texture.Width, Texture.Height);
        }

        public override void Draw(GameTime gameTime)
        {

            _game.SpriteBatch.Draw(Texture, downPos, Color.White);

        }
    }
}
