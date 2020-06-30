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
        private readonly Chunks _chunks;

        public WorldRenderer(PlayerMouse playerMouse, InputController inputController, SpriteBatch spriteBatch, Main main, Chunks chunks)
        {
            _playerMouse = playerMouse;
            _inputController = inputController;
            _spriteBatch = spriteBatch;
            _main = main;
            _chunks = chunks;
            _chunks.Impassable.Add(Blocks.BlockWater);
            _chunks.Impassable.Add(Blocks.BlockRoof);
            _chunks.Impassable.Add(Blocks.BlockWall);
        }

        private void DrawWorld(SpriteBatch batch, int layer)
        {
            foreach (var chunkpair in _chunks.LoadedChunks.Keys.Where(chunkpair => layer==chunkpair.Item3))
            {
                batch.Draw(
                    Main.Materials[_chunks.LoadedChunks[chunkpair]],
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