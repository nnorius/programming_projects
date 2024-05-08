using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BEPUphysics;
using BEPUphysics.Entities;
using System.Drawing;
using System.Windows.Forms;
using BEPUutilities;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Matrix = Microsoft.Xna.Framework.Matrix;
using System.Collections;

namespace Project2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        public SpriteBatch _spriteBatch;
        Spaceship _spaceship;
        public Space _space;
        Clock clock;

        RasterizerState skyboxRaster = new RasterizerState();
        RasterizerState defaultRaster = new RasterizerState();

        float angle = 0;
        float distance = 20;

        ArrayList hoops;
        Hoop hoop1;
        Hoop hoop2;
        Hoop hoop3;
        Hoop hoop4;
        Hoop hoop5;
        Hoop hoop6;
        Hoop hoop7;

        int curHoopIndex = 0;
        int missedHoops = 0;
        int hoopScore = 0;

        Skyball sky;

        public Matrix projection;
        public Matrix view;
        public Vector3 cameraPosition;
        public Matrix world;
        public float aspectRatio;


        public Game1()
        {            

            _graphics = new GraphicsDeviceManager(this);

            _graphics.GraphicsProfile = GraphicsProfile.HiDef;

            // Change the resolution to 720p
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            aspectRatio = _graphics.GraphicsDevice.Viewport.AspectRatio;


            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        
        protected override void Initialize()
        {

            skyboxRaster.CullMode = CullMode.CullClockwiseFace;
            defaultRaster.CullMode = CullMode.CullCounterClockwiseFace;
            _graphics.GraphicsDevice.RasterizerState = defaultRaster;


            _space = new Space();
            Services.AddService(typeof(Space), _space);



            hoop1 = new Hoop(this, new Vector3(0.0f, 0.0f, 30.0f), new Vector3(0.0f, 0.0f, 0.0f));
            Components.Add(hoop1);

            hoop2 = new Hoop(this, new Vector3(10.0f, 0.0f, 120.0f), new Vector3(0.0f, 0.0f, 0.0f));
            Components.Add(hoop2);

            hoop3 = new Hoop(this, new Vector3(30.0f, 0.0f, 250.0f), new Vector3(0.0f, 0.0f, 0.0f));
            Components.Add(hoop3);

            hoop4 = new Hoop(this, new Vector3(60.0f, 0.0f, 300.0f), new Vector3(0.0f, 0.0f, 0.0f));
            Components.Add(hoop4);


            hoop5 = new Hoop(this, new Vector3(100.0f, 0.0f, 400.0f), new Vector3(0.0f, 0.0f, 0.0f));
            Components.Add(hoop5);


            hoop6 = new Hoop(this, new Vector3(140.0f, 0.0f, 480.0f), new Vector3(0.0f, 0.0f, 0.0f));
            Components.Add(hoop6);


            hoop7 = new Hoop(this, new Vector3(130.0f, 0.0f, 600.0f), new Vector3(0.0f, 0.0f, 0.0f));
            Components.Add(hoop7);

            hoops = new ArrayList { hoop1, hoop2, hoop3, hoop4, hoop5, hoop6, hoop7};


            hoop1.nextHoop = true;


            clock = new Clock(this);
            Components.Add(clock);

            _spaceship = new Spaceship(this, clock);
            Components.Add(_spaceship);

            sky = new Skyball(this, new Vector3(0, 0, -4000), 200);
            Components.Add(sky);

            //set up camera
            view = _spaceship.view;
            projection = _spaceship.projection;
            cameraPosition = _spaceship.cameraPosition;
            world = _spaceship.world;


            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                Exit();

            _space.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            view = _spaceship.view;
            projection = _spaceship.projection;
            cameraPosition = _spaceship.cameraPosition;
            world = _spaceship.world;




            //from end check for first completed hoop, subtract the current index to get hoops missed and light up the next hoop
            //or stop time if last hoop is completed
            for (int i = hoops.Count-1; i >= curHoopIndex; i--)
            {
                if ((hoops[i] as Hoop).completed == true) {

                    //1 minute added to score for each missed hoop
                    clock.missed += (i - curHoopIndex);
                    (hoops[curHoopIndex] as Hoop).nextHoop = false;

                    curHoopIndex = i+1;

                    if (curHoopIndex < hoops.Count)
                    {
                        (hoops[i+1] as Hoop).nextHoop = true;
                    }
                    else
                    {
                        clock.StopTime();

                    }

                    break;
                }
            }
               




            sky.modelPosition = MathConverter.Convert(_spaceship._spaceEntity.Position);


            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

            //texture is on outside of sphere and triangles not facing camera get culled so switch raster cullmode while drawing skybox
            _graphics.GraphicsDevice.RasterizerState = skyboxRaster;

            sky.Draw(gameTime);

            //then switch back
            _graphics.GraphicsDevice.RasterizerState = defaultRaster;

            //draw clock
            _spriteBatch.Begin();
            base.Draw(gameTime);
            _spriteBatch.End();

            //run after spritebatch or the 3d assets get messed up
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;


        }
    }
}
