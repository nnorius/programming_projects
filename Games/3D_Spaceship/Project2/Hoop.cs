using BEPUphysics.Entities.Prefabs;
using BEPUphysics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEPUphysics.BroadPhaseEntries.Events;
using SharpDX.MediaFoundation.DirectX;

namespace Project2
{
    internal class Hoop : DrawableGameComponent
    {

        //ContactEventManager<Box> manager;

        readonly Game1 game;
        //Sphere _hoopEntity;
        Box _hoopTop;
        Box _hoopBottom;
        Box _hoopLeft;
        Box _hoopRight;

        private Model hoop;
        private float hoopRadius;
        // Vector3 modelPosition = Vector3.Zero;  
        private Vector3 modelPosition;// = new Vector3(0.0f, 0.0f, 0.0f);
        //float modelRotation = 0.0f;
        private Vector3 rotation;

        public bool nextHoop = false;
        public Trigger trigger;
        public bool completed = false;

        // Create and set the position of the camera in world space, for our view matrix.
        Vector3 cameraPosition = new Vector3(0.0f, 0.0f, -15.0f);

        Space space;
        private float _aspectRatio;
        private Matrix _worldMatrix;
        BoundingBox bound;
        //private Camera _camera;

        public Hoop(Game1 game, Vector3 position, Vector3 rotation) : base(game)
        {

            this.game = game;
            space = (Space)Game.Services.GetService(typeof(Space));
            

            modelPosition = position;
            this.rotation = rotation;

        }

        protected override void LoadContent()
        {
            hoop = game.Content.Load<Model>("Models\\hoop8");
            hoopRadius = hoop.Meshes[0].BoundingSphere.Radius;
            BEPUutilities.Vector3 modPosBepu = MathConverter.Convert(modelPosition);

            //_hoopEntity = new Box(modPosBepu, hoopRadius, hoopRadius, 1f);
            //space.Add(_hoopEntity);

            BEPUutilities.Vector3 top = new BEPUutilities.Vector3(modPosBepu.X, modPosBepu.Y += 9, modPosBepu.Z += 2);
            _hoopTop = new Box(top, 11f, 2f, 2f);

            BEPUutilities.Vector3 bottom = new BEPUutilities.Vector3(modPosBepu.X, modPosBepu.Y -= 14, modPosBepu.Z += 2);
            _hoopBottom = new Box(bottom, 11f, 2f, 2f);

            BEPUutilities.Vector3 left = new BEPUutilities.Vector3(modPosBepu.X+=7f, modPosBepu.Y, modPosBepu.Z-=2);
            _hoopLeft = new Box(left, 2f,11f, 2f);

            BEPUutilities.Vector3 right = new BEPUutilities.Vector3(modPosBepu.X -= 14f, modPosBepu.Y, modPosBepu.Z-=2);
            _hoopRight = new Box(right, 2f, 11f, 2f);




            space.Add(_hoopTop);
            space.Add(_hoopBottom);
            space.Add(_hoopLeft); 
            space.Add(_hoopRight);

            trigger = new Trigger(game, MathConverter.Convert(modelPosition));
           
  

            base.LoadContent();
        }

        public override void Initialize()
        {

            base.Initialize();

        }



        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            //taken from example by James Lathrop
            Matrix[] transforms = new Matrix[hoop.Bones.Count];
            hoop.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in hoop.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {                        
                    effect.EnableDefaultLighting();

                    if (nextHoop)
                    {
                        effect.EmissiveColor = new Vector3(.4f, .4f, .3f);

                    }
                    else
                    {
                        effect.EmissiveColor = new Vector3(0, 0, 0);

                    }


                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateRotationY(rotation.Y) * Matrix.CreateTranslation(modelPosition);

                    effect.View = game.view;
                    effect.Projection = game.projection;

                   // effect.View = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
                    //effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), game.aspectRatio, 1.0f, 10000.0f);
                }
                mesh.Draw();
            }


            base.Draw(gameTime);
        }



        public override void Update(GameTime gameTime)
        {
            //_worldMatrix = MathConverter.Convert(_hoopEntity.WorldTransform);



            if (trigger.collided == true)
            {
                completed = true;

            }

            base.Update(gameTime);
        }

    
}
}
