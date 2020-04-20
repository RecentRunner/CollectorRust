using Collector.Dimension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Collector
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private InputController _inputController;
        
        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {// TODO: Add your initialization logic here
            

            base.Initialize();
        }

        protected override void LoadContent()
        {// TODO: use this.Content to load your game content here
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            BlockMaterials.LoadContent(Content);
        }
        
        protected override void Update(GameTime gameTime)
        {// TODO: Add your update logic here/*
            _inputController.PlayerInput();
            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            base.Draw(gameTime);
            _spriteBatch.Begin();
            BlockMaterials.Draw("grass",_spriteBatch,0,0);
            _spriteBatch.End();
        }
    }
}