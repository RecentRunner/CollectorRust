using System;
using System.Collections.Generic;
using System.IO;
using Collector.Character;
using Collector.Dimension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace Collector
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private InputController _inputController;
        private Player _player;
        private PlayerMouse _playerMouse;
        private static OrthographicCamera _cam;
        private static int _virtualWidth;
        private static int _virtualHeight;
        private WorldRenderer WorldRenderer { get; set; }
        public static Dictionary<Blocks, Texture2D> Materials { get; } = new Dictionary<Blocks, Texture2D>();


        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
            IsFixedTimeStep = true;
            _graphics.PreferMultiSampling = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            foreach (Blocks name in Enum.GetValues(typeof(Blocks))) 
            {
                Materials.Add(name,Content.Load<Texture2D>(name.ToString()));
            }

            _virtualWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _virtualHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            
            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, _virtualWidth, _virtualHeight);
            _player = new Player(0, 0);
            _cam = new OrthographicCamera(viewportAdapter);
            _cam.LookAt(new Vector2(Player.X, Player.Y));
            _cam.Zoom = IRestrictions.Zoom;
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _playerMouse = new PlayerMouse(Content, _spriteBatch, _cam);
            _inputController = new InputController(_cam, _spriteBatch, Content);
            WorldRenderer = new WorldRenderer(_playerMouse, _inputController, _spriteBatch, this);
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here/*
            base.Update(gameTime);
            World.LoadChunks();
            World.UnloadChunks();
        }

        protected override void Draw(GameTime gameTime)
        {// TODO: Add your drawing code here
            var transformMatrix = _cam.GetViewMatrix();
            base.Draw(gameTime);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //Turn on Anti-aliasing by changing SamplerState.PointClamp
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, transformMatrix: transformMatrix);
            _playerMouse.Draw();
            WorldRenderer.Draw(gameTime);
            _spriteBatch.End();
        }
    }
}