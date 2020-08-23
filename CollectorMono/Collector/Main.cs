using System;
using System.Collections.Generic;
using System.IO;
using Collector.Character;
using Collector.Dimension;
using Collector.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using Myra;
using Myra.Graphics2D.UI;

namespace Collector
{
    public class Main : Game
    {
        private SpriteBatch _spriteBatch;
        private InputController _inputController;
        private Player Player1 { get; set; }
        private PlayerMouse _playerMouse;
        private OrthographicCamera _cam;
        private int _virtualWidth;
        private int _virtualHeight;
        private readonly Desktop _desktop;
        private Gui _gui;
        private World _world;
        private WorldRenderer WorldRenderer { get; set; }
        public static Dictionary<Blocks, Texture2D> Materials { get; } = new Dictionary<Blocks, Texture2D>();


        public Main()
        {
            _desktop = new Desktop();
            var graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
            IsFixedTimeStep = true;
            graphics.PreferMultiSampling = true;
        }

        protected override void Initialize()
        {
            _gui = new Gui(_desktop);

            base.Initialize();

            foreach (Blocks name in Enum.GetValues(typeof(Blocks)))
            {
                Materials.Add(name, Content.Load<Texture2D>(name.ToString()));
            }

            _virtualWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _virtualHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, _virtualWidth, _virtualHeight);
            _world = new World();
            Player1 = new Player(0, 0, _world);
            _cam = new OrthographicCamera(viewportAdapter);
            _cam.LookAt(new Vector2(Player.X, Player.Y));
            _cam.Zoom = IRestrictions.Zoom;

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _playerMouse = new PlayerMouse(Content, _spriteBatch, _cam);
            _inputController = new InputController(_cam, _spriteBatch, Content, this,_world,Player1,_playerMouse);
            WorldRenderer = new WorldRenderer(_playerMouse, _inputController, _spriteBatch, this,_world);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            MyraEnvironment.Game = this;

            _gui.LoadGui();
        }


        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _world.LoadChunks();
            _world.UnloadChunks();
            Gui.Update();
        }

        public void Quit()
        {
            Exit();
        }

        protected override void Draw(GameTime gameTime)
        {
            var transformMatrix = _cam.GetViewMatrix();
            base.Draw(gameTime);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //Turn on Anti-aliasing by changing SamplerState.PointClamp
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                DepthStencilState.None, RasterizerState.CullNone, transformMatrix: transformMatrix);
            _playerMouse.Draw();
            WorldRenderer.Draw(gameTime);
            _spriteBatch.End();
            _gui.Render();
        }
    }
}