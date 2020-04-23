using System;
using System.IO;
using Collector.Character;
using Collector.Dimension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using Mouse = Collector.Character.Mouse;

namespace Collector
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private InputController _inputController;
        private Player _player;
        private Mouse _mouse;
        private OrthographicCamera _cam;
        private WorldRenderer WorldRenderer { get; set; }

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            BlockMaterials.Initialize(Content);

            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 480);
            _player = new Player(0, 0);
            _cam = new OrthographicCamera(viewportAdapter);
            _cam.LookAt(new Vector2(Player.X, Player.Y));

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _mouse = new Mouse(Content, _spriteBatch, _cam);
            _inputController = new InputController(_mouse, _cam, _spriteBatch, Content);
            WorldRenderer = new WorldRenderer(_mouse, _inputController, _player, _spriteBatch, this, _cam);
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
            _spriteBatch.Begin(transformMatrix: transformMatrix);
            _mouse.Draw();
            WorldRenderer.Draw(gameTime);
            _spriteBatch.End();
        }
    }
}