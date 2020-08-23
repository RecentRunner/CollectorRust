using System.Linq;
using Collector.Character;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Collector.Dimension
{
    public class WorldRenderer : IRestrictions
    {
        private readonly PlayerMouse _playerMouse;
        private readonly InputController _inputController;
        private readonly SpriteBatch _spriteBatch;
        private readonly Main _main;
        private readonly World _world;

        public WorldRenderer(PlayerMouse playerMouse, InputController inputController, SpriteBatch spriteBatch, Main main, World world)
        {
            _playerMouse = playerMouse;
            _inputController = inputController;
            _spriteBatch = spriteBatch;
            _main = main;
            _world = world;
            _world.Impassable.Add(Blocks.BlockWater);
            _world.Impassable.Add(Blocks.BlockRoof);
            _world.Impassable.Add(Blocks.BlockWall);
        }

        private void DrawWorld(SpriteBatch batch, int layer)
        {
            foreach (var chunkpair in _world.LoadedChunks.Keys.Where(chunkpair => layer==chunkpair.Item3))
            {
                batch.Draw(
                    Main.Materials[_world.LoadedChunks[chunkpair]],
                    new Rectangle(
                        chunkpair.Item1,
                        chunkpair.Item2,
                        IRestrictions.TileSize, IRestrictions.TileSize
                    ),
                    Color.White
                );
            }
        }

        public void Draw(GameTime gameTime)
        {
            //Higher means draws in a lower layer
            DrawWorld(_spriteBatch,0);
            _playerMouse.Draw();
            DrawWorld(_spriteBatch,1);
            _inputController.Draw();
            _inputController.PlayerInput(gameTime);
            //DrawWorld(_spriteBatch,2);
        }
    }
}