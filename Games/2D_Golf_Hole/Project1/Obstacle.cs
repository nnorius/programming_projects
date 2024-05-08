using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class Obstacle : DrawableGameComponent
    {

        public Vector2 obstPos = new Vector2(850, 200);
        public Rectangle obstRectangle;
        public Microsoft.Xna.Framework.Color[] obstTextureData;

        public Vector2 cornerPos = new Vector2(0, 0);
        public Rectangle cornerRectangle;
        public Microsoft.Xna.Framework.Color[] cornerTextureData;

        Texture2D Texture { get; set; }
        Texture2D Texture2 { get; set; }
        readonly Game1 _game;
        public Obstacle(Game1 game) : base(game)
        {
            _game = game;
        }

        protected override void LoadContent()
        {
            Texture = _game.Content.Load<Texture2D>("obstacle");
            Texture2 = _game.Content.Load<Texture2D>("corner");

            //get color data for collision calculation
            obstTextureData = new Color[Texture.Width * Texture.Height];
            Texture.GetData(obstTextureData);

            cornerTextureData = new Color[Texture2.Width * Texture2.Height];
            Texture2.GetData(cornerTextureData);

            // Get the bounding rectangle
            obstRectangle = new Rectangle((int)obstPos.X, (int)obstPos.Y, Texture.Width, Texture.Height);
            cornerRectangle = new Rectangle((int)cornerPos.X, (int)cornerPos.Y, Texture2.Width, Texture2.Height);

        }

        public override void Draw(GameTime gameTime)
        {

            _game.SpriteBatch.Draw(Texture, obstPos, Color.White);
            _game.SpriteBatch.Draw(Texture2, cornerPos, Color.White);


        }
    }
}
