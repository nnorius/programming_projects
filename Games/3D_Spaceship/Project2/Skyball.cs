using BEPUphysics.Entities.Prefabs;
using BEPUphysics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    internal class Skyball : DrawableGameComponent
    {

        readonly Game1 game;

        private Model sky;
        public Vector3 modelPosition = Vector3.Zero;  
        float modelScale;
        float modelRotation = 0.0f;



        public Skyball(Game1 game, Vector3 position, float scale) : base(game)
        {

            this.game = game;

            modelScale = scale;
            modelPosition = position;

        }

        protected override void LoadContent()
        {
            sky = game.Content.Load<Model>("Models\\Skyb");
       
            base.LoadContent();
        }

        public override void Initialize()
        {

            base.Initialize();

        }



        public override void Draw(GameTime gameTime)
        {

            //taken from example by James Lathrop
            // Copy any parent transforms.
            Matrix[] transforms = new Matrix[sky.Bones.Count];
            sky.CopyAbsoluteBoneTransformsTo(transforms);
            // Draw the model. A model can have multiple meshes, so loop.
            foreach (ModelMesh mesh in sky.Meshes)
            {
                // This is where the mesh orientation is set, as well
                // as our camera and projection.
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateScale(modelScale) * Matrix.CreateRotationX(modelRotation)
                        * Matrix.CreateTranslation(modelPosition);
                    effect.View = game.view;
                    effect.Projection = game.projection;
                }
                // Draw the mesh, using the effects set above.
                mesh.Draw();
            }


            base.Draw(gameTime);
        }



        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }



    }
}
