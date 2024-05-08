using BEPUphysics;
using BEPUphysics.Entities.Prefabs;
using BEPUutilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Quaternion = BEPUutilities.Quaternion;
using Vector3 = BEPUutilities.Vector3;
using Matrix = Microsoft.Xna.Framework.Matrix;
using System.Diagnostics;


namespace Project2
{


    internal class Spaceship : DrawableGameComponent
    {
        private const int Radius = 1;
        readonly Game1 game;
        public Sphere _spaceEntity;
        float modelScale = 1f;
        private Model spaceship;
        private Texture2D spaceshipTexture;
        public bool started = false;

        Vector3 direction = Vector3.Zero;

        Vector3 newCameraPos = Vector3.Zero;
        Microsoft.Xna.Framework.Vector3 cameraLookUp = Microsoft.Xna.Framework.Vector3.Up;
        Microsoft.Xna.Framework.Vector3 viewDirection = Microsoft.Xna.Framework.Vector3.Zero;

       // Microsoft.Xna.Framework.Vector3 modelPosition = Microsoft.Xna.Framework.Vector3.Zero;  

        //private Camera _camera;
        public Matrix projection;
        public Matrix view;
        public Matrix world;

        // Create and set the position of the camera in world space, for our view matrix.
        public Microsoft.Xna.Framework.Vector3 cameraPosition = new Microsoft.Xna.Framework.Vector3(0.0f, 0.0f, -15.0f);

        Space space;
        private float _aspectRatio;
        private Microsoft.Xna.Framework.Matrix _worldMatrix;
        private float shipRadius;

        Vector3 localForce = Vector3.Zero;

        Vector3 globalForce = new Vector3(0, 0, 0); 

        Vector3 modelVelocityAdd = Vector3.Zero;

        Clock clock;


        public Spaceship(Game1 game, Clock clock) : base (game) {

            this.game = game;
            this.clock = clock;
            space = (Space)Game.Services.GetService(typeof(Space));
            //space.ForceUpdater.Gravity = new Vector3(0, -9.81f, 0);

        }

        protected override void LoadContent()
        {
            spaceship = game.Content.Load<Model>("Models\\origship4");

            //shipRadius = spaceship.Meshes[0].BoundingSphere.Radius;
            shipRadius = 3f;
            _spaceEntity = new Sphere(new BEPUutilities.Vector3(0, 0, 0), shipRadius, 1);
            //_spaceEntity.Material.Bounciness = 1;
            //_spaceEntity.Material.StaticFriction = 40;
            space.Add(_spaceEntity);
            _spaceEntity.Orientation = Quaternion.Identity;


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

            view = Microsoft.Xna.Framework.Matrix.CreateLookAt(cameraPosition, viewDirection, cameraLookUp);
            projection = Microsoft.Xna.Framework.Matrix.CreatePerspectiveFieldOfView(BEPUutilities.MathHelper.ToRadians(45.0f), game.aspectRatio, 1.0f, 30000.0f);
            world = _worldMatrix;

            foreach (ModelMesh mesh in spaceship.Meshes)
            {

                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();

                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                }

                mesh.Draw();
            }

            base.Draw(gameTime);
        }

        

        public override void Update(GameTime gameTime)
        {
           _worldMatrix = MathConverter.Convert(_spaceEntity.WorldTransform);
            KeyboardState currentKeyState = Keyboard.GetState();

            _spaceEntity.ActivityInformation.Activate();

            _spaceEntity.AngularDamping = 0.5f;

            Vector3 offset = new Vector3(0, 2, -15);

            //find model position
            Microsoft.Xna.Framework.Vector3 spacepos = MathConverter.Convert(_spaceEntity.Position);

            spacepos += MathConverter.Convert(_spaceEntity.WorldTransform.Backward) * offset.Z;
            spacepos += MathConverter.Convert(_spaceEntity.WorldTransform.Up) * offset.Y;


            cameraPosition = spacepos;

            //Up vector rotated by ship orientation 
            cameraLookUp = MathConverter.Convert(Quaternion.Transform(new Vector3(0, 1,0), _spaceEntity.Orientation));

            viewDirection = MathConverter.Convert(_spaceEntity.Position);

            //friction
            _spaceEntity.LinearVelocity *= 0.99f;

            Vector3 angRot;
            float rot = 0.1f;
            if (currentKeyState.IsKeyDown(Keys.A))
            {
                angRot = new Vector3(0, rot, 0 );
                globalForce = angRot;
                localForce = Quaternion.Transform(globalForce, _spaceEntity.Orientation);
                _spaceEntity.ApplyAngularImpulse(ref localForce);

                _spaceEntity.ActivityInformation.Activate();

            }

            if (currentKeyState.IsKeyDown(Keys.D)) {
                angRot = new Vector3(0, -rot, 0);
                globalForce = angRot;
                localForce = Quaternion.Transform(globalForce, _spaceEntity.Orientation);
                _spaceEntity.ApplyAngularImpulse(ref localForce);
                _spaceEntity.ActivityInformation.Activate();

            }

            if (currentKeyState.IsKeyDown(Keys.W)) {
                angRot = new Vector3(-rot, 0, 0);
                globalForce = angRot;
                localForce = Quaternion.Transform(globalForce, _spaceEntity.Orientation);
                _spaceEntity.ApplyAngularImpulse(ref localForce); _spaceEntity.ActivityInformation.Activate();
            }

            if (currentKeyState.IsKeyDown(Keys.S)){
                angRot = new Vector3(rot, 0, 0);
                globalForce = angRot;
                localForce = Quaternion.Transform(globalForce, _spaceEntity.Orientation);
                _spaceEntity.ApplyAngularImpulse(ref localForce);
                _spaceEntity.ActivityInformation.Activate();
            }


            if (currentKeyState.IsKeyDown(Keys.Space)) //gas
            {

                if (!started)
                {
                    clock.StartTime();
                    started = true;
                }


                _spaceEntity.LinearVelocity = _spaceEntity.WorldTransform.Backward*25;

                
                _spaceEntity.ActivityInformation.Activate();


            }


            _spaceEntity.ActivityInformation.Activate();

            base.Update(gameTime);
        }

        public Vector3 GetVelocity()
        {

            return _spaceEntity.LinearVelocity;
        }

    }
}
