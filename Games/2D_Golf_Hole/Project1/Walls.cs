using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class Walls : DrawableGameComponent
    {
        const int lineWeight = 4;
        //public Microsoft.Xna.Framework.Color[] wallTextureData;

        // Right square walls
        public Rectangle NSq;
        public Rectangle ESq;
        public Rectangle SSq;
        public Rectangle WUSq; //Upper half West wall
        public Rectangle WLSq; //Lower half West wall

        //Middle strip walls
        Rectangle NMid;
        Rectangle SMid;

        //Left start rectangle walls
        public Rectangle WSt;
        public Rectangle ESt;
        public Rectangle SSt;

        Texture2D Texture { get; set; }
        readonly Game1 _game;
        public Walls(Game1 game) : base(game)
        {
            _game = game;
        }

        protected override void LoadContent()
        {

            Texture = new Texture2D(GraphicsDevice, 1, 1);
            Texture.SetData(new[] { Color.Black });
            //wallTextureData = new Color[Texture.Width * Texture.Height];
            //Texture.GetData(wallTextureData);

            // Right square walls
            NSq = new Rectangle(1225, 43, lineWeight, 565);
            ESq = new Rectangle(704, 43, 525, lineWeight );
            SSq = new Rectangle(704, 604, 525, lineWeight);
            WUSq = new Rectangle(704, 43, lineWeight, 85);  //Upper half West wall
            WLSq = new Rectangle(704, 322, lineWeight, 283);  //Lower half West wall

            ////Middle strip walls
            NMid = new Rectangle(195, 127, 510, lineWeight);
            SMid = new Rectangle(408, 322, 298, lineWeight);

            //Left start rectangle walls
            WSt = new Rectangle(195, 130, lineWeight, 570);
            ESt = new Rectangle(408, 325, lineWeight, 375);
            SSt = new Rectangle(195, 695, 216, lineWeight);
        }

        public override void Draw(GameTime gameTime)
        {

            _game.SpriteBatch.Draw(Texture, WSt, Color.White);
            _game.SpriteBatch.Draw(Texture, ESt, Color.White);
            _game.SpriteBatch.Draw(Texture, SSt, Color.White);
            _game.SpriteBatch.Draw(Texture, NMid, Color.White);
            _game.SpriteBatch.Draw(Texture, SMid, Color.White);
            _game.SpriteBatch.Draw(Texture, WLSq, Color.White);
            _game.SpriteBatch.Draw(Texture, WUSq, Color.White);
            _game.SpriteBatch.Draw(Texture, SSq, Color.White);
            _game.SpriteBatch.Draw(Texture, ESq, Color.White);
            _game.SpriteBatch.Draw(Texture, NSq, Color.White);






        }
    }
}
