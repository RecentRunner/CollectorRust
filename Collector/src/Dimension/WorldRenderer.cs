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

        public WorldRenderer(PlayerMouse playerMouse, InputController inputController, SpriteBatch spriteBatch, Main main)
        {
            _playerMouse = playerMouse;
            _inputController = inputController;
            _spriteBatch = spriteBatch;
            _main = main;
            Chunks.Impassable.Add(Blocks.BlockWater);
            Chunks.Impassable.Add(Blocks.BlockRoof);
            Chunks.Impassable.Add(Blocks.BlockWall);
        }

        private static void DrawWorld(SpriteBatch batch, int layer)
        {
            foreach (var chunkpair in Chunks.LoadedChunks.Keys.Where(chunkpair => layer==chunkpair.Item3))
            {
                batch.Draw(
                    Main.Materials[Chunks.LoadedChunks[chunkpair]],
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
            _inputController.PlayerInput(_main,gameTime);
            //DrawWorld(_spriteBatch,2);
        }
    }
}